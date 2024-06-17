using System;
using System.Windows.Forms;
using System.ComponentModel;
using ApplicationBusinessLayer;

namespace DVLD_Project.Forms.ApplicationTypes
{
    public partial class EditApplicationTypesForm : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private static int _targetAppTypeID = -1;
        private clsApplicationTypes _ApplicationType = new clsApplicationTypes(), _BackupApplicationType = new clsApplicationTypes();


        public EditApplicationTypesForm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public EditApplicationTypesForm(int targetAppTypeID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _targetAppTypeID = targetAppTypeID;

            _LoadData();

        }




        private void _fillData()
        {
            _ApplicationType.ApplicationID = int.Parse(lblApplicationTypeID.Text.Trim());
            _ApplicationType.ApplicationName = txtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text.Trim());
        }

        private void _LoadData()
        {
            _ApplicationType = clsApplicationTypes.GetApplicationType(_targetAppTypeID);

            if (_ApplicationType == null)
            {
                MessageBox.Show("Faild to edit this Application Type", "Edit failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblApplicationTypeID.Text = _ApplicationType.ApplicationID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationName;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();


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

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
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
                MessageBox.Show("There is some invalid or empty fields and they are required", "Invalid or empty fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _BackupApplicationType.CopyData(_ApplicationType);

            _fillData();


            ///// check if there is no changes
            if (clsApplicationTypes.AreTheyDuplicated(_ApplicationType, _BackupApplicationType))
            {
                MessageBox.Show("There is no change of inforamtion to Save !! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (_ApplicationType.Save())
            {
                if (_Mode == enMode.AddNew)
                    MessageBox.Show("Application Type Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Application Type Updated Successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ///// take backup 
                _BackupApplicationType.CopyData(_ApplicationType);

            }

        }
    }

}
