using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SmartHoldInstall
{


    public partial class Main : Form
    {
        const string REGISTRY_I3PATH = @"SOFTWARE\Wow6432Node\Interactive Intelligence";
        const string REGISTRY_I3ROOT = @"SOFTWARE\Wow6432Node\Interactive Intelligence\EIC\Directory Services\Root";

        List<string> oldPromptNames = new List<string>(16) { "Callback_Greeting.wav"
            ,"Callback_CurrentNumberA.wav"
            , "Callback_CurrentNumberB.wav"
            , "Callback_EnterPhone.wav"
            , "Callback_EnterPhoneIntl.wav"
            , "Callback_ExtQuestion.wav"
            , "Callback_CollectExt.wav"
            , "Callback_Invalid.wav"
            , "Callback_ReplaceA.wav"
            , "Callback_ReplaceB.wav"
            , "Callback_CallbackA.wav"
            , "Callback_CallbackB.wav"
            , "Callback_YouEnteredA.wav"
            , "Callback_YouEnteredB.wav"
            , "Callback_YouEnteredExt.wav"
            , "Callback_Goodbye.wav"
        };

        List<string> addPromptNames = new List<string>()
        {
            "SmartHold_AD_AlertAgent.wav"
            , "SmartHold_PreQueueCallback.wav"
            , "SmartHold_PreQueueCallbackAssigned.wav"
            ,"SmartHold_PreQueueKeepCallback.wav"
            , "SmartHold_PreQueueKeepContToQueue.wav"
        };

        List<string> customizationPoints = new List<string>()
        {
            "CustomAcdInitiateProcessing.ihd"
            , "CustomACDPostAlertInteraction.ihd"
            , "CustomAcdWaitSetCallState.ihd"
            , "CustomCallDisconnect.ihd"
            , "CustomGetDigitsExAsync.ihd"
            , "CustomInitiateCallRequestAudio.ihd"
            , "CustomInitiateCallRequestComplete.ihd"
            , "CustomIntAttRequestCallbackInit.ihd"
            , "CustomIVRUserQueuePostAlert.ihd"
            , "CustomTransferCallRequest.ihd"
        };

        List<bool> TaskAutoProcessCompleted = new List<bool>() { true, true, true, false, false, false, false, true, false };

        //Initialized in Constructor for Main. Used to write out Installation logging
        FileStream writer;

        public Main()
        {
            InitializeComponent();
            Startup_RegistryLookup();
            writer = File.Open(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"InstallationLog.txt"), FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            this.txtUser.Focus();
        }

        private void Startup_RegistryLookup()
        {
            //Lookup I3 path in Registry
            RegistryKey registryKey;
            string serverName = string.Empty;
            // Lookup the Primary Server Name
            try
            {
                registryKey = Registry.LocalMachine.OpenSubKey(REGISTRY_I3ROOT);
                serverName = ((string[])registryKey.GetValue("SERVER"))[0];
                serverName = serverName.Substring(serverName.LastIndexOf(@"\") + 1);
                registryKey = Registry.LocalMachine.OpenSubKey(REGISTRY_I3PATH);
                this.txtICFolder.Text = (string)registryKey.GetValue("Value");
                this.txtNotifier.Text = serverName;
            }
            catch (Exception)
            {
                this.txtNotifier.Text = serverName;
            }

            if (serverName.Equals(string.Empty))
            {
                this.txtCustomHandlerFolder.Text = string.Empty;
            }
            else if (serverName.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))
            {
                this.txtCustomHandlerFolder.Text = this.txtICFolder.Text + @"Handlers\Custom\";
            }
            else
            {
                //this.txtCustomHandlerFolder.Text = @"\\" + serverName + @"\" + this.txtICFolder.Text.Replace(":", "$") + @"Handlers\Custom\";
                this.txtCustomHandlerFolder.Text = Path.Combine(@"\\" + serverName, this.txtICFolder.Text.Replace(":", "$"), @"Handlers\Custom\");
            }
        }

        #region Upgrade Tasks
        private void CopyOrRenamePromptFiles()
        {
            LogEntry("Process Prompts", "---- SmartHold Prompts - If old Prompt files exist, make a copy and Rename appropriately otherwise Copy from package ----", Color.Black);
            string promptPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Prompts\";

            string i3Path = VerifyI3Path();
            if (i3Path.Equals(string.Empty))
            {
                return;
            }

            i3Path += @"Resources\InteractionAttendantWaves\";
            string[] langFolderPath = Directory.GetDirectories(i3Path);
            foreach (string dirPath in langFolderPath)
            {
                //Get Folder Name without path
                string folderName = dirPath.Remove(0, i3Path.Length);
                string pathToCopy = dirPath + @"\";

                //Log which language folder we are converting
                LogEntry("Process Prompts", @" - ************ " + folderName + " ************", Color.Black);

                //Handle the english and spanish language folders
                //                if (folderName.Equals("en-US", StringComparison.CurrentCultureIgnoreCase) || (folderName.Substring(0, 2).Equals("es", StringComparison.CurrentCultureIgnoreCase) && folderName.Length > 3))
                if (folderName.Equals("en-US", StringComparison.CurrentCultureIgnoreCase) || (folderName.Substring(0, 2).Equals("es", StringComparison.CurrentCultureIgnoreCase)))
                {
                    foreach (string fileName in oldPromptNames)
                    {
                        string oldFileNamePath = pathToCopy + fileName;
                        Regex regex = new Regex(Regex.Escape("Callback"));
                        string newFileName = regex.Replace(fileName, "SmartHold", 1);

                        switch (fileName)
                        {
                            case "Callback_EnterPhone.wav":
                                //Callback_EnterPhone.wav is being changed to SmartHold_EnterPhone_10.wav
                                newFileName = newFileName.Replace(".wav", "_10.wav");
                                break;
                            case "Callback_EnterPhoneIntl.wav":
                                //Callback_EnterPhoneIntl.wav is being changed to SmartHold_EnterPhone_Default.wav
                                newFileName = newFileName.Replace("Intl.wav", "_Default.wav");
                                break;
                            default:
                                break;
                        }
                        

                        if (File.Exists(oldFileNamePath))
                        {
                            ///File Exists, Make a copy and rename the file
                            try
                            {
                            File.Copy(oldFileNamePath, pathToCopy + newFileName);
                            LogEntry("Process Prompts", string.Format(@"{0} - made copy of existing file and renamed to {1}", fileName, newFileName), Color.Green);
                            }
                            catch (Exception ex)
                            {
                                LogEntry("Process Prompts", ex.Message, Color.Red);
                            }
                        }
                        else
                        {
                            ///File does not exist, copy the file into the folder
                            try
                            {
                                File.Copy(promptPath + newFileName, pathToCopy + newFileName);
                                LogEntry("Process Prompts", string.Format(@"SmartHold package version of {0} copied and placed in {1}", newFileName, dirPath), Color.Blue);
                            }
                            catch (Exception ex)
                            {
                                LogEntry("Process Prompts", ex.Message, Color.Red);
                            }
                        }

                        ///Now copy all the new prompts that were added to SmartHold 4.3

                    }
                    foreach (string fileName in addPromptNames)
                    {
                        try
                        {
                            File.Copy(promptPath + fileName, pathToCopy + fileName);
                            LogEntry("Process Prompts", string.Format(@"SmartHold package version of {0} copied and placed in {1}", fileName, dirPath), Color.Blue);
                        }
                        catch (Exception ex)
                        {
                            LogEntry("Process Prompts", ex.Message, Color.Red);
                        }
                    }

                }
                else
                {
                    ///Check to see if there are SmartHold prompt files for this language.
                    foreach (string fileName in oldPromptNames)
                    {
                        string oldFileNamePath = pathToCopy + fileName;
                        Regex regex = new Regex(Regex.Escape("Callback"));
                        string newFileName = regex.Replace(fileName, "SmartHold", 1);

                        switch (fileName)
                        {
                            case "Callback_EnterPhone.wav":
                                //Callback_EnterPhone.wav is being changed to SmartHold_EnterPhone_10.wav
                                newFileName = newFileName.Replace(".wav", "_10.wav");
                                break;
                            case "Callback_EnterPhoneIntl.wav":
                                //Callback_EnterPhoneIntl.wav is being changed to SmartHold_EnterPhone_Default.wav
                                newFileName = newFileName.Replace("Intl.wav", "_Default.wav");
                                break;
                            default:
                                break;
                        }

                        if (File.Exists(oldFileNamePath))
                        {
                            ///File Exists, Make a copy and rename the file
                            try
                            {
                                File.Copy(oldFileNamePath, pathToCopy + newFileName);
                                LogEntry("Process Prompts", string.Format(@"{0} - made copy of existing file and renamed to {1}", fileName, newFileName), Color.Green);
                            }
                            catch (Exception ex)
                            {
                                LogEntry("Process Prompts", ex.Message, Color.Red);
                            }
                        }
                        else
                        {
                            ///Prompt does not exist, to use SmartHold for this language, customer must record the necessary prompt
                            File.Copy(promptPath + newFileName, pathToCopy + newFileName);
                            LogEntry("Process Prompts", string.Format(@"SmartHold package version of {0} copied and placed in {1}", fileName, dirPath), Color.Blue);
                        }

                    }
                    ///Now log all the new prompts that were added to SmartHold 4.3 for customer to record
                    foreach (string fileName in addPromptNames)
                    {
                        try
                        {
                            File.Copy(promptPath + fileName, pathToCopy + fileName);
                            LogEntry("Process Prompts", string.Format(@"SmartHold package version of {0} copied and placed in {1}", fileName, dirPath), Color.Blue);
                        }
                        catch (Exception ex)
                        {
                            LogEntry("Process Prompts", ex.Message, Color.Red);
                        }

                    }

                }
            }



        }

        private void PublishCustomHandlers()
        {
            string i3Path = VerifyI3Path();
            if (i3Path.Equals(string.Empty))
            {
                return;
            }

            LogEntry("Publish Custom Handlers", "---- Publish all Custom SmartHold handler (Does not include Customization Points and System Handlers ----", Color.Black);
            //Publish Handlers using the eicpublisheru command
            string pubFileFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "pub Files");
            LogEntry("Publish Custom Handlers", @"Pub File Path is " + pubFileFolder, Color.Black);
            Process pubProcess = new Process();
            pubProcess.StartInfo.WorkingDirectory = pubFileFolder;
            pubProcess.StartInfo.FileName = Path.Combine(this.txtICFolder.Text, @"Server\eicpublisheru.exe");
            LogEntry("Publish Custom Handlers", @"EicPublisherU Executable " + pubProcess.StartInfo.FileName, Color.Black);
            pubProcess.StartInfo.Arguments = "/notifier=" + txtNotifier.Text + " /user=" + txtUser.Text + " /password=" + txtPassword.Text + " /verbose @\"" + Path.Combine(pubFileFolder,"pubFiles.lst") + "\"";
            pubProcess.StartInfo.UseShellExecute = false;
            pubProcess.StartInfo.RedirectStandardOutput = true;
            pubProcess.StartInfo.RedirectStandardError = true;
            //Set output and error (asynchronous) handlers
            pubProcess.OutputDataReceived += new DataReceivedEventHandler(ProcessPublishOutputHandler);
            pubProcess.ErrorDataReceived += new DataReceivedEventHandler(ProcessPublishErrorHandler);
            //start the eicpublisheru process
            pubProcess.Start();
            pubProcess.BeginErrorReadLine();
            pubProcess.BeginOutputReadLine();
            pubProcess.WaitForExit();

            LogEntry("", "", Color.Black);
            LogEntry("Publish Custom Handlers", @"---- Place Copy of Custom Handler in the Custom\SmartHold folder ----", Color.Black);

            string smartHoldCustomFolder = Path.Combine(this.txtCustomHandlerFolder.Text, @"SmartHold");
            string smartHoldSourceFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SmartHold");

            //Check if directory exists if so rename to SmartHold_old
            if (Directory.Exists(smartHoldCustomFolder))
            {
                //IEnumerable<string> directories = Directory.EnumerateDirectories(smartHoldCustomFolder + @"_old");
                List<string> directories = new List<string>(Directory.GetDirectories(this.txtCustomHandlerFolder.Text, @"SmartHold_old*"));
                Directory.Move(smartHoldCustomFolder, smartHoldCustomFolder + @"_old" + directories.Count().ToString());
                LogEntry("Publish Custom Handlers", @"Renamed SmartHold folder to SmartHold_old in Handlers\Custom ", Color.Black);
            }
            //Create the SmartHold folder
            Directory.CreateDirectory(smartHoldCustomFolder);

            //Copy all SmartHold_ ihd from the Server\Handler folder to Custom\SmartHold folder
            string[] files = Directory.GetFiles(smartHoldSourceFolder, "*.ihd");
            foreach (string file in files)
            {
                string filename = file.Remove(0, smartHoldSourceFolder.Length + 1);
                string destFile = Path.Combine(smartHoldCustomFolder, filename);
                File.Copy(file, destFile);
                LogEntry("Copy Custom Handlers", string.Format("{0} placed in {1}", filename, smartHoldCustomFolder), Color.Green);
            }

            //Copy SmartHold Administration Guide to Custom\SmartHold\Documentation
            string adminGuide = Path.Combine(new string[] { smartHoldSourceFolder, "Documentation", "Smart Hold Administration Guide.docx" });
            string docFolder = Path.Combine(smartHoldCustomFolder, "Documentation");
            try
            {
                Directory.CreateDirectory(docFolder);
            }
            catch { }
            string adminGuideCopy = Path.Combine(docFolder, "Smart Hold Administration Guide.docx");

            File.Copy(adminGuide, adminGuideCopy);
            LogEntry("Copy Admininstrator Guide", string.Format("SmartHold Administrator Guide placed in {0}", docFolder), Color.Green);

        }

        private void SendCustomNotification()
        {
            try
            {
                writer.Close();
            }
            catch (Exception) {}

            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(new string[] { this.txtICFolder.Text, @"Server", @"sendcustomnotification.exe" });
            process.StartInfo.Arguments = "SmartHold RunInstall \"" + Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"InstallationLog.txt") + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //Set output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler(ProcessCustomNotificationOutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(ProcessCustomNotificationErrorHandler);
            //start the eicpublisheru process
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();

        }

        private void CopyReports()
        {
            string i3Path = VerifyI3Path();
            if (i3Path.Equals(string.Empty))
            {
                return;
            }

            string serverReportFolder = Path.Combine(new string[] { i3Path, "Server", "Reports", "SQL", "Custom" });
            try
            {
                Directory.CreateDirectory(serverReportFolder);
            }
            catch {
                MessageBox.Show(string.Format("Unable to create the following directory for the custom reports: {0}", serverReportFolder), "Copy Reports to Server Folder", MessageBoxButtons.OK);
                LogEntry("Create Report Directory", string.Format(@"Unable to Create the Custom Report Directory, please manually create this and copy the reports to it {0}", serverReportFolder), Color.Red);
                return;
            }
            //Copy all SmartHold_ ihd from the Server\Handler folder to Custom\SmartHold folder
            string reportFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Reports");
            string[] files = Directory.GetFiles(reportFolder, "*.rpt");
            foreach (string file in files)
            {
                string filename = file.Remove(0, reportFolder.Length + 1);
                string destFile = Path.Combine(serverReportFolder, filename);
                try
                {
                    File.Copy(file, destFile);
                    LogEntry("Copy Report Files", string.Format("{0} placed in {1}", filename, serverReportFolder), Color.Green);
                }
                catch (Exception ex)
                {
                    LogEntry("Copy Report Files", ex.Message, Color.Red);
                }
            }
        }
        
        #endregion Upgrade Tasks

        #region Form Events

         private void NecessaryDataFilledIn(object sender, EventArgs e)
        {
            bool completed = (this.txtNotifier.Text.Length > 0 && this.txtUser.Text.Length > 0 && this.txtPassword.Text.Length > 0);
            this.btnStart.Enabled = completed;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnPreRequisiteTasks_Click(null, null);
            //Copy Report files to the Custom Report folder
            CopyReports();

            //Verify the Prompts, rename as needed or copy if not existing
            CopyOrRenamePromptFiles();

            PublishCustomHandlers();

            //Send Custom Notification to write the Report Log and Structured Parameter
            SendCustomNotification();
        }

        private void btnPreRequisiteTasks_Click(object sender, EventArgs e)
        {
            PreRequisiteTasks form = new PreRequisiteTasks();
            form.ShowDialog();
        }

         private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                LogEntry("Upgrade Complete", "-------------------------------- End of SmartHold Upgrade --------------------------------", Color.Black);
                writer.Flush();
                writer.Close();
            }
            catch { }
        }

       #endregion Form Events

        private void LogEntry(string process, string msg, Color color)
        {
            string logText = DateTime.UtcNow.ToString("s") + @" - " + process.PadRight(25) + msg + Environment.NewLine;
            if (!writer.CanWrite)
            {
                writer = File.Open(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"InstallationLog.txt"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            }

            if (process.Length == 0 && msg.Length == 0)
            {
                //writer.WriteLine("");
                byte[] newLine = Encoding.Default.GetBytes(Environment.NewLine);
                writer.Write(newLine, 0, newLine.Length);
            }
            else
            {
                byte[] newEntry = Encoding.Default.GetBytes(logText);
                writer.Write(newEntry, 0, newEntry.Length);
                //writer.WriteLine(DateTime.UtcNow.ToString("s") + @" - " + process.PadRight(25) + msg);
            }
            writer.Flush();

            this.txtResults.Select(txtResults.TextLength, 0);
            this.txtResults.SelectionColor = color;
            this.txtResults.AppendText(logText);
        }

        private string VerifyI3Path()
        {
            string i3Path = this.txtICFolder.Text;

            if (i3Path.Substring(i3Path.Length - 1, 1) == @"\")
            {
                if (!i3Path.Substring(i3Path.Length - 6).Equals(@"i3\ic\", StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show(@"Please verify that the IC folder is pointing to IC root folder (ie: D:\I3\IC)", "Location of IC folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return string.Empty;
                }
            }
            else
            {
                if (!i3Path.Substring(i3Path.Length - 5).Equals(@"i3\ic", StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show(@"Please verify that the IC folder is pointing to IC root folder (ie: D:\I3\IC)", "Location of IC folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return string.Empty;
                }
                else
                {
                    i3Path += @"\";
                }
            }
            return i3Path;

        }

        private void ProcessPublishOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            LogEntry("Publish Custom Handlers", outLine.Data, Color.Green);
        }

        private void ProcessPublishErrorHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            LogEntry("Publish Custom Handlers", outLine.Data, Color.Red);
        }

        private void ProcessCustomNotificationOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            LogEntry("Custom Notification", string.Format("Success: {0}",outLine.Data), Color.Green);
        }

        private void ProcessCustomNotificationErrorHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            LogEntry("Custom Notification", string.Format("Failed: {0}", outLine.Data), Color.Red);
            LogEntry("Custom Notification", "You can run the custom notification manually. Open a cmd window. Enter 'sendcustomnotification.exe SmartHold RunInstall' (without the single quotes).", Color.Red);
            LogEntry("Custom Notification", "The following tasks or completed with the custom notification:", Color.Black);
            LogEntry("Custom Notification", "... Create the SmartHold Structured Parameter", Color.Black);
            LogEntry("Custom Notification", "... Set Report Logs 9999 - Custom Passthrough Active to Yes", Color.Black);
            LogEntry("Custom Notification", "... Report Log 112 custom map string to the Workgroup Queue Statistics Interval Report Log", Color.Black);
            LogEntry("Custom Notification", "... Add the Server Parameter ACDDoNotDeleteObjectsUntilDeallocation and set it to 1", Color.Black);
            LogEntry("Custom Notification", "... Create the Custom Schema in the I3 Database", Color.Black);
            LogEntry("Custom Notification", "... Create the SmartHold Report Tables in the I3 Database", Color.Black);
            LogEntry("Custom Notification", "... Create the SmartHold Report views in the I3 Database", Color.Black);
        }

    }
}
