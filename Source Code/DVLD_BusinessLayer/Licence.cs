using System;
using System.Data;
using DriversBusinessLayer;
using LicenseDataAccessLayer;
using LicenseClassBusinessLayer;

namespace LicenseBusinessLayer
{
    public class clsLicense
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };


        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo = new clsDriver();
        public int LicenseClass { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { set; get; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public int CreatedByUserID { get; set; }


        public clsLicense()
        {
            this.ID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }
        private clsLicense(int ID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;

            LicenseClassInfo = clsLicenseClass.GetALicenseClassByID(LicenseClass);
            DriverInfo = clsDriver.GetDriverWithID(DriverID);

        }


        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        private bool _AddNewLicense()
        {
            this.ID = clsLicensesDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate
                 , this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return this.ID != -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicensesDataAccess.UpdateLicense(this.ID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate
                 , this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicense();


            }

            return false;

        }

        public void CopyData(clsLicense FromLicense)
        {
            this.ID = FromLicense.ID;
            this.ApplicationID = FromLicense.ApplicationID;
            this.DriverID = FromLicense.DriverID;
            this.LicenseClass = FromLicense.LicenseClass;
            this.IssueDate = FromLicense.IssueDate;
            this.ExpirationDate = FromLicense.ExpirationDate;
            this.Notes = FromLicense.Notes;
            this.PaidFees = FromLicense.PaidFees;
            this.IsActive = FromLicense.IsActive;
            this.IssueReason = FromLicense.IssueReason;
            this.CreatedByUserID = FromLicense.CreatedByUserID;
            this.LicenseClassInfo = FromLicense.LicenseClassInfo;
            this.DriverInfo = FromLicense.DriverInfo;

        }

        public bool Deactivate()
        {
            return clsLicensesDataAccess.Deactivate(this.ID);
        }

        public bool IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool IsLicenseActive()
        {
            return this.IsActive;
        }


        public static clsLicense GetLicenseWithID(int ID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = 0;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;


            if (clsLicensesDataAccess.GetLicenseInfoByID(ID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(ID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees,
                    IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;


        }

        public static clsLicense GetLicenseWithDriverIDAndLicenseClass(int DriverID, string LicenseClassName)
        {
            int ID = -1, ApplicationID = -1, LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;


            if (clsLicensesDataAccess.GetLicenseWithDriverIDAndLicenseClassName(LicenseClassName, ref ID, ref ApplicationID, DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(ID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static clsLicense GetLicenseWithApplicationIDAndLicenseClassName(int ApplicationID, string LicenseClassName)
        {
            int ID = -1;
            int DriverID = -1;
            int LicenseClass = 0;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;


            if (clsLicensesDataAccess.GetLicenseInfoByApplicationIDAndLicenseClassName(LicenseClassName, ref ID, ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(ID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static DataTable GetAllLicenses()
        {
            return clsLicensesDataAccess.GetAllLicenses();

        }

        public static DataTable GetAllLocalLicensesForDriver(int DriverID)
        {
            return clsLicensesDataAccess.GetAllLocalLicensesForDriver(DriverID);
        }

        public static bool DeleteALicense(int ID)
        {
            return clsLicensesDataAccess.DeleteLicense(ID);
        }

        public static bool IsLicenseExist(int ID)
        {
            return clsLicensesDataAccess.IsLicenseExist(ID);
        }

        public static bool IsLicenseExistWithDriverID(int ID)
        {
            return clsLicensesDataAccess.IsLicenseExistWithDriverID(ID);
        }

        public static bool IsLicenseExistWithDriverIDAndLicenseClassID(int ID, int LicenseClassID)
        {
            return clsLicensesDataAccess.IsLicenseExistWithDriverIDAndLicenseClassID(ID, LicenseClassID);
        }

        public static bool IsThereLicenseHasThisApplicationID(int ApplicationID)
        {
            return clsLicensesDataAccess.IsThereLicenseHasThisApplicationID(ApplicationID);
        }

        public static bool DoesDriverHasAValidLicenseWithThisLicenseClass(int DriverID, int LicenseClassID, ref int ValidLicenseID, ref bool IsActive)
        {
            return clsLicensesDataAccess.DoesDriverHasAValidLicenseWithThisLicenseClass(DriverID, LicenseClassID, ref ValidLicenseID, ref IsActive);
        }





    }
}

