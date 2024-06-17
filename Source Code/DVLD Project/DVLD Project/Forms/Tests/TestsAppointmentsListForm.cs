using System;
using System.Data;
using TesttBusinessLayer;
using System.Windows.Forms;
using TestTypesBusinessLayer;
using DVLD_Project.Properties;
using TestAppointmentBusinessLayer;

namespace DVLD_Project.Forms.Tests.Test_Types
{
    public partial class TestsAppointmentsListForm : Form
    {
        private DataTable _dtTestAppointments;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;


        public TestsAppointmentsListForm()
        {
            InitializeComponent();
        }
        public TestsAppointmentsListForm(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType, int PassedTestNum)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            drivingLicenceinfo_CTRL1.LoadData(LocalDrivingLicenseApplicationID, PassedTestNum);

            _TestType = TestType;

            _LoadTestTypeImageAndTitle();
        }




        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.icons8_eye_942;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.icons8_notes_481;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.icons8_car_941;
                        break;
                    }
            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            //////  Check if person has an active (not locked) or completed appointment if so prevent Making a new one
            if (clsTest.DoesPersonPassedThisTestBefore(_LocalDrivingLicenseApplicationID, _TestType))
            {
                MessageBox.Show("This person already has passed this test so thay can't take it again!", "Already been tested", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestAppointment.DoesPersonHaveAnActiveAppointment(_LocalDrivingLicenseApplicationID, _TestType))
            {
                MessageBox.Show("This person already has an active appointment so thay can't take a new one", "Already been tested", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ScheduleTestForm scheduleTestForm = new ScheduleTestForm(_TestType, _LocalDrivingLicenseApplicationID);
            scheduleTestForm.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleTestForm scheduleTestForm = new ScheduleTestForm((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value, _TestType, _LocalDrivingLicenseApplicationID);
            scheduleTestForm.ShowDialog();
            _RefreshTestAppointmentsList();

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow CurrentRow = dgvLicenseTestAppointments.CurrentRow;

            bool IsLocked = (bool)CurrentRow.Cells[3].Value;

            TakeTestForm takeTestForm = new TakeTestForm((int)CurrentRow.Cells[0].Value, _LocalDrivingLicenseApplicationID, _TestType, IsLocked);
            takeTestForm.ShowDialog();

            _RefreshTestAppointmentsList();
        }

        private void _RefreshTestAppointmentsList()
        {
            _LoadTestTypeImageAndTitle();

            _dtTestAppointments = clsTestAppointment.GetAllTestAppointmentsForLocalAppAndTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtTestAppointments;

            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 200;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 250;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 200;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 150;
            }



        }

        private void TestsAppointmentsListForm_Load(object sender, EventArgs e)
        {
            _RefreshTestAppointmentsList();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((bool)dgvLicenseTestAppointments.CurrentRow.Cells[3].Value) //// is Locked
            {
                takeTestToolStripMenuItem.Text = "Show Test Result";
                editToolStripMenuItem.Text = "Show Appointment Info";
                editToolStripMenuItem.Image = Resources.icons8_choice_48;
            }
            else
            {
                takeTestToolStripMenuItem.Text = "Take Test";
                editToolStripMenuItem.Text = "Edit";
                editToolStripMenuItem.Image = Resources.icons8_edit_48;
            }

        }


    }
}
