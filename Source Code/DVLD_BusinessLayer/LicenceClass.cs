using System.Data;
using LicenseClassDataAccessLayer;

namespace LicenseClassBusinessLayer
{
    public class clsLicenseClass
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }



        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;

            Mode = enMode.AddNew;

        }

        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            Mode = enMode.Update;

        }

        public static clsLicenseClass GetALicenseClassByClassName(string LicenseClassName)
        {
            int LicanseClassID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            float ClassFees = 0;



            if (clsLicenseClassDataAccess.GetLicenseClassWithName(ref LicanseClassID, LicenseClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClass(LicanseClassID, LicenseClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }

        public static clsLicenseClass GetALicenseClassByID(int TagetLicenseClassID)
        {

            string ClassName = "";
            string ClassDescription = "";
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            float ClassFees = 0;



            if (clsLicenseClassDataAccess.GetLicenseClassInfoByID(TagetLicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClass(TagetLicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;


        }

        private bool _AddNewLicenseClass()
        {

            this.LicenseClassID = clsLicenseClassDataAccess.AddNewLicenseClassWithName(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

            return this.LicenseClassID != -1;
        }

        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassDataAccess.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

        }

        public static bool DeleteLicenseClass(int TargetID)
        {
            return clsLicenseClassDataAccess.DeleteLicenseClass(TargetID);

        }

        public static bool IsLicenseClassExistWithID(int TargetID)
        {
            return clsLicenseClassDataAccess.IsLicenseClassExist(TargetID);
        }

        public static bool IsLicenseClassExistWithName(string TargetName)
        {
            return clsLicenseClassDataAccess.IsLicenseClassExist(TargetName);
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassDataAccess.GetAllLicenseClasses();
        }

        public static DataTable GetFilterdLicenseClassesBy(string AttributeName, string AttributeValue)
        {
            return clsLicenseClassDataAccess.GetFilterdLicenseClassesBy(AttributeName, AttributeValue);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicenseClass();
            }
            return false;

        }

        public static int GetMinimumAllowedAge(int ID)
        {
            return clsLicenseClassDataAccess.GetMinimumAllowedAge(ID);
        }

    }
}


