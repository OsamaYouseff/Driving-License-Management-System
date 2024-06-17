using System;
using DVLD.Classes;
using LicenseBusinessLayer;
using System.Windows.Forms;
using ApplicationBusinessLayer;
using DVLD_Project.Global_Classes;
using DVLD_Project.Forms.Licences;
using DetainedLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Applications.ReplaceLostedOrDamagedLicense
{
    public partial class ReplaceLostOrDamagedLicenseForm : Form
    {
        private clsLicense _OldLicense = new clsLicense(), _NewLicense = new clsLicense();
        private clsApplication _NewApplication = new clsApplication(), _OldApplication = new clsApplication();
        float ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.ReplacementforaLostDrivingLicense).ApplicationFees;

        public ReplaceLostOrDamagedLicenseForm()
        {
            InitializeComponent();
        }


        private void _ResetDefaultValues()
        {

            lblApplicationDate.Text = "[??/??/????]";
            lblApplicationFees.Text = "[$$$]";
            lblOldLicenseID.Text = "[???]";
            lblRreplacedLicenseID.Text = "[???]";
            lblCreatedByUser.Text = "[????]";
            lblRreplacedLicenseID.Text = "[???]";
            this.Text = "License Replacement";


            gpApplicationInfo.Enabled = false;
            ReplaceBtn.Enabled = false;
            ShowLicensesHistoryBtn.Enabled = false;
            ShowNewLicensesInfoBtn.Enabled = false;
            gbReplacementFor.Enabled = false;



        }

        private void _LoadNewAppData()
        {
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblOldLicenseID.Text = _OldLicense.ID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            gpApplicationInfo.Enabled = true;
            ShowNewLicensesInfoBtn.Enabled = false;

        }

        private void ReplaceLostOrDamagedLicenseForm_Load(object sender, System.EventArgs e)
        {
            driverLicenceInfoWithFiliter_CTRL.getLicenseID += LoadData;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.ReplacementforaLostDrivingLicense).ApplicationFees;
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense).ApplicationFees;
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
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


            if (rbDamagedLicense.Checked)
                _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense;
            else
                _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplacementforaLostDrivingLicense;

            _NewApplication.Mode = clsApplication.enMode.AddNew;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void _FillNewLicenseData()
        {
            _NewLicense.ApplicationID = _NewApplication.ApplicationID;
            _NewLicense.DriverID = _OldLicense.DriverID;
            _NewLicense.LicenseClass = _OldLicense.LicenseClass;
            _NewLicense.IssueDate = _OldLicense.IssueDate;
            _NewLicense.ExpirationDate = _OldLicense.ExpirationDate;
            _NewLicense.Notes = _OldLicense.Notes;
            _NewLicense.PaidFees = _OldLicense.PaidFees;
            _NewLicense.IsActive = true;

            if (rbDamagedLicense.Checked)
                _NewLicense.IssueReason = clsLicense.enIssueReason.DamagedReplacement;
            else
                _NewLicense.IssueReason = clsLicense.enIssueReason.LostReplacement;

            _NewLicense.CreatedByUserID = clsGlobal.CurrentUser.ID;
        }

        private void ReplaceBtn_Click(object sender, EventArgs e)
        {

            ///// reset info in new license obj if there any new info
            _NewLicense = new clsLicense();
            


            ///// Check If License is Not Active Refuse the operation 
            if (!_OldLicense.IsLicenseActive())
            {
                MessageBox.Show("The selected license is not active. Please choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                ShowLicensesHistoryBtn.Enabled = true;
                return;
            }


            if (MessageBox.Show("Are you sure you want to issue a replacement for this license?", "Confirm replacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _FillNewApplicationData();

            if (!_NewApplication.Save())
            {
                MessageBox.Show("The replacement application failed to be added", "Failed to add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillNewLicenseData();


            if (_NewLicense.Save())
            {
                MessageBox.Show(_OldLicense.LicenseClassInfo.ClassName + " License has been replaced successfully for this driver.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _NewApplication.SetCompleted();
                _OldLicense.Deactivate();

            }
            else
            {
                MessageBox.Show("Unable to replace the license for this driver.", "Failed to replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblApplicationID.Text = _NewApplication.ApplicationID.ToString();
            lblRreplacedLicenseID.Text = _NewLicense.ID.ToString();


            ShowNewLicensesInfoBtn.Enabled = true;
            ReplaceBtn.Enabled = false;
            gbReplacementFor.Enabled = false;

            driverLicenceInfoWithFiliter_CTRL.DisableSearchBtn();

            ///// Refresh Old_License_Is_Active Label 
            driverLicenceInfoWithFiliter_CTRL.DeactivateOldLicenseLabel();


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

            /////////

            if (clsDetainedLicense.IsLicenseDetained(_OldLicense.ID))
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is detaind you have to release it first", "Detaind license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLicensesHistoryBtn.Enabled = true;
                return;
            }


            ///// check if Driver's License is Not Active 
            if (!_OldLicense.IsLicenseActive())
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is not active you can't replace it.", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ///// check if Driver's License is Expired
            if (_OldLicense.IsLicenseExpired())
            {
                _ResetDefaultValues();
                MessageBox.Show("This license is expired you can't replace an expired license.", "expired License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ////////


            _OldApplication = clsApplication.GetApplicationInfoByID(_OldLicense.ApplicationID);

            if (_OldApplication == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Could not find the Local Driving License Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Update UI
            _LoadNewAppData();
            ShowLicensesHistoryBtn.Enabled = true;
            ReplaceBtn.Enabled = true;
            gbReplacementFor.Enabled = true;


        }

    }
}
