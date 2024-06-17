using System;
using System.Windows.Forms;
using LicenseBusinessLayer;

namespace DVLD_Project.Controls.Licences
{
    public partial class DriverLicenceInfoWithFiliter_CTRL : UserControl
    {



        public delegate void GetLicenseID(object sender, int ID);
        public GetLicenseID getLicenseID;


        private int _LicenseID = -1;
        public int LicenseID
        {
            get { return driverLicenceInfo_CTRL.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return driverLicenceInfo_CTRL.SelectedLicenseInfo; }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }


        public void setLicenseID(int LicenseID)
        {
            _LicenseID = LicenseID;
        }
        public void DisableSearchBtn()
        {
            SearchLicenseBtn.Enabled = false;
        }
        public void DeactivateOldLicenseLabel()
        {
            driverLicenceInfo_CTRL.DeactivateOldLicenseLabel();
        }
        public void EnableDetaindFiled()
        {
            driverLicenceInfo_CTRL.EnableDetaindFiled();
        }
        public void DisableIsDetained()
        {
            driverLicenceInfo_CTRL.DisableIsDetained();
        }


        public DriverLicenceInfoWithFiliter_CTRL()
        {
            InitializeComponent();
        }


        public void LoadData(int LicenseID, bool WithPerforming = false)
        {
            _LicenseID = LicenseID;

            if (WithPerforming)
            {
                txtLicenseID.Text = LicenseID.ToString();

                SearchLicenseBtn.PerformClick();
                FilterEnabled = false;

                SearchLicense_Btn_Click(this, new EventArgs());
            }

        }

        private void DriverLicenceInfoWithFiliter_CTRL_Load(object sender, EventArgs e)
        {
            SearchLicenseBtn.Enabled = false;
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {

            if (txtLicenseID.Text.Trim() != string.Empty && !SearchLicenseBtn.Enabled) SearchLicenseBtn.Enabled = true;
            if (txtLicenseID.Text.Trim() == string.Empty && SearchLicenseBtn.Enabled) SearchLicenseBtn.Enabled = false;

        }

        private void SearchLicense_Btn_Click(object sender, EventArgs e)
        {

            if (txtLicenseID.Text.Trim() != string.Empty)
            {
                _LicenseID = Convert.ToInt32(txtLicenseID.Text);
                driverLicenceInfo_CTRL.LoadInfo(_LicenseID);
            }

            if (_LicenseID != -1)
                getLicenseID?.Invoke(this, Convert.ToInt32(txtLicenseID.Text));

        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtLicenseID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }
    }
}
