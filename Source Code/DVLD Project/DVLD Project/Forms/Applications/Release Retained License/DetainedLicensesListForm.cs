using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Project.Forms.People;
using DVLD_Project.Forms.Licences;
using DetainedLicenseBusinessLayer;
using DVLD_Project.Forms.Licences.Detain_License;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Applications.Release_Retained_License
{
    public partial class DetainedLicensesListForm : Form
    {
        private string SearchAttribute = String.Empty;
        string[] FilterTypeArr =
        {
            "DetainID",
            "IsReleased",
            "NationalNo",
            "FullName",
            "ReleaseApplicationID"
        };
        private static DataTable _dtAllDetainedLicenses;
        public DetainedLicensesListForm()
        {
            InitializeComponent();
        }


        private void _RefreshDetainedLicensesList(bool filter = false)
        {
            if (filter)
            {
                ///// if input is empty 
                if (txtFilterValue.Text.ToString().Trim() == string.Empty)
                {

                    _dtAllDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

                    dgvDetainedLicenses.DataSource = _dtAllDetainedLicenses;
                }
                else
                {
                    if (SearchAttribute == "ReleaseApplicationID" || SearchAttribute == "DetainID")
                        _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", SearchAttribute, txtFilterValue.Text.Trim());
                    else
                        _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", SearchAttribute, txtFilterValue.Text.Trim());

                }
            }
            else
            {
                ///// get Updated Data from Database
                _dtAllDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
                dgvDetainedLicenses.DataSource = _dtAllDetainedLicenses;
                cbFilterBy.SelectedIndex = 0;
            }

            lblTotalRecords.Text = (dgvDetainedLicenses.Rows.Count).ToString();
        }

        private void DetainedLicensesListForm_Load(object sender, EventArgs e)
        {
            _RefreshDetainedLicensesList();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 180;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 180;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 120;

            }

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            ReleaseRetainedLicenseForm releaseRetainedLicenseForm = new ReleaseRetainedLicenseForm();
            releaseRetainedLicenseForm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            DetainLicenseApplicationForm detainLicenseApplicationForm = new DetainLicenseApplicationForm();
            detainLicenseApplicationForm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonInfoForm showPersonInfo = new ShowPersonInfoForm((string)dgvDetainedLicenses.CurrentRow.Cells[6].Value);
            showPersonInfo.Show();
            _RefreshDetainedLicensesList();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            showLicenseInfoForm.ShowDialog();
            _RefreshDetainedLicensesList();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString());
            showPersonLicenceHistoryForm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseRetainedLicenseForm releaseRetainedLicenseForm = new ReleaseRetainedLicenseForm((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            releaseRetainedLicenseForm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                _RefreshDetainedLicensesList();
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = false;
            }
            else
            {
                SearchAttribute = FilterTypeArr[cbFilterBy.SelectedIndex - 1];

                if (SearchAttribute != "IsReleased")
                {
                    txtFilterValue.Visible = true;
                    cbIsReleased.Visible = false;

                }
                else
                {
                    cbIsReleased.Visible = true;
                    txtFilterValue.Visible = false;
                    cbIsReleased.SelectedIndex = 0;
                }
            }

            txtFilterValue.Text = string.Empty;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _RefreshDetainedLicensesList(true);
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;
            int Value = 1;


            switch (FilterValue)
            {
                case "Yes":
                    Value = 1;
                    break;

                case "No":
                    Value = 0;
                    break;
            }


            if (FilterValue == "All")
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            else
            {
                //in this case we deal with numbers not string.
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, Value);
            }

            lblTotalRecords.Text = _dtAllDetainedLicenses.DefaultView.Count.ToString();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;

            //// using queries
            //releaseDetainedLicenseToolStripMenuItem.Enabled = clsDetainedLicense.IsLicenseDetained((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
        }
    }
}
