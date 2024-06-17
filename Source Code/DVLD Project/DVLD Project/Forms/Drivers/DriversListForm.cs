using System;
using System.Data;
using System.Windows.Forms;
using DriversBusinessLayer;
using DVLD_Project.Forms.People;
using DVLD_Project.Forms.Licences;
using DVLD_Project.Forms.Licences.International_Driving_Licence;

namespace DVLD_Project.Forms.Drivers
{
    public partial class DriversListForm : Form
    {
        //// Variables
        private string SearchAttribute = String.Empty;
        string[] FilterTypeArr =
        {
            "DriverID",
            "PersonID",
            "NationalNo",
            "Full Name",
        };
        private static DataTable _dtAllDrivers;


        public DriversListForm()
        {
            InitializeComponent();
        }



        private void _RefreshContactsList(bool filter = false)
        {
            if (filter)
            {

                ///// if input is empty 
                if (txtFilterValue.Text.ToString().Trim() == string.Empty)
                {
                    _dtAllDrivers = clsDriver.GetAllDrivers();
                    DriversGridView.DataSource = _dtAllDrivers;
                }
                else
                {
                    if (SearchAttribute == "DriverID" || SearchAttribute == "PersonID")
                        _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", SearchAttribute, txtFilterValue.Text.Trim());
                    else
                        _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", SearchAttribute, txtFilterValue.Text.Trim());

                }
            }
            else
            {

                ///// get Updated Data from Database
                _dtAllDrivers = clsDriver.GetAllDrivers();
                DriversGridView.DataSource = _dtAllDrivers;

                FilterMenu.SelectedIndex = 0;
                txtFilterValue.Visible = false;


            }

            lblRecordsCount.Text = (DriversGridView.Rows.Count).ToString();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DriversListForm_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();


            if (DriversGridView.Rows.Count > 0)
            {
                DriversGridView.Columns[0].HeaderText = "Driver ID";
                DriversGridView.Columns[0].Width = 130;

                DriversGridView.Columns[1].HeaderText = "Person ID";
                DriversGridView.Columns[1].Width = 150;

                DriversGridView.Columns[2].HeaderText = "NationalNo";
                DriversGridView.Columns[2].Width = 120;

                DriversGridView.Columns[3].HeaderText = "Full Name";
                DriversGridView.Columns[3].Width = 350;

                DriversGridView.Columns[4].HeaderText = "Created Date";
                DriversGridView.Columns[4].Width = 180;

                DriversGridView.Columns[5].HeaderText = "Active Licenses";
                DriversGridView.Columns[5].Width = 100;
            }

        }

        private void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterMenu.SelectedIndex == 0)
            {
                _RefreshContactsList();
                txtFilterValue.Visible = false;
            }
            else
            {
                SearchAttribute = FilterTypeArr[FilterMenu.SelectedIndex - 1];
                txtFilterValue.Visible = true;
            }
            txtFilterValue.Text = string.Empty;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _RefreshContactsList(true);
        }

        ////restrict entering string in Person ID Filiter
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (SearchAttribute == "PersonID" || SearchAttribute == "DriverID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true; // Cancel the input if it's not a digit
                }
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowPersonInfoForm showPersonInfoForm = new ShowPersonInfoForm((int)DriversGridView.CurrentRow.Cells[1].Value);

            showPersonInfoForm.ShowDialog();

        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddInternationalDrivingLicenceForm addInternationalDrivingLicenceForm = new AddInternationalDrivingLicenceForm();
            addInternationalDrivingLicenceForm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm((int)DriversGridView.CurrentRow.Cells[0].Value);

            showPersonLicenceHistoryForm.ShowDialog();
        }


    }
}
