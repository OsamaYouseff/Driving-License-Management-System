using System;
using System.Data;
using DriversBusinessLayer;
using LicenseBusinessLayer;
using InternationalLicensesDataAccessLayer;

namespace InternationalLicenseBusinessLayer
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo = new clsDriver();

        public int IssuedUsingLocalLicenseID { get; set; }
        public clsLicense LocalLicenseInfo = new clsLicense();


        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public clsInternationalLicense()
        {
            this.ID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }
        private clsInternationalLicense(int ID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;


            Mode = enMode.Update;

            DriverInfo = clsDriver.GetDriverWithID(DriverID);
            LocalLicenseInfo = clsLicense.GetLicenseWithID(IssuedUsingLocalLicenseID);

        }



        private bool _AddNewInternationalLicense()
        {
            this.ID = clsInternationalLicensesDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return this.ID != -1;
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicensesDataAccess.UpdateInternationalLicense(this.ID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateInternationalLicense();


            }

            return false;

        }

        public static clsInternationalLicense GetInternationalLicenseWithID(int ID)
        {

            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;


            if (clsInternationalLicensesDataAccess.GetInternationalLicenseInfoByID(ID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))

                return new clsInternationalLicense(ID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;


        }

        public static clsInternationalLicense GetUserWithDriverID(int DriverID)
        {
            int ID = -1;
            int ApplicationID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;



            if (clsInternationalLicensesDataAccess.GetInternationalLicenseInfoByDriverID(ref ID, ref ApplicationID, DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))

                return new clsInternationalLicense(ID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;

        }

        public static bool DeleteAnInternationalLicense(int ID)
        {
            return clsInternationalLicensesDataAccess.DeleteInternationalLicense(ID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenses();

        }

        public static DataTable GetAllInternationalLicensesForDriver(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicensesForDriver(DriverID);
        }

        public static bool IsInternationalLicenseExist(int ID)
        {
            return clsInternationalLicensesDataAccess.IsInternationalLicenseExist(ID);
        }

        public static bool IsInternationalLicenseExistWithDriverID(int ID)
        {
            return clsInternationalLicensesDataAccess.IsInternationalLicenseExistWithDriverID(ID);
        }

        public static bool IsInternationalLicenseExistWithLocalLicenseIDActive(int ID, ref int InternationalLicenseID)
        {
            return clsInternationalLicensesDataAccess.IsInternationalLicenseExistWithLocalLicenseID(ID, ref InternationalLicenseID);
        }


    }
}
