using System;
using DVLD.Classes;
using System.Windows.Forms;
using LicenseBusinessLayer;
using System.ComponentModel;
using DVLD_Project.Global_Classes;
using DetainedLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Licences.Detain_License
{
    public partial class DetainLicenseApplicationForm : Form
    {
        private clsLicense _TargetLicense = new clsLicense();
        private clsDetainedLicense _DetainedLicense = new clsDetainedLicense();

        public DetainLicenseApplicationForm()
        {
            InitializeComponent();
        }



        private void _ResetDefaultValues()
        {
            lblDetainDate.Text = "[??/??/????]";
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            txtFineFees.Text = string.Empty;
            lblCreatedByUser.Text = "[????]";

            gpDetain.Enabled = false;
            DetainBtn.Enabled = false;
            ShowLicensesHistoryBtn.Enabled = false;
            ShowLicensesInfoBtn.Enabled = false;

        }

        private void driverLicenceInfoWithFiliter_CTRL1_Load(object sender, EventArgs e)
        {
            driverLicenceInfoWithFiliter_CTRL.getLicenseID += LoadData;
        }

        private void _LoadNewDetainAppData()
        {

            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            gpDetain.Enabled = true;
            ShowLicensesInfoBtn.Enabled = false;
            lblLicenseID.Text = _TargetLicense.ID.ToString();


        }

        private void LoadData(object sender, int LicenseID)
        {
            _TargetLicense = clsLicense.GetLicenseWithID(LicenseID);

            if (_TargetLicense == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find this license with ID = " + LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ShowLicensesHistoryBtn.Enabled = true;

            //// Check If License Is still not expired 
            if (_TargetLicense.IsLicenseExpired())
            {
                _ResetDefaultValues();
                MessageBox.Show("You can't detain this license because it has expired.", "Expired License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadNewDetainAppData();


            ///// check if Driver's License is Already Detained 
            if (clsDetainedLicense.IsLicenseDetained(_TargetLicense.ID))
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is already detained, so you can't detain it again.", "Already Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DetainBtn.Enabled = true;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowNewLicensesInfoBtn_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm(_TargetLicense.ID);
            showLicenseInfoForm.ShowDialog();
        }

        private void ShowLicensesHistoryBtn_Click(object sender, EventArgs e)
        {
            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm(_TargetLicense.DriverID);
            showPersonLicenceHistoryForm.ShowDialog();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true; // Cancel the input if it's not a digit
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
        }

        private void _FillDetainedLicenseInfo()
        {
            _DetainedLicense.LicenseID = _TargetLicense.ID;
            _DetainedLicense.DetainDate = DateTime.Now;
            _DetainedLicense.FineFees = Convert.ToSingle(txtFineFees.Text);
            _DetainedLicense.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void DetainBtn_Click(object sender, EventArgs e)
        {

            /////// First Check if Fine Fee is Empty also Make sure to make user enter only Numbers

            if (!ValidateChildren())
            {
                MessageBox.Show("There are some invalid or empty fields that are required.", "Invalid Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm Detaining", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            _FillDetainedLicenseInfo();

            if (_DetainedLicense.Save())
                MessageBox.Show("License has been detainded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
                MessageBox.Show("Can't detain this license ", "Faild To Detain", MessageBoxButtons.OK, MessageBoxIcon.Error);




            // Update UI
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            driverLicenceInfoWithFiliter_CTRL.EnableDetaindFiled();
            txtFineFees.Enabled = false;
            ShowLicensesInfoBtn.Enabled = true;
            DetainBtn.Enabled = false;


        }

    }
}
