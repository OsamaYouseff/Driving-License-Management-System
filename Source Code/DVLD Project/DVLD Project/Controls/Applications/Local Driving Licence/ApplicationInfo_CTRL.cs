using System;
using System.Windows.Forms;
using ApplicationBusinessLayer;
using DVLD_Project.Forms.People;

namespace DVLD_Project.Controls.Applications.Driving_Licence.Local_Driving_Licence
{
    public partial class ApplicationInfo_CTRL : UserControl
    {
        private int _ApplicationID = -1;
        private string _LicenseName = string.Empty;
        clsApplication _Application;
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
        public clsApplication SelectedApplicationInfo
        {
            get { return _Application; }
        }
        public ApplicationInfo_CTRL()
        {
            InitializeComponent();
        }


        public void LoadApplicationInfo(int ApplicationID, string LicenseName)
        {

            _ApplicationID = ApplicationID;
            _Application = clsApplication.GetApplicationInfoByID(ApplicationID);
            _LicenseName = LicenseName;

            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No application exists with the specified ApplicationID = " + ApplicationID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }

        private void _FillApplicationInfo()
        {
            lblApplicant.Text = _Application.ApplicantFullName;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblType.Text = _LicenseName;
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;
            lblStatus.Text = _Application.StatusText;

        }

        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;
            lblApplicant.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblDate.Text = "[???]";
            lblFees.Text = "[???]";
            lblStatusDate.Text = "[???]";
            lblType.Text = "[???]";
            lblCreatedByUser.Text = "[???]";
            lblStatus.Text = "[???]";
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonInfoForm showPersonInfoForm = new ShowPersonInfoForm(_Application.ApplicantPersonID);
            showPersonInfoForm.ShowDialog();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            _Application = clsApplication.GetApplicationInfoByID(_ApplicationID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No application exists with the specified ApplicationID = " + ApplicationID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }
    }
}
