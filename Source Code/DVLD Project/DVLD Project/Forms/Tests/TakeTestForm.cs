using System;
using TesttBusinessLayer;
using System.Windows.Forms;
using TestTypesBusinessLayer;
using DVLD_Project.Global_Classes;
using TestAppointmentBusinessLayer;

namespace DVLD_Project.Forms.Tests
{
    public partial class TakeTestForm : Form
    {

        private int _TestAppointmentID = -1, _LocalDrivingLicenseApplicationID = -1;
        private bool _IsLocked = false;
        private clsTest _Test = new clsTest();
        private clsTestAppointment _TestAppointment;

        public TakeTestForm()
        {
            InitializeComponent();
        }
        public TakeTestForm(int TestAppointmentID, int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, bool IsLocked)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _IsLocked = IsLocked;

            scheduledTest_CTRL.TestTypeID = TestTypeID;

            if (_IsLocked)
            {
                _Test = clsTest.GetTestByTestAppointmentID(_TestAppointmentID);
                if (_Test == null)
                {
                    MessageBox.Show("Unable to find information for this test info.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                scheduledTest_CTRL.LoadInfo(_Test, _TestAppointmentID, _LocalDrivingLicenseApplicationID);

            }
            else
            {

                _TestAppointment = clsTestAppointment.GetTestAppointmentByID(_TestAppointmentID);

                if (_TestAppointment == null)
                {
                    MessageBox.Show("Unable to find this Test Appointment Info", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                scheduledTest_CTRL.LoadInfo(_TestAppointmentID, _LocalDrivingLicenseApplicationID);
            }

            _LoadInfo();

        }


        private void _DisableControls()
        {
            rbFail.Enabled = false;
            rbPass.Enabled = false;
            lblUserMessage.Visible = true;
            txtNotes.Enabled = false;
            SaveBtn.Enabled = false;

        }

        private void _LoadInfo()
        {
            ///// ReadOnly Mode
            if (_IsLocked)
            {
                _DisableControls();

                if (_Test.TestResult)
                {
                    rbPass.Checked = true;
                    rbFail.Checked = false;
                }
                else
                {
                    rbPass.Checked = false;
                    rbFail.Checked = true;
                }

                if (_Test.Notes == string.Empty)
                    txtNotes.Text = "No Notes.";

                else
                    txtNotes.Text = _Test.Notes;

            }
        }

        private void _FillData()
        {
            //// New Test
            if (!_IsLocked)
            {

                _Test.Mode = clsTest.enMode.AddNew;
                _Test.TestAppointmentID = _TestAppointmentID;
                _Test.CreatedByUserID = clsGlobal.CurrentUser.ID;
                _Test.Notes = txtNotes.Text;

                if (rbPass.Checked)
                    _Test.TestResult = true;
                else
                    _Test.TestResult = false;
            }
            else
            {
                MessageBox.Show("Unable to save information for this test because it has already been taken.", "Already been taken", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            _FillData();

            if (MessageBox.Show("Are you sure that you want to set this result for this test.", "Confirm Taking Test", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (_Test.Save())
                {
                    scheduledTest_CTRL.UpdateTestID(_Test.ID);
                    scheduledTest_CTRL.UpdateResultMessage(_Test.TestResult);


                    ///// Lock Test Appointment 
                    if (!_TestAppointment.LockTestAppointment())
                        MessageBox.Show("Unable to lock the test appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _DisableControls();

                    MessageBox.Show("The result was set for this test successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Unable to set the result for this test.", "Faild to set", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
