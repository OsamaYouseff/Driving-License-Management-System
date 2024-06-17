using System;
using System.Data;
using TesttBusinessLayer;
using PeopleBusinessLayer;
using System.Windows.Forms;
using LicenseBusinessLayer;
using DriversBusinessLayer;
using TestTypesBusinessLayer;
using DVLD_Project.Forms.Licences;
using DVLD_Project.Global_Classes;
using DVLD_Project.Forms.Tests.Test_Types;
using LocalDrivingLicenseApplicationBusinessLayer;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;

namespace DVLD_Project.Forms.Applications.Driving_License.Local_Driving_Licence
{
    public partial class LocalDrivingLicenceListForm : Form
    {
        private string SearchAttribute = String.Empty;
        string[] FilterTypeArr =
        {
            "LocalDrivingLicenseApplicationID",
            "NationalNo",
            "FullName",
            "Status"
        };
        private static DataTable _dtAllLocalApp;
        private string _LicenseNotes = string.Empty;


        public LocalDrivingLicenceListForm()
        {
            InitializeComponent();
        }

        private void _RefreshLocalDrivingLicenseList(bool filter = false)
        {
            if (filter)
            {

                ///// if input is empty 
                if (txtFilterValue.Text.ToString().Trim() == string.Empty)
                {
                    _dtAllLocalApp = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                    dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalApp;
                }
                else
                {
                    if (SearchAttribute == "LocalDrivingLicenseApplicationID")
                        _dtAllLocalApp.DefaultView.RowFilter = string.Format("[{0}] = {1}", SearchAttribute, txtFilterValue.Text.Trim());
                    else
                        _dtAllLocalApp.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", SearchAttribute, txtFilterValue.Text.Trim());

                    //// this line to filter data suing Query to database but it's not your best choise 
                    //dgvLocalDrivingLicenseApplications.DataSource = clsPerson.GetFilterdPeopleBy(SearchAttribute, txtFilterValue.Text.ToString().Trim());

                }
            }
            else
            {
                ///// get Updated Data from Database
                _dtAllLocalApp = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalApp;
                cbFilterBy.SelectedIndex = 0;
            }

            lblRecordsCount.Text = (dgvLocalDrivingLicenseApplications.Rows.Count).ToString();
        }

        private void LocalDrivingLicenceListForm_Load(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseList();

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 190;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;


                dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 120;
            }

            cbFilterBy.SelectedIndex = 0;

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            AddUpdateLocalDrivingLicenceForm addUpdateLocalDrivingLicenceForm = new AddUpdateLocalDrivingLicenceForm();

            addUpdateLocalDrivingLicenceForm.ShowDialog();

            _RefreshLocalDrivingLicenseList();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.SelectedIndex == 0)
            {
                _RefreshLocalDrivingLicenseList();
                txtFilterValue.Visible = false;
                cbStatus.Visible = false;
            }
            else
            {
                SearchAttribute = FilterTypeArr[cbFilterBy.SelectedIndex - 1];

                if (SearchAttribute != "Status")
                {
                    txtFilterValue.Visible = true;
                    cbStatus.Visible = false;

                }
                else
                {
                    cbStatus.Visible = true;
                    txtFilterValue.Visible = false;
                    cbStatus.SelectedIndex = 0;
                }
            }

