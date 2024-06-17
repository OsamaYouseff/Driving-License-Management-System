namespace DVLD_Project.Forms.Applications.Driving_License.Local_Driving_Licence
{
    partial class ShowLocalDrivingLicenseApplicationInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowLocalDrivingLicenseApplicationInfoForm));
            this.CloseBtn = new System.Windows.Forms.Button();
            this.drivingLicenceinfo_CTRL = new DVLD_Project.Controls.Applications.Driving_Licence.Local_Driving_Licence.DrivingLicenceinfo_CTRL();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBtn.Font = new System.Drawing.Font("SF Pro Rounded", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Image = global::DVLD_Project.Properties.Resources.icons8_close_24;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(431, 415);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(108, 53);
            this.CloseBtn.TabIndex = 143;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // drivingLicenceinfo_CTRL
            // 
            this.drivingLicenceinfo_CTRL.Location = new System.Drawing.Point(32, 31);
            this.drivingLicenceinfo_CTRL.Name = "drivingLicenceinfo_CTRL";
            this.drivingLicenceinfo_CTRL.Size = new System.Drawing.Size(906, 371);
            this.drivingLicenceinfo_CTRL.TabIndex = 0;
            // 
            // ShowLocalDrivingLicenseApplicationInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseBtn;
            this.ClientSize = new System.Drawing.Size(975, 480);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.drivingLicenceinfo_CTRL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowLocalDrivingLicenseApplicationInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving Licence Application Info ";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Applications.Driving_Licence.Local_Driving_Licence.DrivingLicenceinfo_CTRL drivingLicenceinfo_CTRL;
        private System.Windows.Forms.Button CloseBtn;
    }
}