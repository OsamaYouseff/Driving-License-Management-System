using System;
using DVLD.Classes;
using LicenseBusinessLayer;
using System.Windows.Forms;
using ApplicationBusinessLayer;
using LicenseClassBusinessLayer;
using DVLD_Project.Global_Classes;
using DetainedLicenseBusinessLayer;
using InternationalLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Licences.International_Driving_Licence
{
    public partial class AddInternationalDrivingLicenceForm : Form
    {

        private clsLicense _LocalLicense = new clsLicense();
        private clsInternationalLicense _InternationalLicense = new clsInternationalLicense();
        private clsApplication _OldApplication = new clsApplication(), _NewApplication = new clsApplication();
        float ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
        private int _VadilityPeriod = 0;


        public AddInternationalDrivingLicenceForm()
        {
            InitializeComponent();
        }


        private void _ResetDefaultValues()
        {
            lblApplicationID.Text = "[???]";
            lblExpirationDate.Text = "[??/??/????]";
            lblFees.Text = "[$$$]";
            lblInternationalLicenseID.Text = "[???]";
            lblIssueDate.Text = "[??/??/????]";
            lblLocalLicenseID.Text = "[???]";
            lblApplicationDate.Text = "[??/??/????]";
            lblCreatedByUser.Text = "[????]";


            gpApplicationInfo.Enabled = false;
            ShowLicensesHistoryBtn.Enabled = false;
            ShowNewLicensesInfoBtn.Enabled = false;
            IssueBtn.Enabled = false;


        }

        private void _LoadNewAppData()
        {
            _VadilityPeriod = clsLicenseClass.GetALicenseClassByID(_LocalLicense.LicenseClass).DefaultValidityLength;


            lblFees.Text = ApplicationFees.ToString();
            lblLocalLicenseID.Text = _LocalLicense.ID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(_VadilityPeriod));
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            gpApplicationInfo.Enabled = true;
            ShowNewLicensesInfoBtn.Enabled = false;

        }

        private void ShowNewLicensesInfoBtn_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm(_InternationalLicense.ID, true);
            showLicenseInfoForm.ShowDialog();
        }

        private void ShowLicensesHistoryBtn_Click(object sender, EventArgs e)
        {
            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm(_LocalLicense.DriverID);
            showPersonLicenceHistoryForm.ShowDialog();
        }

        private void AddInternationalDrivingLicenceForm_Load(object sender, EventArgs e)
        {
            driverLicenceInfoWithFiliter_CTRL.getLicenseID += LoadData;
        }

        private void LoadData(object sender, int LicenseID)
        {
            _LocalLicense = clsLicense.GetLicenseWithID(LicenseID);

            if (_LocalLicense == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find this License with ID = " + LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /////////

            int FoundInternationalLicenseID = -1;

            if (clsInternationalLicense.IsInternationalLicenseExistWithLocalLicenseIDActive(_LocalLicense.ID, ref FoundInternationalLicenseID))
            {
                MessageBox.Show("This driver already possesses an active international license with the provided ID = [ " + FoundInternationalLicenseID + " ]", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                ShowLicensesHistoryBtn.Enabled = true;
                return;
            }

            /////////

            if (!_LocalLicense.IsLicenseActive())
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is not active you can't use it to issue an international license", "Inactive license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLicensesHistoryBtn.Enabled = true;
                return;
            }

            /////////

            if (clsDetainedLicense.IsLicenseDetained(_LocalLicense.ID))
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is detaind you have to release it first", "Detaind license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLicensesHistoryBtn.Enabled = true;
                return;
            }

            /////////

            _OldApplication = clsApplication.GetApplicationInfoByID(_LocalLicense.ApplicationID);

            if (_OldApplication == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find the Local Driving License Application ", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _LoadNewAppData();

            ShowLicensesHistoryBtn.Enabled = true;
            IssueBtn.Enabled = true;

            if (_LocalLicense.LicenseClass != 3)
            {
                _ResetDefaultValues();
                MessageBox.Show("The license class is not suitable for this operation. You must have a [ Class 3 - Ordinary driving license ].", "Unsuitable License Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private void _FillNewApplicationData()
        {
            _NewApplication.ApplicantPersonID = _OldApplication.ApplicantPersonID;
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = ApplicationFees;
            _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            _NewApplication.Mode = clsApplication.enMode.AddNew;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void _FillNewLicenseData()
        {

            _InternationalLicense.ApplicationID = _NewApplication.ApplicationID;
            _InternationalLicense.DriverID = _LocalLicense.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = _LocalLicense.ID;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(_VadilityPeriod);
            _InternationalLicense.IsActive = true;
            _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void IssueBtn_Click(object sender, EventArgs e)
        {

            // Check if the local license is active
            if (!_LocalLicense.IsLicenseActive())
            {
                MessageBox.Show("The selected license is not active. Please choose an active license.", "License not permitted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            //////

            if (MessageBox.Show("Are you sure you want to issue a replacement for this license?", "Confirm replacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            //////

            _FillNewApplicationData();

            if (!_NewApplication.Save())
            {
                MessageBox.Show("Failed to add this renewal application.", "Failed to add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //////

            _FillNewLicenseData();

            if (_InternationalLicense.Save())
            {
                MessageBox.Show(_LocalLicense.LicenseClassInfo.ClassName + "International license has been issued successfully for this driver.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _NewApplication.SetCompleted();
            }
            else
            {
                MessageBox.Show("Can't issue the international license for this driver.", "Failed to issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Update UI
            lblApplicationID.Text = _NewApplication.ApplicationID.ToString();
            lblInternationalLicenseID.Text = _InternationalLicense.ID.ToString();
            ShowNewLicensesInfoBtn.Enabled = true;
            IssueBtn.Enabled = false;
            driverLicenceInfoWithFiliter_CTRL.DisableSearchBtn();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
