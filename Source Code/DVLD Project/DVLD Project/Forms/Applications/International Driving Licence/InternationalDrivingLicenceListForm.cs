using System;
using System.Data;
using System.Windows.Forms;
using DriversBusinessLayer;
using DVLD_Project.Forms.People;
using InternationalLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Licences.International_Driving_Licence
{
    public partial class InternationalDrivingLicenceListForm : Form
    {

        private string SearchAttribute = String.Empty;
        string[] FilterTypeArr =
        {
            "InternationalLicenseID",
            "ApplicationID",
            "DriverID",
            "IssuedUsingLocalLicenseID"
        };
        private static DataTable _dtAllInternationalApp;

        public InternationalDrivingLicenceListForm()
        {
            InitializeComponent();
        }

        private void _RefreshInternationalDrivingLicenseList(bool filter = false)
        {
            if (filter)
            {
                ///// if input is empty 
                if (txtFilterValue.Text.ToString().Trim() == string.Empty)
                {
                    _dtAllInternationalApp = clsInternationalLicense.GetAllInternationalLicenses();
                    dgvInternationalDrivingLicenseApplications.DataSource = _dtAllInternationalApp;
                }
                else
                {
                    _dtAllInternationalApp.DefaultView.RowFilter = string.Format("[{0}] = {1}", SearchAttribute, txtFilterValue.Text.Trim());

                }
            }
            else
            {
                ///// get Updated Data from Database
                _dtAllInternationalApp = clsInternationalLicense.GetAllInternationalLicenses();
                dgvInternationalDrivingLicenseApplications.DataSource = _dtAllInternationalApp;
                cbFilterBy.SelectedIndex = 0;
            }

            lblRecordsCount.Text = (dgvInternationalDrivingLicenseApplications.Rows.Count).ToString();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InternationalDrivingLicenceListForm_Load(object sender, EventArgs e)
        {
            _RefreshInternationalDrivingLicenseList();

            if (dgvInternationalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvInternationalDrivingLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalDrivingLicenseApplications.Columns[0].Width = 160;

                dgvInternationalDrivingLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalDrivingLicenseApplications.Columns[1].Width = 200;

                dgvInternationalDrivingLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvInternationalDrivingLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalDrivingLicenseApplications.Columns[3].Width = 200;

                dgvInternationalDrivingLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalDrivingLicenseApplications.Columns[4].Width = 200;

                dgvInternationalDrivingLicenseApplications.Columns[5].HeaderText = "Expiartion Date";
                dgvInternationalDrivingLicenseApplications.Columns[5].Width = 200;

                dgvInternationalDrivingLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalDrivingLicenseApplications.Columns[6].Width = 120;

                dgvInternationalDrivingLicenseApplications.Columns[7].HeaderText = "Created By User ID";
                dgvInternationalDrivingLicenseApplications.Columns[7].Width = 120;
            }

            cbFilterBy.SelectedIndex = 0;

        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[2].Value;

            try
            {
                ShowPersonInfoForm showPersonInfoForm = new ShowPersonInfoForm(clsDriver.GetDriverWithID(DriverID).Person);
                showPersonInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to locate information for this person.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm((int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[0].Value, true);
            showLicenseInfoForm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm((int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[2].Value);
            showPersonLicenceHistoryForm.ShowDialog();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                _RefreshInternationalDrivingLicenseList();
                txtFilterValue.Visible = false;
            }
            else
            {
                SearchAttribute = FilterTypeArr[cbFilterBy.SelectedIndex - 1];
                txtFilterValue.Visible = true;
            }

            txtFilterValue.Text = string.Empty;
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            AddInternationalDrivingLicenceForm addInternationalDrivingLicenceForm = new AddInternationalDrivingLicenceForm();
            addInternationalDrivingLicenceForm.ShowDialog();
            _RefreshInternationalDrivingLicenseList();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _RefreshInternationalDrivingLicenseList(true);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Cancel the input if it's not a digit
            }
        }


    }
}
