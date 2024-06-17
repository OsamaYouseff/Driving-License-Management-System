namespace DVLD_Project.Controls.Licences
{
    partial class DriverLicenceInfoWithFiliter_CTRL
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.SearchLicenseBtn = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.driverLicenceInfo_CTRL = new DVLD_Project.Controls.Licences.DriverLicenceInfo_CTRL();
            this.gbFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.SearchLicenseBtn);
            this.gbFilters.Controls.Add(this.txtLicenseID);
            this.gbFilters.Controls.Add(this.label1);
            this.gbFilters.Location = new System.Drawing.Point(2, 3);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(514, 63);
            this.gbFilters.TabIndex = 18;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Filter";
            // 
            // SearchLicenseBtn
            // 
            this.SearchLicenseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchLicenseBtn.Image = global::DVLD_Project.Properties.Resources.icons8_search_24;
            this.SearchLicenseBtn.Location = new System.Drawing.Point(464, 16);
            this.SearchLicenseBtn.Name = "SearchLicenseBtn";
            this.SearchLicenseBtn.Size = new System.Drawing.Size(44, 37);
            this.SearchLicenseBtn.TabIndex = 18;
            this.SearchLicenseBtn.UseVisualStyleBackColor = true;
            this.SearchLicenseBtn.Click += new System.EventHandler(this.SearchLicense_Btn_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicenseID.Location = new System.Drawing.Point(118, 21);
            this.txtLicenseID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(323, 26);
            this.txtLicenseID.TabIndex = 17;
            this.txtLicenseID.TextChanged += new System.EventHandler(this.txtLicenseID_TextChanged);
            this.txtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseID_KeyPress);
            this.txtLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.txtLicenseID_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "License ID:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // driverLicenceInfo_CTRL
            // 
            this.driverLicenceInfo_CTRL.Location = new System.Drawing.Point(1, 72);
            this.driverLicenceInfo_CTRL.Name = "driverLicenceInfo_CTRL";
            this.driverLicenceInfo_CTRL.Size = new System.Drawing.Size(861, 335);
            this.driverLicenceInfo_CTRL.TabIndex = 19;
            // 
            // DriverLicenceInfoWithFiliter_CTRL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.driverLicenceInfo_CTRL);
            this.Controls.Add(this.gbFilters);
            this.Name = "DriverLicenceInfoWithFiliter_CTRL";
            this.Size = new System.Drawing.Size(870, 414);
            this.Load += new System.EventHandler(this.DriverLicenceInfoWithFiliter_CTRL_Load);
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.Button SearchLicenseBtn;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label1;
        private DriverLicenceInfo_CTRL driverLicenceInfo_CTRL;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
