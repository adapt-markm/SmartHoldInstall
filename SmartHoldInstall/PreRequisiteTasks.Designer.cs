namespace SmartHoldInstall
{
    partial class PreRequisiteTasks
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
            this.txtFirstSteps = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtFirstSteps
            // 
            this.txtFirstSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFirstSteps.Location = new System.Drawing.Point(0, 0);
            this.txtFirstSteps.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFirstSteps.Name = "txtFirstSteps";
            this.txtFirstSteps.ReadOnly = true;
            this.txtFirstSteps.Size = new System.Drawing.Size(907, 323);
            this.txtFirstSteps.TabIndex = 0;
            this.txtFirstSteps.Text = "";
            // 
            // PreRequisiteTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 323);
            this.Controls.Add(this.txtFirstSteps);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PreRequisiteTasks";
            this.Text = "PreRequisiteTasks";
            this.Load += new System.EventHandler(this.PreRequisiteTasks_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtFirstSteps;
    }
}