            txtFilterValue.Text = string.Empty;
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && (SearchAttribute == "LocalDrivingLicenseApplicationID"))
            {
                e.Handled = true; // Cancel the input if it's not a digit
            }
        }

        private void cbFilterBy_TextChanged(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseList(true);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseList(true);
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Status";
            string FilterValue = cbStatus.Text;

            if (FilterValue == "All")
                _dtAllLocalApp.DefaultView.RowFilter = "";
            else
            {
                //in this case we deal with numbers not string.
                _dtAllLocalApp.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, FilterValue);
            }

            lblRecordsCount.Text = _dtAllLocalApp.DefaultView.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateLocalDrivingLicenceForm addUpdateLocalDrivingLicenceForm = new AddUpdateLocalDrivingLicenceForm((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            addUpdateLocalDrivingLicenceForm.ShowDialog();

            _RefreshLocalDrivingLicenseList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow CurrentRow = dgvLocalDrivingLicenseApplications.CurrentRow;

            ShowLocalDrivingLicenseApplicationInfoForm showLocalLicense = new ShowLocalDrivingLicenseApplicationInfoForm((int)CurrentRow.Cells[0].Value, (int)CurrentRow.Cells[5].Value);
            showLocalLicense.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// if the person is not a driver yet he doesn't have a history yet
            if (clsDriver.GetDriverWithNationalNo(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value.ToString()) == null)
            {
                MessageBox.Show("This person is not a driver, so they do not have any license history yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            ShowPersonLicenceHistoryForm showPersonLicenceHistoryForm = new ShowPersonLicenceHistoryForm(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value.ToString());
            showPersonLicenceHistoryForm.ShowDialog();

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow CurrentRow = dgvLocalDrivingLicenseApplications.CurrentRow;
            ShowLicenseInfoForm showLicenseInfoForm = new ShowLicenseInfoForm(CurrentRow.Cells[2].Value.ToString(), CurrentRow.Cells[1].Value.ToString());
            showLicenseInfoForm.ShowDialog();

        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to delete this Local Driving Licence [" + dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value))
                    MessageBox.Show("Local Driving Licence was deleted successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Local Driving Licence was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _RefreshLocalDrivingLicenseList();
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value.ToString() == "Cancelled")
            {
                MessageBox.Show("This local application has already been cancelled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value.ToString() == "Completed")
            {
                MessageBox.Show("Unable to cancel this local application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MessageBox.Show("Are you sure that you want to Cancel this Local Driving Licence [" + dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value + "]", "Confirm Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

                if (clsLocalDrivingLicenseApplication.Cancel())
                    MessageBox.Show("Local Driving Licence was cancelled successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Cannot cancel this Local Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _RefreshLocalDrivingLicenseList();
            }

        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

            ///// Variables
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID);


            bool IsLicenseExist = clsLicense.IsThereLicenseHasThisApplicationID(LocalDrivingLicenseApplication.ApplicationID);
            bool IsApplicationNew = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);
            bool IsApplicationCanceled = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.Cancelled);


            //Enabled only if person passed all tests and Does not have his license yet. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3 && !IsLicenseExist);

            showLicenseToolStripMenuItem.Enabled = IsLicenseExist;

            editToolStripMenuItem.Enabled = !IsLicenseExist && TotalPassedTests == 0 && IsApplicationNew;

            ScheduleTestsMenue.Enabled = !(IsLicenseExist || TotalPassedTests == 3 || !IsApplicationNew);

            CancelApplicaitonToolStripMenuItem.Enabled = IsApplicationNew;

            DeleteApplicationToolStripMenuItem.Enabled = !IsLicenseExist && TotalPassedTests == 0 && IsApplicationNew;

            ///// in case if Local Driving License Application is Cancelled before taking any Test then you can delete it form system normally
            DeleteApplicationToolStripMenuItem.Enabled = TotalPassedTests == 0 && (IsApplicationCanceled || IsApplicationNew);


            ///// in case if person is not a driver yet then you should hide show licences history 
            showPersonLicenseHistoryToolStripMenuItem.Enabled = clsDriver.IsDriverExist((string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value);

            bool PassedVisionTest = clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest); ;
            bool PassedStreetTest = clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, clsTestType.enTestType.StreetTest); ;


            if (ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }

        private bool _HandelPassedTests(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            if (TestType == clsTestType.enTestType.WrittenTest)
            {
                if (!clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, clsTestType.enTestType.VisionTest))
                {
                    MessageBox.Show("You must pass vision test before taking this test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else if (TestType == clsTestType.enTestType.StreetTest)
            {
                if (!clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, clsTestType.enTestType.VisionTest))
                {
                    MessageBox.Show("You must pass vision test before taking this test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest))
                {
                    MessageBox.Show("You must pass Written Test before taking this test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;
        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {
            DataGridViewRow CurrentRow = dgvLocalDrivingLicenseApplications.CurrentRow;
            int LocalDrivingLicenseApplicationID = (int)CurrentRow.Cells[0].Value;
            int PassedTestsNum = (int)CurrentRow.Cells[5].Value;

            ///// Make sure that person Has passed Pervious Test before Get to this test
            if (!_HandelPassedTests(LocalDrivingLicenseApplicationID, TestType))
                return;

            TestsAppointmentsListForm testsAppointmentsListForm = new TestsAppointmentsListForm(LocalDrivingLicenseApplicationID, TestType, PassedTestsNum);
            testsAppointmentsListForm.ShowDialog();

            _RefreshLocalDrivingLicenseList();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void GetNotes(object sender, string Notes)
        {
            _LicenseNotes = Notes;
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue a license for this person?", "Confirm issue a license", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            DataGridViewRow CurrentRow = dgvLocalDrivingLicenseApplications.CurrentRow;
            string NationalNo = (string)CurrentRow.Cells[2].Value;
            int LocalDrivingLicenseApplicationID = (int)CurrentRow.Cells[0].Value;
            int DriverID = -1;


            clsPerson person = clsPerson.GetPerson(NationalNo);

            if (person == null)
            {
                MessageBox.Show("Unable to find a person with the provided National No =" + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!clsDriver.IsDriverExistWithPersonID(person.ID))
            {
                clsDriver driver = new clsDriver();
                driver.PersonID = person.ID;
                driver.CreatedDate = DateTime.Now;
                driver.CreatedByUserID = clsGlobal.CurrentUser.ID;


                if (!driver.Save())
                {
                    MessageBox.Show("Unable to designate this person as a driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DriverID = driver.ID;
            }
            else
            {
                DriverID = clsDriver.GetDriverWithPersonID(person.ID).ID;
            }


            //// Get LocalDrivingLicenseApplication Info
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
            {
                MessageBox.Show("Unable to locate information for the local driving license application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            ///// Create New License Steps
            clsLicense license = new clsLicense();

            license.DriverID = DriverID;
            license.ApplicationID = localDrivingLicenseApplication.ApplicationID;
            license.LicenseClass = localDrivingLicenseApplication.LicenseClassInfo.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = license.IssueDate.AddYears(localDrivingLicenseApplication.LicenseClassInfo.DefaultValidityLength);
            license.IsActive = true;
            license.IssueReason = clsLicense.enIssueReason.FirstTime;
            license.CreatedByUserID = clsGlobal.CurrentUser.ID;
            license.PaidFees = localDrivingLicenseApplication.LicenseClassInfo.ClassFees;

            //// Ask For Taking Notes
            if (MessageBox.Show("Do You Want To Take Some Notes For this License ", "Confirm Take Notes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                LicenseNotesForm licenseNotesForm = new LicenseNotesForm();

                licenseNotesForm.GetNotes += GetNotes;

                licenseNotesForm.ShowDialog();
            }

            license.Notes = _LicenseNotes;


            if (license.Save())
            {
                MessageBox.Show(localDrivingLicenseApplication.LicenseClassInfo.ClassName + " has been created successfully for this person.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                localDrivingLicenseApplication.SetCompleted();
            }
            else
            {
                ////// important Step : if system Can't Create New License you should Delete Driver if the person wasn't A driver Before 
                if (!clsLicense.IsLicenseExistWithDriverID(DriverID))//// means that person wasn't A driver Before you should delete it's driver data
                    clsDriver.DeleteADriver(DriverID);

                MessageBox.Show("Unable to create a new license for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            _RefreshLocalDrivingLicenseList();


        }
    }
}
