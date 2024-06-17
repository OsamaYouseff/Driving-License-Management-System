using System;
using System.Windows.Forms;
using DriversBusinessLayer;
using LicenseBusinessLayer;
using InternationalLicenseBusinessLayer;

namespace DVLD_Project.Forms.Licences.Local_Driving_Licence
{
    public partial class ShowLicenseInfoForm : Form
    {

        public int LicenseID = -1;
        private clsLicense _License;
        private clsDriver _Driver;
        private clsInternationalLicense _InternationalLicense;


        public ShowLicenseInfoForm()
        {
            InitializeComponent();
        }
        public ShowLicenseInfoForm(int LicenseID, bool IsInterNational = false)
        {
            InitializeComponent();

            if (IsInterNational)
            {
                _InternationalLicense = clsInternationalLicense.GetInternationalLicenseWithID(LicenseID);
                LicenseID = _InternationalLicense.ID;

            }
            else
            {
                _License = clsLicense.GetLicenseWithID(LicenseID);
                LicenseID = _License.ID;

            }


            if (_License == null && _InternationalLicense == null)
            {
                _ResetLicenseInfo();
                MessageBox.Show("There is no license exist", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (IsInterNational)
            {
                lblTitle.Text = "Driver International License Info";
                driverLicenceInfo_CTRL1.LoadInfo(_InternationalLicense);
            }
            else
                driverLicenceInfo_CTRL1.LoadInfo(_License);


        }
        public ShowLicenseInfoForm(int ApplicationID, string LicenseClassName)
        {
            InitializeComponent();


            _License = clsLicense.GetLicenseWithApplicationIDAndLicenseClassName(ApplicationID, LicenseClassName);

            if (_License == null)
            {
                _ResetLicenseInfo();
                MessageBox.Show("There is no license exist", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            LicenseID = _License.ID;
            driverLicenceInfo_CTRL1.LoadInfo(_License.ID);

        }
        public ShowLicenseInfoForm(string NationalNo, string LicenseClassName)
        {
            InitializeComponent();

            _Driver = clsDriver.GetDriverWithNationalNo(NationalNo);

            if (_Driver == null)
            {
                MessageBox.Show("Unable to locate this driver.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            _License = clsLicense.GetLicenseWithDriverIDAndLicenseClass(_Driver.ID, LicenseClassName);

            if (_License == null)
            {
                _ResetLicenseInfo();
                MessageBox.Show("No license exists.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            LicenseID = _License.ID;
            driverLicenceInfo_CTRL1.LoadInfo(_License.ID);

        }

        private void _ResetLicenseInfo()
        {
            driverLicenceInfo_CTRL1.ResetValues();
        }
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
