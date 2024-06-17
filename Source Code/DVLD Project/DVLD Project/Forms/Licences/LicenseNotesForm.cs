using System;
using System.Windows.Forms;

namespace DVLD_Project.Forms.Licences
{
    public partial class LicenseNotesForm : Form
    {

        public delegate void GetNotesBack(object sender, string Notes);
        public event GetNotesBack GetNotes;

        public LicenseNotesForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = txtNotes.Text.Trim() != string.Empty;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            GetNotes?.Invoke(this, txtNotes.Text);
            this.Close();
        }
    }
}
