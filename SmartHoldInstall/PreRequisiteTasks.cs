using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartHoldInstall
{
    public partial class PreRequisiteTasks : Form
    {
        public PreRequisiteTasks()
        {
            InitializeComponent();
        }

        private void PreRequisiteTasks_Load(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder("01.\tCreate an ACD queue named SmartHold_Flowout (this is case sensitive).");
            text.AppendLine(@"\tIn the Extension field type DO NOT DELETE!!!");
            text.AppendLine(@"\tCheck the Workgroup has Queue and verify that ACD is selected in dropdown. Click OK");
            text.AppendLine(@"02. Import the four (4) IA tables:");
            text.AppendLine(@"          a. SmartHold_Workgroup");
            text.AppendLine(@"          b. SmartHold_DNIS");
            text.AppendLine(@"          c. SmartHold_AutoDial_Workgroup");
            text.AppendLine(@"          d. SmartHold_AutoDial_Agent");
            text.AppendLine(@"03. Create the Reporting tables in the database.(see Installation guide step 4)");
            text.AppendLine(@"04. Import the Reports in Administrator (see Installation guide)");


            txtFirstSteps.Text = text.ToString();
        }
    }
}
