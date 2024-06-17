using System;
using System.Data;
using UsersBusinessLayer;
using PeopleBusinessLayer;
using ApplicationBusinessLayer;
using LicenseClassBusinessLayer;
using ApplicationDataAccessLayer;
using LocalDrivingLicenseApplicationDataAccessLayer;

namespace LocalDrivingLicenseApplicationBusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enMode LMode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public string FullName
        {
            get
            {
                return base.ApplicantFullName;
            }
        }

        public clsLicenseClass LicenseClassInfo;

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            LMode = enMode.AddNew;

        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int LicenseClassID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, clsUser CreatedByUserInfo)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = CreatedByUserInfo;
            this.LicenseClassInfo = clsLicenseClass.GetALicenseClassByID(LicenseClassID);
            LMode = enMode.Update;
        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationDataAccess.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.LicenseClassID, this.ApplicationID);
        }

        public static bool DeleteLocalDrivingLicenseApplication(int TargetID)
        {
            int ApplicationID = -1;

            ///// check first if the Application is exist and if it's get the ApplicationID
            if (clsLocalDrivingLicenseApplicationDataAccess.GetApplicationIDForThisLocalDrivingLicenseApplication(TargetID, ref ApplicationID))
            {
                if (clsLocalDrivingLicenseApplicationDataAccess.DeleteLocalDrivingLicenseApplication(TargetID))
                {
                    if (ApplicationID != -1)
                        return clsApplicationDataAccess.DeleteApplication(ApplicationID);
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

        }

        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationInfoByID(int TargetID)
        {

            int ApplicationID = -1;
            int LicenseClassID = 0;

            if (clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingLicenseApplicationInfoByID(TargetID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplication app = clsApplication.GetApplicationInfoByID(ApplicationID);


                return new clsLocalDrivingLicenseApplication(TargetID, LicenseClassID, ApplicationID, app.ApplicantPersonID, app.ApplicationDate,
                    app.ApplicationTypeID, (enApplicationStatus)app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID, app.CreatedByUserInfo);
            }

            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID)
        {

            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID = 0;

            if (clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingLicenseApplicationInfoByApplicationID(ref LocalDrivingLicenseApplicationID, ApplicationID, ref LicenseClassID))
            {
                clsApplication app = clsApplication.GetApplicationInfoByID(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID, ApplicationID, app.ApplicantPersonID, app.ApplicationDate,
                    app.ApplicationTypeID, (enApplicationStatus)app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID, app.CreatedByUserInfo);
            }

            else
                return null;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetAllLocalDrivingLicenseApplications();
        }

        public static bool IsLocalDrivingLicenseApplicationExist(int TargetID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.IsLocalDrivingLicenseApplicationExist(TargetID);
        }

        public static bool IsPersonAgeBelowAllowedAge(int PersonID, int LicenseClassID)
        {
            return (clsPerson.GetPersonAge(PersonID) < clsLicenseClass.GetMinimumAllowedAge(LicenseClassID));
        }

        public bool SaveLocalDrivingLicense()
        {

            if (LMode == enMode.Update)
                Mode = enMode.Update;

            if (!Save())
                return false;

            switch (LMode)
            {
                case enMode.AddNew:

                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        LMode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }
            return false;


        }

        public bool CopyLocalDrivingApplicationInfo(clsLocalDrivingLicenseApplication FromLocalDrivingLicenseApplication)
        {
            if (FromLocalDrivingLicenseApplication != null)
            {
                this.LocalDrivingLicenseApplicationID = FromLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
                this.LicenseClassID = FromLocalDrivingLicenseApplication.LicenseClassID;
                this.ApplicationID = FromLocalDrivingLicenseApplication.ApplicationID;
                this.ApplicantPersonID = FromLocalDrivingLicenseApplication.ApplicantPersonID;
                this.ApplicationDate = FromLocalDrivingLicenseApplication.ApplicationDate;
                this.ApplicationTypeID = FromLocalDrivingLicenseApplication.ApplicationTypeID;
                this.ApplicationStatus = FromLocalDrivingLicenseApplication.ApplicationStatus;
                this.LastStatusDate = FromLocalDrivingLicenseApplication.LastStatusDate;
                this.PaidFees = FromLocalDrivingLicenseApplication.PaidFees;
                this.CreatedByUserID = FromLocalDrivingLicenseApplication.CreatedByUserID;
                this.LicenseClassInfo = FromLocalDrivingLicenseApplication.LicenseClassInfo;
                return true;
            }



            return false;

        }


    }
}
