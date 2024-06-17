namespace DVLD_Project.Forms.Users
{
    partial class UsersListScreenForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersListScreenForm));
            this.UsersGridView = new System.Windows.Forms.DataGridView();
            this.cmsUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewUsertoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangePassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PhoneCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterMenu = new System.Windows.Forms.ComboBox();
            this.Search_Input = new System.Windows.Forms.TextBox();
            this.filterby_lbl = new System.Windows.Forms.Label();
            this.People_records_num = new System.Windows.Forms.Label();
            this.Records__lbl = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.ComboBox();
            this.Users_records_num = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.AddNewUser_Btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridView)).BeginInit();
            this.cmsUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // UsersGridView
            // 
            this.UsersGridView.AllowUserToAddRows = false;
            this.UsersGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SF Pro Rounded", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsersGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.UsersGridView.BackgroundColor = System.Drawing.Color.White;
            this.UsersGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UsersGridView.CausesValidation = false;
            this.UsersGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.UsersGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SF Pro Rounded", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UsersGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.UsersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UsersGridView.ContextMenuStrip = this.cmsUsers;
            this.UsersGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SF Pro Rounded", 14.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UsersGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.UsersGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.UsersGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.UsersGridView.Location = new System.Drawing.Point(26, 267);
            this.UsersGridView.Name = "UsersGridView";
            this.UsersGridView.ReadOnly = true;
            this.UsersGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SF Pro Rounded", 14.25F, System.Drawing.FontStyle.Bold);
            this.UsersGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.UsersGridView.ShowCellErrors = false;
            this.UsersGridView.ShowCellToolTips = false;
            this.UsersGridView.ShowEditingIcon = false;
            this.UsersGridView.ShowRowErrors = false;
            this.UsersGridView.Size = new System.Drawing.Size(1119, 377);
            this.UsersGridView.TabIndex = 118;
            // 
            // cmsUsers
            // 
            this.cmsUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem2,
            this.toolStripSeparator3,
            this.AddNewUsertoolStripMenuItem,
            this.EditPersonToolStripMenuItem,
            this.DeletePersonToolStripMenuItem,
            this.ChangePassToolStripMenuItem,
            this.toolStripSeparator4,
            this.SendEmailToolStripMenuItem,
            this.PhoneCallToolStripMenuItem});
            this.cmsUsers.Name = "contextMenuStrip1";
            this.cmsUsers.Size = new System.Drawing.Size(233, 394);
            // 
            // showDetailsToolStripMenuItem2
            // 
            this.showDetailsToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showDetailsToolStripMenuItem2.Image = global::DVLD_Project.Properties.Resources.icons8_details_48;
            this.showDetailsToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDetailsToolStripMenuItem2.Name = "showDetailsToolStripMenuItem2";
            this.showDetailsToolStripMenuItem2.Size = new System.Drawing.Size(232, 54);
            this.showDetailsToolStripMenuItem2.Text = "&Show Details";
            this.showDetailsToolStripMenuItem2.Click += new System.EventHandler(this.showDetailsToolStripMenuItem2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // AddNewUsertoolStripMenuItem
            // 
            this.AddNewUsertoolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewUsertoolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_add_user_male_48;
            this.AddNewUsertoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AddNewUsertoolStripMenuItem.Name = "AddNewUsertoolStripMenuItem";
            this.AddNewUsertoolStripMenuItem.Size = new System.Drawing.Size(232, 54);
            this.AddNewUsertoolStripMenuItem.Text = "Add &New User";
            this.AddNewUsertoolStripMenuItem.Click += new System.EventHandler(this.AddNewUsertoolStripMenuItem_Click);
            // 
            // EditPersonToolStripMenuItem
            // 
            this.EditPersonToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditPersonToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_edit_profile_skin_type_7_481;
            this.EditPersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditPersonToolStripMenuItem.Name = "EditPersonToolStripMenuItem";
            this.EditPersonToolStripMenuItem.Size = new System.Drawing.Size(232, 54);
            this.EditPersonToolStripMenuItem.Text = "&Edit User";
            this.EditPersonToolStripMenuItem.Click += new System.EventHandler(this.EditPersonToolStripMenuItem_Click);
            // 
            // DeletePersonToolStripMenuItem
            // 
            this.DeletePersonToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeletePersonToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_remove_48;
            this.DeletePersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeletePersonToolStripMenuItem.Name = "DeletePersonToolStripMenuItem";
            this.DeletePersonToolStripMenuItem.Size = new System.Drawing.Size(232, 54);
            this.DeletePersonToolStripMenuItem.Text = "&Delete User";
            this.DeletePersonToolStripMenuItem.Click += new System.EventHandler(this.DeletePersonToolStripMenuItem_Click);
            // 
            // ChangePassToolStripMenuItem
            // 
            this.ChangePassToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangePassToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_lock_481;
            this.ChangePassToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ChangePassToolStripMenuItem.Name = "ChangePassToolStripMenuItem";
            this.ChangePassToolStripMenuItem.Size = new System.Drawing.Size(232, 54);
            this.ChangePassToolStripMenuItem.Text = "Change Password";
            this.ChangePassToolStripMenuItem.Click += new System.EventHandler(this.ChangePassToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(229, 6);
            // 
            // SendEmailToolStripMenuItem
            // 
            this.SendEmailToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendEmailToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_message_48;
            this.SendEmailToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SendEmailToolStripMenuItem.Name = "SendEmailToolStripMenuItem";
            this.SendEmailToolStripMenuItem.Size = new System.Drawing.Size(232, 54);
            this.SendEmailToolStripMenuItem.Text = "Send E&mail";
            this.SendEmailToolStripMenuItem.Click += new System.EventHandler(this.SendEmailToolStripMenuItem_Click);
            // 
            // PhoneCallToolStripMenuItem
            // 
            this.PhoneCallToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhoneCallToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.icons8_call_48;
            this.PhoneCallToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PhoneCallToolStripMenuItem.Name = "PhoneCallToolStripMenuItem";
            this.PhoneCallToolStripMenuItem.Size = new System.Drawing.Size(232, 54);
            this.PhoneCallToolStripMenuItem.Text = "Phone &Call";
            this.PhoneCallToolStripMenuItem.Click += new System.EventHandler(this.PhoneCallToolStripMenuItem_Click);
            // 
            // FilterMenu
            // 
            this.FilterMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FilterMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterMenu.FormattingEnabled = true;
            this.FilterMenu.Items.AddRange(new object[] {
            "None",
            "User ID",
            "UserName",
            "Person ID",
            "Full Name",
            "Is Active"});
            this.FilterMenu.Location = new System.Drawing.Point(134, 229);
            this.FilterMenu.Name = "FilterMenu";
            this.FilterMenu.Size = new System.Drawing.Size(150, 28);
            this.FilterMenu.TabIndex = 124;
            this.FilterMenu.SelectedIndexChanged += new System.EventHandler(this.FilterMenu_SelectedIndexChanged);
            // 
            // Search_Input
            // 
            this.Search_Input.Font = new System.Drawing.Font("SF Pro Rounded", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search_Input.Location = new System.Drawing.Point(299, 229);
            this.Search_Input.Name = "Search_Input";
            this.Search_Input.Size = new System.Drawing.Size(266, 27);
            this.Search_Input.TabIndex = 123;
            this.Search_Input.Visible = false;
            this.Search_Input.TextChanged += new System.EventHandler(this.Search_Input_TextChanged);
            this.Search_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Search_Input_KeyPress);
            // 
            // filterby_lbl
            // 
            this.filterby_lbl.AutoSize = true;
            this.filterby_lbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterby_lbl.Location = new System.Drawing.Point(25, 230);
            this.filterby_lbl.Name = "filterby_lbl";
            this.filterby_lbl.Size = new System.Drawing.Size(103, 26);
            this.filterby_lbl.TabIndex = 122;
            this.filterby_lbl.Text = "Filter By :";
            // 
            // People_records_num
            // 
            this.People_records_num.AutoSize = true;
            this.People_records_num.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.People_records_num.Location = new System.Drawing.Point(106, 627);
            this.People_records_num.Name = "People_records_num";
            this.People_records_num.Size = new System.Drawing.Size(17, 19);
            this.People_records_num.TabIndex = 121;
            this.People_records_num.Text = "0";
            // 
            // Records__lbl
            // 
            this.Records__lbl.AutoSize = true;
            this.Records__lbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Records__lbl.Location = new System.Drawing.Point(26, 626);
            this.Records__lbl.Name = "Records__lbl";
            this.Records__lbl.Size = new System.Drawing.Size(83, 19);
            this.Records__lbl.TabIndex = 120;
            this.Records__lbl.Text = "# Records :";
            // 
            // cbIsActive
            // 
            this.cbIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsActive.FormattingEnabled = true;
            this.cbIsActive.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsActive.Location = new System.Drawing.Point(299, 229);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(121, 26);
            this.cbIsActive.TabIndex = 126;
            this.cbIsActive.Visible = false;
            this.cbIsActive.SelectedIndexChanged += new System.EventHandler(this.cbIsActive_SelectedIndexChanged);
            // 
            // Users_records_num
            // 
            this.Users_records_num.AutoSize = true;
            this.Users_records_num.Font = new System.Drawing.Font("SF Pro Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Users_records_num.Location = new System.Drawing.Point(124, 647);
            this.Users_records_num.Name = "Users_records_num";
            this.Users_records_num.Size = new System.Drawing.Size(20, 19);
            this.Users_records_num.TabIndex = 128;
            this.Users_records_num.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SF Pro Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 647);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 19);
            this.label2.TabIndex = 127;
            this.label2.Text = "# Records :";
            // 
            // CloseBtn
            // 
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBtn.Font = new System.Drawing.Font("SF Pro Rounded", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Image = global::DVLD_Project.Properties.Resources.icons8_close_24;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(532, 661);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(108, 53);
            this.CloseBtn.TabIndex = 125;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // AddNewUser_Btn
            // 
            this.AddNewUser_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddNewUser_Btn.Image = global::DVLD_Project.Properties.Resources.icons8_add_user_male_484;
            this.AddNewUser_Btn.Location = new System.Drawing.Point(1087, 203);
            this.AddNewUser_Btn.Name = "AddNewUser_Btn";
            this.AddNewUser_Btn.Size = new System.Drawing.Size(58, 58);
            this.AddNewUser_Btn.TabIndex = 129;
            this.AddNewUser_Btn.UseVisualStyleBackColor = true;
            this.AddNewUser_Btn.Click += new System.EventHandler(this.AddNewUser_Btn_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.male_team_illustration_23_2150201039;
            this.pictureBox1.Location = new System.Drawing.Point(487, -7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 186);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 116;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(112, 182);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(958, 44);
            this.lblTitle.TabIndex = 130;
            this.lblTitle.Text = "Manage Users ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsersListScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseBtn;
            this.ClientSize = new System.Drawing.Size(1172, 726);
            this.Controls.Add(this.AddNewUser_Btn);
            this.Controls.Add(this.Users_records_num);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.UsersGridView);
            this.Controls.Add(this.FilterMenu);
            this.Controls.Add(this.filterby_lbl);
            this.Controls.Add(this.People_records_num);
            this.Controls.Add(this.Records__lbl);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Search_Input);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UsersListScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Users List Screen";
            this.Load += new System.EventHandler(this.UsersListScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridView)).EndInit();
            this.cmsUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UsersGridView;
        private System.Windows.Forms.ComboBox FilterMenu;
        private System.Windows.Forms.TextBox Search_Input;
        private System.Windows.Forms.Label filterby_lbl;
        private System.Windows.Forms.Label People_records_num;
        private System.Windows.Forms.Label Records__lbl;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbIsActive;
        private System.Windows.Forms.ContextMenuStrip cmsUsers;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem AddNewUsertoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem SendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangePassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PhoneCallToolStripMenuItem;
        private System.Windows.Forms.Label Users_records_num;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddNewUser_Btn;
        private System.Windows.Forms.Label lblTitle;
    }
}