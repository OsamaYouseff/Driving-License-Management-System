using System;
using System.Windows.Forms;
using System.ComponentModel;
using TestTypesBusinessLayer;

namespace DVLD_Project.Forms.Test.Test_Types
{
    public partial class EditTestTypeForm : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private static int _targetAppTypeID = -1;



        private clsTestType _TestType = new clsTestType(), _BackupTestType = new clsTestType();
        public EditTestTypeForm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public EditTestTypeForm(int targetAppTypeID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _targetAppTypeID = targetAppTypeID;

            _LoadData();

        }



        private void _fillData()
        {
            _TestType.TestID = int.Parse(lblTestTypeID.Text.Trim());
            _TestType.TestName = txtTitle.Text;
            _TestType.TestDescription = txtDescription.Text;
            _TestType.TestFees = Convert.ToSingle(txtFees.Text.Trim());
        }

        private void _LoadData()
        {
            _TestType = clsTestType.GetTestType(_targetAppTypeID);

            if (_TestType == null)
            {
                MessageBox.Show("Unable to edit this test type.", "Faild to edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblTestTypeID.Text = _TestType.TestID.ToString();
            txtTitle.Text = _TestType.TestName;
            txtDescription.Text = _TestType.TestDescription;
            txtFees.Text = _TestType.TestFees.ToString();


        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true; // Cancel the input if it's not a digit
            }
        }

        private void ValidateEmptyString(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txt_Validating(object sender, CancelEventArgs e)
        {

            ValidateEmptyString(sender, e);

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyString(sender, e);

            TextBox Temp = ((TextBox)sender);
            if (Convert.ToSingle(Temp.Text) <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Invalid Value");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }

        }

        private void txtFees_TextChanged(object sender, EventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                Temp.Text = "0";
                txtTitle.Focus();
                Temp.Focus();
            }

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("There are invalid or empty files required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _BackupTestType.CopyData(_TestType);

            _fillData();


            ///// check if there is no changes
            if (clsTestType.AreTheyDuplicated(_TestType, _BackupTestType))
            {
                MessageBox.Show("There is no change of inforamtion to save!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (_TestType.Save())
            {
                if (_Mode == enMode.AddNew)
                    MessageBox.Show("Test Type Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Test Type Updated Successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ///// take backup 
                _BackupTestType.CopyData(_TestType);

            }

        }
    }
}
