using System.Data;
using PeopleDataAccessLayer;

namespace PeopleBusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int CountryID { set; get; }
        public string CountryName { set; get; }


        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;


            Mode = enMode.Update;
        }
        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;
        }


        private bool _AddNewCountry(string TargetCountryName)
        {

            this.CountryID = clsCountryDataAccess.AddNewCountryWithName(TargetCountryName);

            return CountryID != -1;
        }
        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.CountryID, this.CountryName);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry(this.CountryName))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCountry();


            }

            return false;

        }


        public static clsCountry GetCountry(string TargetCountryName)
        {
            int CountryID = -1;

            if (clsCountryDataAccess.GetCountryWithName(ref TargetCountryName, ref CountryID))

                return new clsCountry(CountryID, TargetCountryName);

            else
                return null;
        }
        public static clsCountry GetCountry(int CountryID)
        {
            string CountryName = "";

            if (clsCountryDataAccess.GetCountryWithID(CountryID, ref CountryName))

                return new clsCountry(CountryID, CountryName);
            else
                return null;

        }
        public static bool IsCountryExistWithName(string TargetCountryName)
        {

            return clsCountryDataAccess.IsCountryExist(TargetCountryName);

        }
        public static bool IsCountryExistWithID(int ID)
        {

            return clsCountryDataAccess.IsCountryExist(ID);

        }
        public static bool DeleteCountry(int ID)
        {
            return clsCountryDataAccess.DeleteCountry(ID);
        }
        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();

        }

    }

}
