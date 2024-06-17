using System;
using TesttBusinessLayer;
using System.Windows.Forms;
using TestTypesBusinessLayer;
using DVLD_Project.Properties;
using ApplicationBusinessLayer;
using DVLD_Project.Global_Classes;
using TestAppointmentBusinessLayer;
using LocalDrivingLicenseApplicationBusinessLayer;

namespace DVLD_Project.Controls.Tests
{
    public partial class ScheduleTest_CTRL : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;


        private bool _FirstTimeTake = true;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private string _OldDateValue = string.Empty;
        private clsTestAppointment _testAppointment;
        private float _TestFees = 0, _ApplicationFees = 0;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            lblTitle.Text = "Vision Test Schedule";
                            pbTestTypeImage.Image = Resources.icons8_eye_941;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            lblTitle.Text = "Written Test Schedule";
                            pbTestTypeImage.Image = Resources.icons8_notes_481; ;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            lblTitle.Text = "Street Test Schedule";
                            pbTestTypeImage.Image = Resources.icons8_car_941;
                            break;
                        }
                }
            }
        }


        public ScheduleTest_CTRL()
        {
            InitializeComponent();
        }

        public void ResetValues()
        {
            lblLocalDrivingLicenseAppID.Text = "[??]";
            lblDrivingClass.Text = "[???????]";
            lblFees.Text = "[$$$]";
            lblFullName.Text = "[???????]";
            lblTrial.Text = "[??]";
            lblRetakeTestAppID.Text = "N/A";
            lblRetakeAppFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";

            gbRetakeTestInfo.Enabled = false;

        }

        private void _FillData()
        {
            _testAppointment.AppointmentDate = dtpTestDate.Value;

            if (_Mode == enMode.AddNew)
            {
                _testAppointment.TestTypeID = _TestTypeID;
                _testAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
                _testAppointment.RetakeTestApplicationID = -1;
                _testAppointment.PaidFees = _TestFees;
                _testAppointment.CreatedByUserID = clsGlobal.CurrentUser.ID;
                _testAppointment.IsLocked = false;

                if (!_FirstTimeTake)
                {
                    _testAppointment.RetakeTestAppInfo = new clsApplication();

                    _testAppointment.RetakeTestAppInfo.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                    _testAppointment.RetakeTestAppInfo.ApplicationDate = DateTime.Now;
                    _testAppointment.RetakeTestAppInfo.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                    _testAppointment.RetakeTestAppInfo.ApplicationStatus = clsApplication.enApplicationStatus.New;
                    _testAppointment.RetakeTestAppInfo.LastStatusDate = DateTime.Now;
                    _testAppointment.RetakeTestAppInfo.PaidFees = _ApplicationFees;
                    _testAppointment.RetakeTestAppInfo.CreatedByUserID = clsGlobal.CurrentUser.ID;
                }

            }
        }

        public void LoadInfo(int AppointmentID, int LocalDrivingLicenseApplicationID, bool UpdateMode = false)
        {
            if (!UpdateMode)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            //////  Check if person has an active (not locked) or completed appointment if so prevent Making a new one
            if (_Mode == enMode.AddNew)
            {

                if (clsTest.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, _TestTypeID))
                {
                    MessageBox.Show("This person has already passed this test and cannot take it again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (clsTestAppointment.DoesPersonHaveAnActiveAppointment(LocalDrivingLicenseApplicationID, _TestTypeID))
                {
                    MessageBox.Show("This person already has an active appointment, so they cannot schedule a new one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            ////// Fees Information 
            _TestFees = clsTestType.GetTestType((int)_TestTypeID).TestFees;
            _ApplicationFees = clsApplicationTypes.GetApplicationType((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;


            //-------------------------------

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Could not find this Local Driving License with ID = " + LocalDrivingLicenseApplicationID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LocalDrivingLicenseApplicationID = -1;
                ResetValues();
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFees.Text = _TestFees.ToString();
            lblFullName.Text = _LocalDrivingLicenseApplication.FullName;
            lblTrial.Text = clsTestAppointment.GetTriesNumForTestAppointment(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _TestTypeID).ToString();

            //-------------------------------


            /*
                if person faild before in this test then you have too make this appointment as a retake test
                so we check in databbase if there is a appointment 
            */

            if (clsTestAppointment.IsTestAppointmentExist(LocalDrivingLicenseApplicationID, _TestTypeID))
                _FirstTimeTake = false;
            else
                _FirstTimeTake = true;



            //-------------------------------

            if (_Mode == enMode.AddNew)
            {
                _testAppointment = new clsTestAppointment();

                if (!_FirstTimeTake)
                {
                    lblRetakeAppFees.Text = _ApplicationFees.ToString();
                    lblTotalFees.Text = (_TestFees + _ApplicationFees).ToString();
                    gbRetakeTestInfo.Enabled = true;
                }

            }
            else
            {
                _testAppointment = clsTestAppointment.GetTestAppointmentByID(AppointmentID);
                ///// if there system can't find the any of Tests Appointments 
                if (_testAppointment == null)
                {
                    MessageBox.Show("Could not find this Test Appointment with ID = " + AppointmentID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetValues();
                    return;
                }


                //-------------------------------

                dtpTestDate.Value = _testAppointment.AppointmentDate;

                //// take backup from Date
                _OldDateValue = _testAppointment.AppointmentDate.ToString();
                lblTitle.Text = "Edit " + lblTitle.Text;
                SaveBtn.Enabled = false;

                ///// if this Appointment is A retake Appointment in Case Of Udating Mode
                if (_testAppointment.RetakeTestAppInfo != null)
                {

                    lblRetakeTestAppID.Text = _testAppointment.RetakeTestAppInfo.ApplicationID.ToString();
                    lblRetakeAppFees.Text = _testAppointment.RetakeTestAppInfo.PaidFees.ToString();
                    lblTotalFees.Text = (_TestFees + _testAppointment.RetakeTestAppInfo.PaidFees).ToString();
                }
                else
                    _FirstTimeTake = true;

                ///// check if Appointment is Locked "already took" Disable Changing Date
                if (_testAppointment.IsLocked)
                {
                    dtpTestDate.Enabled = false;
                    lblUserMessage.Visible = true;
                    lblUserMessage.Text = "The person has already been tested, and the appointment has been locked.";
                    SaveBtn.Enabled = false;
                }

            }


            if (_FirstTimeTake)
                gbRetakeTestInfo.Enabled = false;
            else
                gbRetakeTestInfo.Enabled = true;


            ///// set Default Values For retake test
            if (_FirstTimeTake)
            {
                lblRetakeAppFees.Text = "0";
                lblTotalFees.Text = (_TestFees).ToString();
            }


        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {
            dtpTestDate.MinDate = DateTime.Now;
        }

        private void dtpTestDate_ValueChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = !(_OldDateValue == dtpTestDate.Value.ToString());
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ///// make sure that there is a change in Date to save the new info
            if (_OldDateValue != string.Empty && _OldDateValue == dtpTestDate.Value.ToString())
            {
                MessageBox.Show("There is no change of inforamtion to save !! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ///// In update mode pervent user form Make Test Date Earlier than cuurent one

            if (_Mode == enMode.Update && Convert.ToDateTime(_OldDateValue) > Convert.ToDateTime(dtpTestDate.Value))
            {
                MessageBox.Show("You cannot change the date to an earlier date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTestDate.Value = Convert.ToDateTime(_OldDateValue);
                return;
            }


            _FillData();


            //---------- first Save Reatake test Application if it's not first appointment 


            ///// if it's not first appointment then we have to make a new Application which is a Reatake test Application
            if (!_FirstTimeTake)
            {
                ///// if system can't save Reatake test Application
                if (!_testAppointment.RetakeTestAppInfo.Save())
                {
                    MessageBox.Show("Can't save the information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ///// we have no too assign the RetakeTestAppInfo to _testAppointment obj before store it in database 
                _testAppointment.RetakeTestApplicationID = _testAppointment.RetakeTestAppInfo.ApplicationID;
            }

            //---------- then Save the testAppointment

            if (_testAppointment.Save())
            {
                switch (_Mode)
                {
                    case enMode.AddNew:
                        MessageBox.Show("Test Appointment Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case enMode.Update:
                        MessageBox.Show("Test Appointment Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        break;
                }

                //// take backup from Date
                _OldDateValue = _testAppointment.AppointmentDate.ToString();

            }
            else
            {
                MessageBox.Show("There is a problem; the appointment cannot be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                /*
                  here there is a dangrous case you have make sure if the system faild to save test appointment but it already saved Retake Application (if it's not First appointment)
                  then you have to delete Retake Application from Database because it's no belong to any test appointment to avoid any dataLeak
                */

                clsApplication.DeleteApplication(_testAppointment.RetakeTestApplicationID);
            }



            if (!_FirstTimeTake)
                lblRetakeTestAppID.Text = _testAppointment.RetakeTestAppInfo.ApplicationID.ToString();

            SaveBtn.Enabled = false;

        }

    }
}
