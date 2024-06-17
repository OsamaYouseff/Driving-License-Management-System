namespace DVLD_Project.Forms
{
    partial class PeopleListScreen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeopleListScreen));
            this.PeopleGridView = new System.Windows.Forms.DataGridView();
            this.cmsPeople = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewPErsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PhoneCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Records__lbl = new System.Windows.Forms.Label();
            this.People_records_num = new System.Windows.Forms.Label();
            this.filterby_lbl = new System.Windows.Forms.Label();
            this.Search_Input = new System.Windows.Forms.TextBox();
            this.FilterMenu = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.AddNewPerson_Btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PeopleGridView)).BeginInit();
            this.cmsPeople.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PeopleGridView
            // 
            this.PeopleGridView.AllowUserToAddRows = false;
            this.PeopleGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SF Pro Rounded", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.NullValue = "N/A";
            this.PeopleGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PeopleGridView.BackgroundColor = System.Drawing.Color.White;
            this.PeopleGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PeopleGridView.CausesValidation = false;
            this.PeopleGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SF Pro Text", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.NullValue = "N/A";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PeopleGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PeopleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PeopleGridView.ContextMenuStrip = this.cmsPeople;
            this.PeopleGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SF Pro Rounded", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.NullValue = "N/A";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PeopleGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.PeopleGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PeopleGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PeopleGridView.Location = new System.Drawing.Point(17, 268);
            this.PeopleGridView.Name = "PeopleGridView";
            this.PeopleGridView.ReadOnly = true;
            this.PeopleGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.PeopleGridView.ShowCellErrors = false;
            this.PeopleGridView.ShowCellToolTips = false;
            this.PeopleGridView.ShowEditingIcon = false;
            this.PeopleGridView.ShowRowErrors = false;
            this.PeopleGridView.Size = new System.Drawing.Size(1243, 377);
            this.PeopleGridView.TabIndex = 2;
            // 
            // cmsPeople
            // 
            this.cmsPeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.toolStripSeparator2,
            this.AddNewPErsonToolStripMenuItem,
            this.EditPersonToolStripMenuItem,
            this.DeletePersonToolStripMenuItem,
            this.toolStripSeparator1,
            this.SendEmailToolStripMenuItem,
            this.PhoneCallToolStripMenuItem});
            this.cmsPeople.Name = "contextMenuStrip1";
            this.cmsPeople.Size = new System.Drawing.Size(225, 362);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showDetailsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_details_48;
            this.showDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(224, 54);
            this.showDetailsToolStripMenuItem.Text = "&Show Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(221, 6);
            // 
            // AddNewPErsonToolStripMenuItem
            // 
            this.AddNewPErsonToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewPErsonToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_add_user_male_48;
            this.AddNewPErsonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AddNewPErsonToolStripMenuItem.Name = "AddNewPErsonToolStripMenuItem";
            this.AddNewPErsonToolStripMenuItem.Size = new System.Drawing.Size(224, 54);
            this.AddNewPErsonToolStripMenuItem.Text = "Add &New Person";
            this.AddNewPErsonToolStripMenuItem.Click += new System.EventHandler(this.AddNewPerson_Click);
            // 
            // EditPersonToolStripMenuItem
            // 
            this.EditPersonToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditPersonToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_edit_profile_skin_type_7_481;
            this.EditPersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditPersonToolStripMenuItem.Name = "EditPersonToolStripMenuItem";
            this.EditPersonToolStripMenuItem.Size = new System.Drawing.Size(224, 54);
            this.EditPersonToolStripMenuItem.Text = "&Edit";
            this.EditPersonToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // DeletePersonToolStripMenuItem
            // 
            this.DeletePersonToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeletePersonToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_remove_48;
            this.DeletePersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeletePersonToolStripMenuItem.Name = "DeletePersonToolStripMenuItem";
            this.DeletePersonToolStripMenuItem.Size = new System.Drawing.Size(224, 54);
            this.DeletePersonToolStripMenuItem.Text = "&Delete";
            this.DeletePersonToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // SendEmailToolStripMenuItem
            // 
            this.SendEmailToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendEmailToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_message_48;
            this.SendEmailToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SendEmailToolStripMenuItem.Name = "SendEmailToolStripMenuItem";
            this.SendEmailToolStripMenuItem.Size = new System.Drawing.Size(224, 54);
            this.SendEmailToolStripMenuItem.Text = "Send E&mail";
            this.SendEmailToolStripMenuItem.Click += new System.EventHandler(this.sendEmailToolStripMenuItem_Click);
            // 
            // PhoneCallToolStripMenuItem
            // 
            this.PhoneCallToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhoneCallToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_call_48;
            this.PhoneCallToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PhoneCallToolStripMenuItem.Name = "PhoneCallToolStripMenuItem";
            this.PhoneCallToolStripMenuItem.Size = new System.Drawing.Size(224, 54);
            this.PhoneCallToolStripMenuItem.Text = "Phone &Call";
            this.PhoneCallToolStripMenuItem.Click += new System.EventHandler(this.phoneCallToolStripMenuItem_Click);
            // 
            // Records__lbl
            // 
            this.Records__lbl.AutoSize = true;
            this.Records__lbl.Font = new System.Drawing.Font("SF Pro Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Records__lbl.Location = new System.Drawing.Point(18, 648);
            this.Records__lbl.Name = "Records__lbl";
            this.Records__lbl.Size = new System.Drawing.Size(101, 19);
            this.Records__lbl.TabIndex = 5;
            this.Records__lbl.Text = "# Records :";
            // 
            // People_records_num
            // 
            this.People_records_num.AutoSize = true;
            this.People_records_num.Font = new System.Drawing.Font("SF Pro Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.People_records_num.Location = new System.Drawing.Point(115, 648);
            this.People_records_num.Name = "People_records_num";
            this.People_records_num.Size = new System.Drawing.Size(20, 19);
            this.People_records_num.TabIndex = 6;
            this.People_records_num.Text = "0";
            // 
            // filterby_lbl
            // 
            this.filterby_lbl.AutoSize = true;
            this.filterby_lbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterby_lbl.Location = new System.Drawing.Point(16, 231);
            this.filterby_lbl.Name = "filterby_lbl";
            this.filterby_lbl.Size = new System.Drawing.Size(103, 26);
            this.filterby_lbl.TabIndex = 7;
            this.filterby_lbl.Text = "Filter By :";
            // 
            // Search_Input
            // 
            this.Search_Input.Font = new System.Drawing.Font("SF Pro Rounded", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search_Input.Location = new System.Drawing.Point(290, 230);
            this.Search_Input.Name = "Search_Input";
            this.Search_Input.Size = new System.Drawing.Size(153, 27);
            this.Search_Input.TabIndex = 22;
            this.Search_Input.Visible = false;
            this.Search_Input.TextChanged += new System.EventHandler(this.Search_Input_TextChanged);
            this.Search_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Search_Input_KeyPress);
            // 
            // FilterMenu
            // 
            this.FilterMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterMenu.FormattingEnabled = true;
            this.FilterMenu.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No.",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gender",
            "Phone",
            "Email",
            "Address"});
            this.FilterMenu.Location = new System.Drawing.Point(125, 230);
            this.FilterMenu.Name = "FilterMenu";
            this.FilterMenu.Size = new System.Drawing.Size(150, 28);
            this.FilterMenu.TabIndex = 23;
            this.FilterMenu.SelectedIndexChanged += new System.EventHandler(this.FilterMenu_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(-4, 192);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1280, 39);
            this.lblTitle.TabIndex = 124;
            this.lblTitle.Text = "Manage People ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseBtn
            // 
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBtn.Font = new System.Drawing.Font("SF Pro Rounded", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Image = global::DVLD_Project.Properties.Resources.icons8_close_24;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(584, 663);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(108, 53);
            this.CloseBtn.TabIndex = 115;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.Close_People_Form);
            // 
            // AddNewPerson_Btn
            // 
            this.AddNewPerson_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddNewPerson_Btn.Image = global::DVLD_Project.Properties.Resources.icons8_add_user_male_483;
            this.AddNewPerson_Btn.Location = new System.Drawing.Point(1201, 206);
            this.AddNewPerson_Btn.Name = "AddNewPerson_Btn";
            this.AddNewPerson_Btn.Size = new System.Drawing.Size(59, 56);
            this.AddNewPerson_Btn.TabIndex = 125;
            this.AddNewPerson_Btn.UseVisualStyleBackColor = true;
            this.AddNewPerson_Btn.Click += new System.EventHandler(this.AddNewPerson_Btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.International_cooperation_pana;
            this.pictureBox1.Location = new System.Drawing.Point(533, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 189);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PeopleListScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseBtn;
            this.ClientSize = new System.Drawing.Size(1278, 728);
            this.Controls.Add(this.AddNewPerson_Btn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.FilterMenu);
            this.Controls.Add(this.Search_Input);
            this.Controls.Add(this.filterby_lbl);
            this.Controls.Add(this.People_records_num);
            this.Controls.Add(this.Records__lbl);
            this.Controls.Add(this.PeopleGridView);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PeopleListScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "People List Screen";
            this.Load += new System.EventHandler(this.PeopleScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PeopleGridView)).EndInit();
            this.cmsPeople.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView PeopleGridView;
        private System.Windows.Forms.Label Records__lbl;
        private System.Windows.Forms.Label People_records_num;
        private System.Windows.Forms.Label filterby_lbl;
        private System.Windows.Forms.TextBox Search_Input;
        private System.Windows.Forms.ContextMenuStrip cmsPeople;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem AddNewPErsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PhoneCallToolStripMenuItem;
        private System.Windows.Forms.ComboBox FilterMenu;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button AddNewPerson_Btn;
    }
}