using System;
using System.Data;
using System.Windows.Forms;
using DriversBusinessLayer;
using LicenseBusinessLayer;
using InternationalLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Controls.Licences
{
    public partial class DriverLicenceHistory_CTRL : UserControl
    {

        private int _DriverID;
        private clsDriver _Driver = new clsDriver();
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;


        public DriverLicenceHistory_CTRL()
        {
            InitializeComponent();
        }

        public DriverLicenceHistory_CTRL(int DriverID)
        {
            InitializeComponent();
            _DriverID = DriverID;

            try
            {
                _Driver = clsDriver.GetDriverWithID(DriverID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("This person does not exist in the system.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.GetDriverWithID(_DriverID);

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void LoadDriverInfoByPersonID(int PersonID)
        {

            _Driver = clsDriver.GetDriverWithPersonID(PersonID);

            if (_Driver != null)
            {
                _DriverID = _Driver.ID;
            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();


        }

        private void _LoadLocalLicenseInfo()
        {
            _dtDriverLocalLicensesHistory = clsLicense.GetAllLocalLicensesForDriver(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;

            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();


            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 190;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 190;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 80;

            }
        }

        private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicensesHistory = clsInternationalLicense.GetAllInternationalLicensesForDriver(_DriverID);

            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;

            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 210;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 210;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 80;

            }
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value, true);
            showLicenseInfoForm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            showLicenseInfoForm.ShowDialog();
        }

    }
}
