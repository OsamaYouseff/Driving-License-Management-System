using DVLD.Classes;
using System.Drawing;
using TesttBusinessLayer;
using System.Windows.Forms;
using TestTypesBusinessLayer;
using DVLD_Project.Properties;
using TestAppointmentBusinessLayer;
using LocalDrivingLicenseApplicationBusinessLayer;

namespace DVLD_Project.Controls.Tests
{
    public partial class ScheduledTest_CTRL : UserControl
    {

        private clsTestAppointment _TestAppointment;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1, _TestAppointmentID = -1;
        public clsTestType.enTestType TestTypeID = clsTestType.enTestType.VisionTest;

        public ScheduledTest_CTRL()
        {
            InitializeComponent();
        }

        private void _SetDefaultValues()
        {
            lblLocalDrivingLicenseAppID.Text = "[??]";
            lblDrivingClass.Text = "[??????]";
            lblFullName.Text = "[??????]";
            lblDate.Text = "[dd/mm/yyyy]";
            lblFees.Text = "[$$$]";
            lblTestID.Text = "Not Taken Yet";


        }

        private void _SetTestImage()
        {
            switch (TestTypeID)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        gbTestType.Text = "Vision Test";
                        //lblTitle.Text = "Vision Test Schedule";
                        pbTestTypeImage.Image = Resources.icons8_eye_941;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        gbTestType.Text = "Written Test";
                        //lblTitle.Text = "Written Test Schedule";
                        pbTestTypeImage.Image = Resources.icons8_notes_481; ;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        gbTestType.Text = "Street Test";
                        //lblTitle.Text = "Street Test Schedule";
                        pbTestTypeImage.Image = Resources.icons8_car_941;
                        break;
                    }
            }

        }

        private void _LoadBasicInfo()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(_LocalDrivingLicenseApplicationID);
            _TestAppointment = clsTestAppointment.GetTestAppointmentByID(_TestAppointmentID);

            if (_LocalDrivingLicenseApplication == null || _TestAppointment == null)
            {
                MessageBox.Show("Can't Find this Local Driving License Application Info or Test Appointment", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SetDefaultValues();
                return;
            }

            lblDate.Text = clsFormat.DateToShort2(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = clsTestAppointment.GetTriesNumForTestAppointment(_LocalDrivingLicenseApplicationID, TestTypeID).ToString();

            _SetTestImage();

        }

        public void LoadInfo(int TestAppointmentID, int LocalDrivingLicenseApplicationID)
        {
            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LoadBasicInfo();

        }

        public void LoadInfo(clsTest Test, int TestAppointmentID, int LocalDrivingLicenseApplicationID)
        {
            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LoadBasicInfo();
            lblTestID.Text = Test.ID.ToString();


            lblResultMsg.Visible = true;

            if (Test.TestResult)
            {
                lblResultMsg.Text = "Person Has Passed This Test";
                lblResultMsg.ForeColor = Color.Green;
            }
            else
            {
                lblResultMsg.Text = "Person Has Faild in This Test";
                lblResultMsg.ForeColor = Color.Red;
            }


        }

        public void UpdateTestID(int TestID)
        {
            lblTestID.Text = TestID.ToString();
        }

        public void UpdateResultMessage(bool TestResult)
        {
            if (TestResult)
            {
                lblResultMsg.Text = "Person Has Passed This Test";
                lblResultMsg.ForeColor = Color.Green;
            }
            else
            {
                lblResultMsg.Text = "Person Has Faild in This Test";
                lblResultMsg.ForeColor = Color.Red;
            }

            lblResultMsg.Visible = true;
        }

    }
}
