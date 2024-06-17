using System;
using System.Windows.Forms;

namespace DVLD_Project.Forms.Tests.Test_Types
{
    public partial class ShowTestTypeInfoForm : Form
    {

        public ShowTestTypeInfoForm()
        {
            InitializeComponent();
        }
        public ShowTestTypeInfoForm(string Title, string Description, float Fees)
        {
            InitializeComponent();

            txtTitle.Text = Title;
            txtDescription.Text = Description;
            txtFees.Text = Fees.ToString();
        }


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
