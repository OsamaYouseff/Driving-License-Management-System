using System;
using System.Windows.Forms;
using TestTypesBusinessLayer;

namespace DVLD_Project.Forms.Tests.Test_Types
{
    public partial class ScheduleTestForm : Form
    {
        private int _AppointmentID = -1, _LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private bool _UpdateMode = false;

        public ScheduleTestForm()
        {
            InitializeComponent();
        }

        public ScheduleTestForm(clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _UpdateMode = false;
            _TestTypeID = TestTypeID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LoadData();
        }

        public ScheduleTestForm(int AppointmentID, clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();


            _UpdateMode = true;
            _AppointmentID = AppointmentID;
            _TestTypeID = TestTypeID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LoadData();
        }

        private void _LoadData()
        {
            try
            {
                scheduleTest_CTRL.TestTypeID = _TestTypeID;

                scheduleTest_CTRL.LoadInfo(_AppointmentID, _LocalDrivingLicenseApplicationID, _UpdateMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
