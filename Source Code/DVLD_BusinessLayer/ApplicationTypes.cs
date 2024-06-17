using System;
using System.Data;
using ApplicationDataAccessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationBusinessLayer
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;




        public int ApplicationID { set; get; }
        public string ApplicationName { set; get; }
        public float ApplicationFees { set; get; }


        public clsApplicationTypes()
        {
            ApplicationID = -1;
            ApplicationName = "";
            ApplicationFees = 0;

            Mode = enMode.AddNew;
        }
        private clsApplicationTypes(int ApplicationID, string ApplicationName, float applicationFees)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationName = ApplicationName;
            this.ApplicationFees = applicationFees;

            Mode = enMode.Update;
        }


        //// Methods

        private bool _AddNewApplicationType(string TargetApplicationName, float ApplicationFees)
        {

            this.ApplicationID = clsApplicationTypesDataAccess.AddNewApplicationTypeWithName(TargetApplicationName, ApplicationFees);

            return ApplicationID != -1;
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationType(this.ApplicationID, this.ApplicationName, this.ApplicationFees);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType(this.ApplicationName, this.ApplicationFees))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();


            }

            return false;

        }
        public void CopyData(clsApplicationTypes FromApp)
        {
            this.ApplicationID = FromApp.ApplicationID;
            this.ApplicationName = FromApp.ApplicationName;
            this.ApplicationFees = FromApp.ApplicationFees;
        }


        public static clsApplicationTypes GetApplicationType(string TargetApplicationName)
        {
            int ApplicationID = -1;
            float ApplicationFees = 0;

            if (clsApplicationTypesDataAccess.GetApplicationTypeWithName(TargetApplicationName, ref ApplicationID, ref ApplicationFees))

                return new clsApplicationTypes(ApplicationID, TargetApplicationName, ApplicationFees);

            else
                return null;
        }
        public static clsApplicationTypes GetApplicationType(int ApplicationID)
        {
            string ApplicationName = "";
            float ApplicationFees = 0;

            if (clsApplicationTypesDataAccess.GetApplicationTypeWithID(ApplicationID, ref ApplicationName, ref ApplicationFees))

                return new clsApplicationTypes(ApplicationID, ApplicationName, ApplicationFees);
            else
                return null;

        }
        public static bool IsApplicationTypeExistWithName(string TargetApplicationName)
        {

            return clsApplicationTypesDataAccess.IsApplicationTypeExist(TargetApplicationName);

        }
        public static bool IsApplicationTypeExistWithID(int ID)
        {

            return clsApplicationTypesDataAccess.IsApplicationTypeExist(ID);

        }
        public static bool DeleteApplicationType(int ID)
        {
            return clsApplicationTypesDataAccess.DeleteApplicationType(ID);
        }
        public static DataTable GetAllApplicationsTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationsTypes();

        }
        public static DataTable GetFilterdApplicationsBy(string AttributeName, string AttributeValue)
        {
            return clsApplicationTypesDataAccess.GetFilterdApplicationsBy(AttributeName, AttributeValue);
        }
        public static bool AreTheyDuplicated(clsApplicationTypes App1, clsApplicationTypes App2)
        {
            if (App1.ApplicationID != App2.ApplicationID) return false;
            else if (App1.ApplicationName != App2.ApplicationName) return false;
            else if (App1.ApplicationFees != App2.ApplicationFees) return false;

            return true;
        }



    }
}
