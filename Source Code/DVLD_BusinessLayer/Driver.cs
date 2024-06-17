using System;
using System.Data;
using PeopleBusinessLayer;
using DriversDataAccessLayer;

namespace DriversBusinessLayer
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ID { set; get; }
        public int PersonID { set; get; }
        public clsPerson Person { set; get; }
        public DateTime CreatedDate { set; get; }
        public int CreatedByUserID { set; get; }

        public clsDriver()
        {
            this.ID = -1;
            this.PersonID = -1;
            this.CreatedDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.Person = new clsPerson();
            Mode = enMode.AddNew;

        }
        private clsDriver(int ID, int PersonID, DateTime CreatedDate, int CreatedByUserID)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.CreatedDate = CreatedDate;
            this.CreatedByUserID = CreatedByUserID;
            this.Person = clsPerson.GetPerson(PersonID);
            Mode = enMode.Update;

        }

        private bool _AddNewDriver()
        {
            //// Data Layer Method & get The scopeIdentity key
            this.ID = clsDriversDataAccess.AddNewDriver(this.PersonID, this.CreatedDate, this.CreatedByUserID);

            return this.ID != -1;
        }

        private bool _UpdateDriver()
        {
            //// Data Layer Method & get The scopeIdentity key

            return clsDriversDataAccess.UpdateDriver(this.ID, this.PersonID, this.CreatedDate, this.CreatedByUserID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDriver();


            }

            return false;

        }

        public static clsDriver GetDriverWithID(int ID)
        {
            DateTime CreatedDate = DateTime.Now;
            int PersonID = -1, CreatedByUserID = -1;

            if (clsDriversDataAccess.GetDriverInfoByID(ID, ref PersonID, ref CreatedDate, ref CreatedByUserID))

                return new clsDriver(ID, PersonID, CreatedDate, CreatedByUserID);
            else
                return null;


        }

        public static clsDriver GetDriverWithPersonID(int PersonID)
        {
            DateTime CreatedDate = DateTime.Now;
            int ID = -1;
            int CreatedByUserID = -1;


            if (clsDriversDataAccess.GetDriverInfoByPersonID(ref ID, PersonID, ref CreatedDate, ref CreatedByUserID))

                return new clsDriver(ID, PersonID, CreatedDate, CreatedByUserID);
            else
                return null;

        }

        public static clsDriver GetDriverWithNationalNo(string NationalNo)
        {
            DateTime CreatedDate = DateTime.Now;
            int ID = -1, PersonID = -1;
            int CreatedByUserID = -1;


            if (clsDriversDataAccess.GetDriverInfoByNationalNo(ref ID, ref PersonID, NationalNo, ref CreatedDate, ref CreatedByUserID))

                return new clsDriver(ID, PersonID, CreatedDate, CreatedByUserID);
            else
                return null;

        }

        public static bool DeleteADriver(int ID)
        {
            return clsDriversDataAccess.DeleteDriver(ID);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.GetAllDrivers();

        }

        public static bool IsDriverExist(int ID)
        {
            return clsDriversDataAccess.IsDriverExist(ID);
        }

        public static bool IsDriverExist(string NationalNo)
        {
            return clsDriversDataAccess.IsDriverExist(NationalNo);
        }


        public static bool IsDriverExistWithPersonID(int ID)
        {
            return clsDriversDataAccess.IsDriverExistWithPersonID(ID);
        }

    }
}
