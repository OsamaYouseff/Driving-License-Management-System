namespace DVLD_Project.Forms.Licences
{
    partial class ShowPersonLicenceHistoryForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPersonLicenceHistoryForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.driverLicenceHistory_CTRL = new DVLD_Project.Controls.Licences.DriverLicenceHistory_CTRL();
            this.personCardPlusFiliter_CTRL = new DVLD_Project.Controls.People.PersonCardPlusFiliter_CTRL();
            this.LicensesMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.LicensesMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1271, 39);
            this.lblTitle.TabIndex = 132;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // driverLicenceHistory_CTRL
            // 
            this.driverLicenceHistory_CTRL.Location = new System.Drawing.Point(100, 453);
            this.driverLicenceHistory_CTRL.Name = "driverLicenceHistory_CTRL";
            this.driverLicenceHistory_CTRL.Size = new System.Drawing.Size(1135, 407);
            this.driverLicenceHistory_CTRL.TabIndex = 138;
            // 
            // personCardPlusFiliter_CTRL
            // 
            this.personCardPlusFiliter_CTRL.FilterEnabled = true;
            this.personCardPlusFiliter_CTRL.Location = new System.Drawing.Point(304, 51);
            this.personCardPlusFiliter_CTRL.Name = "personCardPlusFiliter_CTRL";
            this.personCardPlusFiliter_CTRL.ShowAddPerson = true;
            this.personCardPlusFiliter_CTRL.Size = new System.Drawing.Size(931, 415);
            this.personCardPlusFiliter_CTRL.TabIndex = 135;
            // 
            // LicensesMenuStrip
            // 
            this.LicensesMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseInfoToolStripMenuItem});
            this.LicensesMenuStrip.Name = "LicensesMenuStrip";
            this.LicensesMenuStrip.Size = new System.Drawing.Size(209, 26);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Font = new System.Drawing.Font("SF Pro Text", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showLicenseInfoToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_id_card_487;
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showLicenseInfoToolStripMenuItem.Text = "Show License Info";
            // 
            // CloseBtn
            // 
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBtn.Font = new System.Drawing.Font("SF Pro Rounded", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Image = global::DVLD_Project.Properties.Resources.icons8_close_24;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(615, 869);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(108, 53);
            this.CloseBtn.TabIndex = 137;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD_Project.Properties.Resources.icons8_view_942;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(29, 164);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(220, 189);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 133;
            this.pbPersonImage.TabStop = false;
            // 
            // ShowPersonLicenceHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseBtn;
            this.ClientSize = new System.Drawing.Size(1345, 928);
            this.Controls.Add(this.driverLicenceHistory_CTRL);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.personCardPlusFiliter_CTRL);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowPersonLicenceHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Person Licence History";
            this.Load += new System.EventHandler(this.ShowPersonLicenceHistoryForm_Load);
            this.LicensesMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.Label lblTitle;
        private Controls.People.PersonCardPlusFiliter_CTRL personCardPlusFiliter_CTRL;
        private System.Windows.Forms.Button CloseBtn;
        private Controls.Licences.DriverLicenceHistory_CTRL driverLicenceHistory_CTRL;
        private System.Windows.Forms.ContextMenuStrip LicensesMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
    }
}