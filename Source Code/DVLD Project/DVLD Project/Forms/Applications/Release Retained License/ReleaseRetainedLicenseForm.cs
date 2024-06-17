using System;
using System.Windows.Forms;
using LicenseBusinessLayer;
using ApplicationBusinessLayer;
using DVLD_Project.Forms.Licences;
using DVLD_Project.Global_Classes;
using DetainedLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Applications.Release_Retained_License
{
    public partial class ReleaseRetainedLicenseForm : Form
    {
        private clsApplication _Application = new clsApplication();
        private clsLicense _TargetLicense = new clsLicense();
        private clsDetainedLicense _DetainedLicense = new clsDetainedLicense();
        private float ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
        private int _LicenseID = -1;


        public ReleaseRetainedLicenseForm()
        {
            InitializeComponent();
        }
        public ReleaseRetainedLicenseForm(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }



        private void driverLicenceInfoWithFiliter_CTRL_Load(object sender, EventArgs e)
        {
            ///// this statement to decide if the user will find the license before the form opens or wants it Prior research depending on  _LicenseID

            if (_LicenseID == -1)
                driverLicenceInfoWithFiliter_CTRL.getLicenseID += LoadData;
            else
            {
                driverLicenceInfoWithFiliter_CTRL.LoadData(_LicenseID, true);
                LoadData(this, _LicenseID);
            }
        }

        private void _ResetDefaultValues()
        {
            lblApplicationFees.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblFineFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";
            lblDetainDate.Text = "[??/??/????]";
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblCreatedByUser.Text = "[????]";

            gpDetain.Enabled = false;
            ReleaseBtn.Enabled = false;
            ShowLicensesHistoryBtn.Enabled = false;
            ShowLicensesInfoBtn.Enabled = false;

        }

        private void _LoadNewDetainAppData()
        {

            lblApplicationFees.Text = ApplicationFees.ToString();
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (ApplicationFees + _DetainedLicense.FineFees).ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString();
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = _TargetLicense.ID.ToString();
            lblCreatedByUser.Text = _TargetLicense.CreatedByUserID.ToString();
        }

        private void LoadData(object sender, int LicenseID)
        {
            _TargetLicense = clsLicense.GetLicenseWithID(LicenseID);

            if (_TargetLicense == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find this License with ID = " + LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


            ///// check if Driver's License is Not Detained 
            if (!clsDetainedLicense.IsLicenseDetained(_TargetLicense.ID))
            {
                _ResetDefaultValues();
                MessageBox.Show("You can't release this license because it is not detained.", "Not Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            _DetainedLicense = clsDetainedLicense.GetDetainedLicenseByLicenseID(LicenseID);

            if (_DetainedLicense == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find this Detained License with ID = " + LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ////////


            // Update UI
            _LoadNewDetainAppData();

            gpDetain.Enabled = true;
            ReleaseBtn.Enabled = true;
            ShowLicensesHistoryBtn.Enabled = true;

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

        private void _FillNewApplicationData()
        {
            _Application.ApplicantPersonID = _TargetLicense.DriverInfo.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = ApplicationFees;
            _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            _Application.Mode = clsApplication.enMode.AddNew;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _Application.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void ReleaseBtn_Click(object sender, EventArgs e)
        {

            //// If Person Wants to Cancel
            if (MessageBox.Show("Are you sure that you want to Release this Licence?? ", "Confirm Renew", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            _FillNewApplicationData();

            if (!_Application.Save())
            {
                MessageBox.Show("Failed to add this renewal application.", "Failed to add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = _Application.ApplicationID.ToString();

            if (_DetainedLicense.ReleaseDetainedLicense(clsGlobal.CurrentUser.ID, _Application.ApplicationID))
            {
                MessageBox.Show("The License has been released successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Faild to release this license.", "Faild To Release", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ////// You have to Delete the application because is not completed correctly
                clsApplication.DeleteApplication(_Application.ApplicationID);

                _ResetDefaultValues();

                return;
            }

            ShowLicensesInfoBtn.Enabled = true;
            ReleaseBtn.Enabled = false;
            driverLicenceInfoWithFiliter_CTRL.DisableIsDetained();


        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
