using System;
using System.Data;
using TestTypesBusinessLayer;
using ApplicationBusinessLayer;
using TestAppointmentDataAccessLayer;

namespace TestAppointmentBusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo { set; get; }

        public clsTestAppointment()
        {
            ID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int ID, clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.ID = ID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestAppInfo = clsApplication.GetApplicationInfoByID(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        public static clsTestAppointment GetTestAppointmentByID(int ID)
        {

            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;


            if (clsTestAppointmentDataAccess.GetTestAppointmentByID(ID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees,
                                    ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))



                return new clsTestAppointment(ID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        private bool _AddewTestAppointment()
        {
            this.ID = clsTestAppointmentDataAccess.AddNewTestAppointment((int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

            return (this.ID != -1);
        }

        private bool _UpdateTestAppointment()

        {
            return clsTestAppointmentDataAccess.UpdateTestAppointment(this.ID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }

        public bool LockTestAppointment()
        {
            return clsTestAppointmentDataAccess.LockTestAppointment(this.ID);
        }

        public static bool DeleteTestAppointment(int ID)
        {
            return clsTestAppointmentDataAccess.DeleteTestAppointment(ID);

        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentDataAccess.GetAllTestAppointments();

        }

        public static bool IsTestAppointmentExist(int ID)
        {
            return clsTestAppointmentDataAccess.IsTestAppointmentExist(ID);
        }

        public static DataTable GetAllTestAppointmentsForLocalAppAndTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.GetAllTestAppointmentsForLocalAppAndTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsTestAppointmentExist(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.IsTestAppointmentExist(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static bool DoesPersonHaveAnActiveAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.DoesPersonHaveAnActiveAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static int GetTriesNumForTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.GetTriesNumForTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

    }
}
