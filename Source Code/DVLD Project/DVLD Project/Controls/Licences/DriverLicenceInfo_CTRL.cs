using System.IO;
using DVLD.Classes;
using System.Windows.Forms;
using LicenseBusinessLayer;
using DVLD_Project.Properties;
using DetainedLicenseBusinessLayer;
using InternationalLicenseBusinessLayer;

namespace DVLD_Project.Controls.Licences
{
    public partial class DriverLicenceInfo_CTRL : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        private clsInternationalLicense _InternationalLicense;

        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }
        public DriverLicenceInfo_CTRL()
        {
            InitializeComponent();
        }


        public void EnableDetaindFiled()
        {
            lblIsDetained.Text = "Yes";
        }
        public void DisableIsDetained()
        {
            lblIsDetained.Text = "No";
        }
        public void DeactivateOldLicenseLabel()
        {
            lblIsActive.Text = "No";
        }


        private void _LoadPersonImage()
        {
            string ImagePath = string.Empty;

            if (_License != null)
            {
                if (_License.DriverInfo.Person.Gender == "Male")
                    pbPersonImage.Image = Resources.icons8_person_94;
                else
                    pbPersonImage.Image = Resources.icons8_businesswoman_94;

                ImagePath = _License.DriverInfo.Person.ImagePath;
            }

            if (_InternationalLicense != null)
            {
                if (_InternationalLicense.DriverInfo.Person.Gender == "Male")
                    pbPersonImage.Image = Resources.icons8_person_94;
                else
                    pbPersonImage.Image = Resources.icons8_businesswoman_94;

                ImagePath = _InternationalLicense.DriverInfo.Person.ImagePath;
            }


            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image : " + ImagePath, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _LoadLicenseFiledsInfo()
        {
            lblLicenseID.Text = _License.ID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = clsDetainedLicense.IsLicenseDetained(_License.ID) ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblGendor.Text = _License.DriverInfo.Person.Gender;
            lblFullName.Text = _License.DriverInfo.Person.FullName;
            lblNationalNo.Text = _License.DriverInfo.Person.NationalNo;
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.Person.DateOfBirth);
            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.GetLicenseWithID(LicenseID);


            if (_License == null)
            {
                //MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                ResetValues();
                return;
            }

            _LoadLicenseFiledsInfo();

            _LoadPersonImage();

        }

        public void LoadInfo(clsLicense License)
        {
            _LicenseID = License.ID;

            _License = License;

            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                ResetValues();
                return;
            }


            _LoadLicenseFiledsInfo();
            _LoadPersonImage();

        }

        public void LoadInfo(clsInternationalLicense InternationalLicense)
        {
            _LicenseID = InternationalLicense.ID;

            _InternationalLicense = InternationalLicense;

            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find this international License ID = " + _LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                ResetValues();
                return;
            }

            lblLicenseID.Text = _InternationalLicense.ID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblClass.Text = _InternationalLicense.LocalLicenseInfo.LicenseClassInfo.ClassName;
            lblGendor.Text = _InternationalLicense.DriverInfo.Person.Gender;
            lblFullName.Text = _InternationalLicense.DriverInfo.Person.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.Person.NationalNo;
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.Person.DateOfBirth);
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);
            lblIssueReason.Text = _InternationalLicense.LocalLicenseInfo.IssueReasonText;
            lblNotes.Text = _InternationalLicense.LocalLicenseInfo.Notes == "" ? "No Notes" : _InternationalLicense.LocalLicenseInfo.Notes;

            ///// hide Is Detained Filed in International License
            lblIsDetained.Visible = false;
            pictureBox12.Visible = false;
            label11.Visible = false;


            _LoadPersonImage();

        }

        public void ResetValues()
        {
            lblLicenseID.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblIsDetained.Text = "[???]";
            lblClass.Text = "[???]";
            lblGendor.Text = "[???]";
            lblFullName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblNotes.Text = "[???]";

            pbPersonImage.Image = Resources.icons8_person_94;

        }

    }
}
