namespace DVLD_Project.Controls.General
{
    partial class CloseBtn_CTRL
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
            this.Close_Btn = new System.Windows.Forms.GroupBox();
            this.Close_lbl = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Close_Btn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // Close_Btn
            // 
            this.Close_Btn.Controls.Add(this.Close_lbl);
            this.Close_Btn.Controls.Add(this.pictureBox3);
            this.Close_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Close_Btn.Location = new System.Drawing.Point(0, 3);
            this.Close_Btn.Name = "Close_Btn";
            this.Close_Btn.Size = new System.Drawing.Size(67, 53);
            this.Close_Btn.TabIndex = 5;
            this.Close_Btn.TabStop = false;
            // 
            // Close_lbl
            // 
            this.Close_lbl.AutoSize = true;
            this.Close_lbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close_lbl.Location = new System.Drawing.Point(8, 32);
            this.Close_lbl.Name = "Close_lbl";
            this.Close_lbl.Size = new System.Drawing.Size(49, 19);
            this.Close_lbl.TabIndex = 5;
            this.Close_lbl.Text = "Close";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::DVLD_Project.Properties.Resources.icons8_remove_94;
            this.pictureBox3.Location = new System.Drawing.Point(18, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // CloseBtn_CTRL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Close_Btn);
            this.Name = "CloseBtn_CTRL";
            this.Size = new System.Drawing.Size(67, 57);
            this.Close_Btn.ResumeLayout(false);
            this.Close_Btn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Close_Btn;
        private System.Windows.Forms.Label Close_lbl;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
