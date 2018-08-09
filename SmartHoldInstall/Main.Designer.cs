using System;

namespace SmartHoldInstall
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSelectServerFolder = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtNotifier = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblNotifier = new System.Windows.Forms.Label();
            this.txtCustomHandlerFolder = new System.Windows.Forms.TextBox();
            this.lblCustomHandlers = new System.Windows.Forms.Label();
            this.txtICFolder = new System.Windows.Forms.TextBox();
            this.lblServerHandlers = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.btnPreRequisiteTasks = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPreRequisiteTasks);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnSelectServerFolder);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.txtNotifier);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Controls.Add(this.lblNotifier);
            this.panel1.Controls.Add(this.txtCustomHandlerFolder);
            this.panel1.Controls.Add(this.lblCustomHandlers);
            this.panel1.Controls.Add(this.txtICFolder);
            this.panel1.Controls.Add(this.lblServerHandlers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 100);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(291, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSelectServerFolder
            // 
            this.btnSelectServerFolder.Location = new System.Drawing.Point(291, 11);
            this.btnSelectServerFolder.Name = "btnSelectServerFolder";
            this.btnSelectServerFolder.Size = new System.Drawing.Size(24, 23);
            this.btnSelectServerFolder.TabIndex = 11;
            this.btnSelectServerFolder.Text = "...";
            this.btnSelectServerFolder.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(591, 36);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(120, 19);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(413, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(137, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.TextChanged += new System.EventHandler(this.NecessaryDataFilledIn);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(413, 36);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(137, 20);
            this.txtUser.TabIndex = 8;
            this.txtUser.TextChanged += new System.EventHandler(this.NecessaryDataFilledIn);
            // 
            // txtNotifier
            // 
            this.txtNotifier.Location = new System.Drawing.Point(413, 13);
            this.txtNotifier.Name = "txtNotifier";
            this.txtNotifier.Size = new System.Drawing.Size(137, 20);
            this.txtNotifier.TabIndex = 7;
            this.txtNotifier.TextChanged += new System.EventHandler(this.NecessaryDataFilledIn);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(352, 65);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(352, 39);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 5;
            this.lblUser.Text = "User:";
            // 
            // lblNotifier
            // 
            this.lblNotifier.AutoSize = true;
            this.lblNotifier.Location = new System.Drawing.Point(352, 13);
            this.lblNotifier.Name = "lblNotifier";
            this.lblNotifier.Size = new System.Drawing.Size(43, 13);
            this.lblNotifier.TabIndex = 4;
            this.lblNotifier.Text = "Notifier:";
            // 
            // txtCustomHandlerFolder
            // 
            this.txtCustomHandlerFolder.Location = new System.Drawing.Point(130, 39);
            this.txtCustomHandlerFolder.Name = "txtCustomHandlerFolder";
            this.txtCustomHandlerFolder.Size = new System.Drawing.Size(161, 20);
            this.txtCustomHandlerFolder.TabIndex = 3;
            this.txtCustomHandlerFolder.Text = "D:\\I3\\IC\\Handlers\\Custom";
            // 
            // lblCustomHandlers
            // 
            this.lblCustomHandlers.AutoSize = true;
            this.lblCustomHandlers.Location = new System.Drawing.Point(13, 39);
            this.lblCustomHandlers.Name = "lblCustomHandlers";
            this.lblCustomHandlers.Size = new System.Drawing.Size(114, 13);
            this.lblCustomHandlers.TabIndex = 2;
            this.lblCustomHandlers.Text = "Custom Handler Folder";
            // 
            // txtICFolder
            // 
            this.txtICFolder.Location = new System.Drawing.Point(130, 13);
            this.txtICFolder.Name = "txtICFolder";
            this.txtICFolder.Size = new System.Drawing.Size(161, 20);
            this.txtICFolder.TabIndex = 1;
            this.txtICFolder.Text = "D:\\I3\\IC";
            // 
            // lblServerHandlers
            // 
            this.lblServerHandlers.AutoSize = true;
            this.lblServerHandlers.Location = new System.Drawing.Point(13, 13);
            this.lblServerHandlers.Name = "lblServerHandlers";
            this.lblServerHandlers.Size = new System.Drawing.Size(49, 13);
            this.lblServerHandlers.TabIndex = 0;
            this.lblServerHandlers.Text = "IC Folder";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 652);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(793, 38);
            this.panel2.TabIndex = 2;
            // 
            // txtResults
            // 
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Location = new System.Drawing.Point(0, 100);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(793, 552);
            this.txtResults.TabIndex = 3;
            this.txtResults.Text = "";
            // 
            // btnPreRequisiteTasks
            // 
            this.btnPreRequisiteTasks.Location = new System.Drawing.Point(591, 13);
            this.btnPreRequisiteTasks.Name = "btnPreRequisiteTasks";
            this.btnPreRequisiteTasks.Size = new System.Drawing.Size(120, 19);
            this.btnPreRequisiteTasks.TabIndex = 13;
            this.btnPreRequisiteTasks.Text = "PreRequisite Tasks...";
            this.btnPreRequisiteTasks.UseVisualStyleBackColor = true;
            this.btnPreRequisiteTasks.Click += new System.EventHandler(this.btnPreRequisiteTasks_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 690);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "SmartHold Installation Process";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCustomHandlerFolder;
        private System.Windows.Forms.Label lblCustomHandlers;
        private System.Windows.Forms.TextBox txtICFolder;
        private System.Windows.Forms.Label lblServerHandlers;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtNotifier;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblNotifier;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSelectServerFolder;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Button btnPreRequisiteTasks;
    }
}

