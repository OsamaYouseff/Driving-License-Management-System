using System;
using System.Data;
using UsersBusinessLayer;
using PeopleBusinessLayer;
using ApplicationDataAccessLayer;

namespace ApplicationBusinessLayer
{
    public class clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewLocalDrivingLicenseService = 1, RenewDrivingLicenseService = 2, ReplacementforaLostDrivingLicense = 3,
            ReplacementforaDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enApplicationStatus
        {
            New = 1, Cancelled = 2, Completed = 3
        };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.GetPerson(ApplicantPersonID).FullName;
            }

        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";

                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";

                }

            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo;



        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate
                , int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {

            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.GetUserWithID(CreatedByUserID);
            Mode = enMode.Update;


        }


        private bool _AddNewApplication()
        {

            this.ApplicationID = clsApplicationDataAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate,
                                                          this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return this.ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationDataAccess.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate
                                                    , this.PaidFees, this.CreatedByUserID);

        }

        public static bool DeleteApplication(int TargetID)
        {
            return clsApplicationDataAccess.DeleteApplication(TargetID);

        }

        public static bool IsApplicationExist(int TargetID)
        {
            return clsApplicationDataAccess.IsApplicationExist(TargetID);
        }

        public static bool IsApplicationExistWithSpaceficAttributes(int TargetApplicationID, int ApplicationType, int ApplicationStatus)
        {
            return clsApplicationDataAccess.IsApplicationExistWithSpaceficAttributes(TargetApplicationID, ApplicationType, ApplicationStatus);
        }

        public static bool IsApplicationExistWithApplicationType(int TargetID, int ApplicationType)
        {
            return clsApplicationDataAccess.IsApplicationExistWithApplicationType(TargetID, ApplicationType);
        }

        public static bool DoesPersonAlreadyHaveALocalApplicationActiveOrCompleted(int PersonID, int ApplicationTypeID, ref int AppLicationID)
        {
            return clsApplicationDataAccess.DoesPersonAlreadyHaveALocalApplicationActiveOrCompleted(PersonID, ApplicationTypeID, ref AppLicationID);

        }

        public static clsApplication GetApplicationInfoByID(int TargetID)
        {

            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;



            if (clsApplicationDataAccess.GetApplicationInfoByID(TargetID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))

                return new clsApplication(TargetID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;



        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationDataAccess.GetAllApplications();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplication();


            }

            return false;

        }

        public bool Cancel()
        {
            return clsApplicationDataAccess.UpdateStatus(ApplicationID, 2);
        }

        public bool SetCompleted()
        {
            return clsApplicationDataAccess.UpdateStatus(ApplicationID, 3);
        }


    }


}
