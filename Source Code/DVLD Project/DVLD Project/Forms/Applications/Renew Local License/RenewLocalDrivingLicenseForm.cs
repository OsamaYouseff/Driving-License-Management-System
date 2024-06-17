using System;
using DVLD.Classes;
using System.Windows.Forms;
using LicenseBusinessLayer;
using ApplicationBusinessLayer;
using LicenseClassBusinessLayer;
using DVLD_Project.Global_Classes;
using DVLD_Project.Forms.Licences;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Applications.Renew_Local_License
{
    public partial class RenewLocalDrivingLicenseForm : Form
    {

        private clsApplication _NewApplication = new clsApplication(), _OldApplication = new clsApplication();
        private clsLicense _OldLicense = new clsLicense(), _NewLicense = new clsLicense();
        private int _VadilityPeriod = 0;
        private float ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.RenewDrivingLicenseService).ApplicationFees;

        public RenewLocalDrivingLicenseForm()
        {
            InitializeComponent();
        }


        private void _ResetDefaultValues()
        {
            lblApplicationFees.Text = "[$$$]";
            lblLicenseFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";
            lblApplicationID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblRenewedLicenseID.Text = "[???]";
            lblApplicationDate.Text = "[??/??/????]";
            lblIssueDate.Text = "[??/??/????]";
            lblExpirationDate.Text = "[??/??/????]";
            txtNotes.Text = string.Empty;
            lblCreatedByUser.Text = "[????]";

            gpApplicationInfo.Enabled = false;
            RenewBtn.Enabled = false;
            ShowLicensesHistoryBtn.Enabled = false;
            ShowNewLicensesInfoBtn.Enabled = false;

        }

        private void _LoadNewAppData()
        {
            _VadilityPeriod = clsLicenseClass.GetALicenseClassByID(_OldLicense.LicenseClass).DefaultValidityLength;


            lblApplicationFees.Text = ApplicationFees.ToString();
            lblLicenseFees.Text = _OldLicense.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (_OldLicense.LicenseClassInfo.ClassFees + ApplicationFees).ToString();
            lblOldLicenseID.Text = _OldLicense.ID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(_VadilityPeriod));
            txtNotes.Text = _OldLicense.Notes == "" ? "No Notes" : _OldLicense.Notes;
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            gpApplicationInfo.Enabled = true;
            ShowNewLicensesInfoBtn.Enabled = false;

        }

        private void ShowLicensesHistoryBtn_Click(object sender, EventArgs e)
        {
            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm(_OldLicense.DriverID);
            showPersonLicenceHistoryForm.ShowDialog();
        }

        private void ShowNewLicensesInfoBtn_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm(_NewLicense.ID);
            showLicenseInfoForm.ShowDialog();

        }

        private void _FillNewApplicationData()
        {
            _NewApplication.ApplicantPersonID = _OldApplication.ApplicantPersonID;
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = ApplicationFees;
            _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService;
            _NewApplication.Mode = clsApplication.enMode.AddNew;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void _FillNewLicenseData()
        {
            _NewLicense.DriverID = _OldLicense.DriverID;
            _NewLicense.ApplicationID = _NewApplication.ApplicationID;
            _NewLicense.LicenseClass = _OldLicense.LicenseClass;
            _NewLicense.IssueDate = DateTime.Now;
            _NewLicense.ExpirationDate = DateTime.Now.AddYears(_VadilityPeriod);
            _NewLicense.IsActive = true;
            _NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            _NewLicense.CreatedByUserID = clsGlobal.CurrentUser.ID;
            _NewLicense.PaidFees = _OldLicense.PaidFees;
            _NewLicense.Notes = _OldLicense.Notes;
        }

        private void RenewBtn_Click(object sender, EventArgs e)
        {
            int ValidLicenseID = -1;
            bool IsActive = false;

            /*
                important Note You Have To Make Sure That Driver Doesn't Have An Active One (Not Expierd Yet) With The same Type 
                It Doesn't matter if it's active or not
            */

            if (clsLicense.DoesDriverHasAValidLicenseWithThisLicenseClass(_OldLicense.DriverID, _OldLicense.LicenseClass, ref ValidLicenseID, ref IsActive))
            {
                if (IsActive)
                    MessageBox.Show("This driver already holds a valid license with the provided ID = " + ValidLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("This driver already holds a valid license with the provided ID = " + ValidLicenseID + " But It's Not Active You Can Active It But You Can't Renew It Untill You Reach Expiration Date ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _ResetDefaultValues();
                ShowLicensesHistoryBtn.Enabled = true;
                return;
            }


            //// If Person Wants to Cancel
            if (MessageBox.Show("Are you sure that you want to renew this licence?? ", "Confirm Renew", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;



            _FillNewApplicationData();

            if (!_NewApplication.Save())
            {
                MessageBox.Show("The renewal application failed to be added.", "Failed to renew", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillNewLicenseData();

            if (_NewLicense.Save())
            {
                MessageBox.Show(_OldLicense.LicenseClassInfo.ClassName + " license has been renewed successfully for this driver.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _NewApplication.SetCompleted();
                _OldLicense.Deactivate();
            }
            else
            {
                MessageBox.Show("Unable to renew the license for this driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Update UI
            lblApplicationID.Text = _NewApplication.ApplicationID.ToString();
            lblRenewedLicenseID.Text = _NewLicense.ID.ToString();
            ShowNewLicensesInfoBtn.Enabled = true;
            txtNotes.Enabled = false;
            RenewBtn.Enabled = false;
            driverLicenceInfoWithFiliter_CTRL.DisableSearchBtn();


        }

        private void driverLicenceInfoWithFiliter_CTRL_Load(object sender, EventArgs e)
        {
            driverLicenceInfoWithFiliter_CTRL.getLicenseID += LoadData;
        }

        private void LoadData(object sender, int LicenseID)
        {
            _OldLicense = clsLicense.GetLicenseWithID(LicenseID);

            if (_OldLicense == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find this License with ID = " + LicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _OldApplication = clsApplication.GetApplicationInfoByID(_OldLicense.ApplicationID);

            if (_OldApplication == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find the Local Driving License Application ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ShowLicensesHistoryBtn.Enabled = true;


            ///// check if Driver's License is Not Active 
            if (!_OldLicense.IsLicenseActive())
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is not active you can't replace it.", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //// Check If License Is still not expired 
            if (!_OldLicense.IsLicenseExpired())
            {
                MessageBox.Show("This License Is Still Valid Untill " + clsFormat.DateToShort2(_OldLicense.ExpirationDate) + " You Can't Renew It Now ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadNewAppData();

            RenewBtn.Enabled = true;


        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
