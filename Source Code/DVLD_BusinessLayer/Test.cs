using System;
using System.Data;
using TestDataAccessLayer;
using TestTypesBusinessLayer;

namespace TesttBusinessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }



        public clsTest()
        {
            this.ID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private clsTest(int ID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.ID = ID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }




        private bool _AddNewTest()
        {
            this.ID = clsTestDataAccess.AddNewTest(this.TestAppointmentID, Convert.ToInt32(this.TestResult), this.Notes, this.CreatedByUserID);

            return this.ID != -1;
        }
        private bool _UpdateTest()
        {
            return clsTestDataAccess.UpdateTest(this.ID, this.TestAppointmentID, Convert.ToInt32(this.TestResult), this.Notes, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }


        public static clsTest GetTestByID(int ID)
        {
            int TestAppointmentID = -1;
            int TestResult = 0;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            if (clsTestDataAccess.GetTestByID(ID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(ID, TestAppointmentID, Convert.ToBoolean(TestResult), Notes, CreatedByUserID);
            else
                return null;
        }
        public static clsTest GetTestByTestAppointmentID(int TestAppointmentID)
        {
            int ID = -1;
            int TestResult = 0;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            if (clsTestDataAccess.GetTestByTestAppointmentID(ref ID, TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(ID, TestAppointmentID, Convert.ToBoolean(TestResult), Notes, CreatedByUserID);
            else
                return null;
        }
        public static DataTable GetAllTests()
        {
            return clsTestDataAccess.GetAllTests();

        }
        public static bool DoesPersonPassedThisTestBefore(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestDataAccess.DoesPersonPassedThisTestBefore(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

    }
}



