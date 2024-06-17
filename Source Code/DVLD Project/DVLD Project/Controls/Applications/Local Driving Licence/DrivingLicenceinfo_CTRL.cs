using System.Windows.Forms;
using LicenseBusinessLayer;
using LocalDrivingLicenseApplicationBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Controls.Applications.Driving_Licence.Local_Driving_Licence
{
    public partial class DrivingLicenceinfo_CTRL : UserControl
    {

        private int _LocalLicenseApplicationID = -1;
        private int _ApplicationID = -1;
        private string _DrivingClassName = string.Empty;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        public DrivingLicenceinfo_CTRL()
        {
            InitializeComponent();
        }



        public void LoadData(int LocalLicenseApplicationID, int PassedTestsNum)
        {

            _LocalLicenseApplicationID = LocalLicenseApplicationID;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(LocalLicenseApplicationID);

            if (_localDrivingLicenseApplication != null)
            {

                _ApplicationID = _localDrivingLicenseApplication.ApplicationID;

                ///// Enable Or Desiable Showing License info Depending on License is exist or not
                LicenseGroupBox.Enabled = clsLicense.IsThereLicenseHasThisApplicationID(_ApplicationID);


                applicationInfo_CTRL.LoadApplicationInfo(_localDrivingLicenseApplication.ApplicationID, _localDrivingLicenseApplication.LicenseClassInfo.ClassName);


                lblLocalDrivingLicenseApplicationID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblAppliedFor.Text = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;
                lblPassedTests.Text = PassedTestsNum.ToString();


                ///// store some data 
                _DrivingClassName = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;

            }
            else
                MessageBox.Show("No Local Driving License exists with the specified ID = " + _LocalLicenseApplicationID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseInfoForm showLocalLicenseInfoForm = new ShowLicenseInfoForm(_ApplicationID, _DrivingClassName);
            showLocalLicenseInfoForm.ShowDialog();

        }

    }
}
