namespace DVLD_Project.Controls.People
{
    partial class PersonCardPlusFiliter_CTRL
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
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.SearchPerson_Btn = new System.Windows.Forms.Button();
            this.AddPerson_Btn = new System.Windows.Forms.Button();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PersonInfo_Card = new DVLD_Project.Controls.People.PersonCard_CTRL();
            this.gbFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.SearchPerson_Btn);
            this.gbFilters.Controls.Add(this.AddPerson_Btn);
            this.gbFilters.Controls.Add(this.cbFilterBy);
            this.gbFilters.Controls.Add(this.txtFilterValue);
            this.gbFilters.Controls.Add(this.label1);
            this.gbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilters.Location = new System.Drawing.Point(19, 18);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(894, 77);
            this.gbFilters.TabIndex = 17;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Filter";
            // 
            // SearchPerson_Btn
            // 
            this.SearchPerson_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchPerson_Btn.Enabled = false;
            this.SearchPerson_Btn.Image = global::DVLD_Project.Properties.Resources.icons8_find_user_male_482;
            this.SearchPerson_Btn.Location = new System.Drawing.Point(534, 14);
            this.SearchPerson_Btn.Name = "SearchPerson_Btn";
            this.SearchPerson_Btn.Size = new System.Drawing.Size(57, 54);
            this.SearchPerson_Btn.TabIndex = 132;
            this.SearchPerson_Btn.UseVisualStyleBackColor = true;
            this.SearchPerson_Btn.Click += new System.EventHandler(this.SearchPerson_Btn_Click);
            // 
            // AddPerson_Btn
            // 
            this.AddPerson_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddPerson_Btn.Image = global::DVLD_Project.Properties.Resources.icons8_add_user_male_48;
            this.AddPerson_Btn.Location = new System.Drawing.Point(607, 14);
            this.AddPerson_Btn.Name = "AddPerson_Btn";
            this.AddPerson_Btn.Size = new System.Drawing.Size(57, 54);
            this.AddPerson_Btn.TabIndex = 131;
            this.AddPerson_Btn.UseVisualStyleBackColor = true;
            this.AddPerson_Btn.Click += new System.EventHandler(this.AddPerson_Btn_Click);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "National No",
            "Person ID"});
            this.cbFilterBy.Location = new System.Drawing.Point(96, 25);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(210, 26);
            this.cbFilterBy.TabIndex = 16;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Location = new System.Drawing.Point(313, 26);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(214, 24);
            this.txtFilterValue.TabIndex = 17;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Find By:";
            // 
            // PersonInfo_Card
            // 
            this.PersonInfo_Card.Location = new System.Drawing.Point(9, 101);
            this.PersonInfo_Card.Name = "PersonInfo_Card";
            this.PersonInfo_Card.Size = new System.Drawing.Size(918, 304);
            this.PersonInfo_Card.TabIndex = 0;
            // 
            // PersonCardPlusFiliter_CTRL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilters);
            this.Controls.Add(this.PersonInfo_Card);
            this.Name = "PersonCardPlusFiliter_CTRL";
            this.Size = new System.Drawing.Size(931, 415);
            this.Load += new System.EventHandler(this.PersonCardPlusFiliter_CTRL_Load);
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PersonCard_CTRL PersonInfo_Card;
        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddPerson_Btn;
        private System.Windows.Forms.Button SearchPerson_Btn;
    }
}
