using System;
using System.Data;
using PeopleDataAccessLayer;

namespace PeopleBusinessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }
        public string ImagePath { set; get; }



        public clsPerson()
        {
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.FullName = FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            this.DateOfBirth = DateTime.Now;
            this.Gender = "";
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
        }
        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, string Gender, int NationalityCountryID, string ImagePath)

        {

            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.FullName = FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;


            Mode = enMode.Update;

        }



        private bool _AddNewPerson()
        {

            //// Data Layer Method & get The scopeIdentity key
            this.ID = clsPersonDataAccess.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
            this.Email, this.Phone, this.Address, this.DateOfBirth, this.Gender, this.NationalityCountryID, this.ImagePath);

            return this.ID != -1;
        }

        private bool _UpdatePerson()
        {
            //// Data Layer Method & get The scopeIdentity key

            return clsPersonDataAccess.UpdatePerson(this.ID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
            this.Email, this.Phone, this.Address, this.DateOfBirth, this.Gender, this.NationalityCountryID, this.ImagePath);

        }

        public void CopyPersonInfo(clsPerson Person)
        {
            this.ID = Person.ID;
            this.NationalNo = Person.NationalNo;
            this.FirstName = Person.FirstName;
            this.SecondName = Person.SecondName;
            this.ThirdName = Person.ThirdName;
            this.LastName = Person.LastName;
            this.DateOfBirth = Person.DateOfBirth;
            this.Gender = Person.Gender;
            this.Address = Person.Address;
            this.Phone = Person.Phone;
            this.Email = Person.Email;
            this.NationalityCountryID = Person.NationalityCountryID;
            this.ImagePath = Person.ImagePath;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();


            }

            return false;

        }

        public static bool IsPersonsInfoDuplicated(clsPerson person1, clsPerson person2)
        {
            if (person1.ID != person2.ID) return false;
            else if (person1.NationalNo.Trim() != person2.NationalNo.Trim()) return false;
            else if (person1.FirstName.Trim() != person2.FirstName.Trim()) return false;
            else if (person1.SecondName.Trim() != person2.SecondName.Trim()) return false;
            else if (person1.ThirdName.Trim() != person2.ThirdName.Trim()) return false;
            else if (person1.LastName.Trim() != person2.LastName.Trim()) return false;
            else if (person1.DateOfBirth != person2.DateOfBirth) return false;
            else if (person1.Gender.Trim() != person2.Gender.Trim()) return false;
            else if (person1.Address.Trim() != person2.Address.Trim()) return false;
            else if (person1.Phone.Trim() != person2.Phone.Trim()) return false;
            else if (person1.Email.Trim() != person2.Email.Trim()) return false;
            else if (person1.NationalityCountryID != person2.NationalityCountryID) return false;
            else if (person1.ImagePath.Trim() != person2.ImagePath.Trim()) return false;

            return true;
        }

        public static clsPerson GetPerson(int ID)
        {
            string NationalNo = "", SecondName = "", ThirdName = "", FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            string Gender = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;

            if (clsPersonDataAccess.GetPersonInfoByID(ID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref
                                                      Email, ref Phone, ref Address, ref DateOfBirth, ref Gender, ref NationalityCountryID, ref ImagePath))

                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, Email, Phone, Address, DateOfBirth, Gender, NationalityCountryID, ImagePath);

            else
                return null;

        }

        public static clsPerson GetPerson(string NationalNo)
        {
            string Gender = "", SecondName = "", ThirdName = "", FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int ID = -1;
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;

            if (clsPersonDataAccess.GetPersonInfoByNationalNo(ref ID, NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref
                                      Email, ref Phone, ref Address, ref DateOfBirth, ref Gender, ref NationalityCountryID, ref ImagePath))

                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                      Email, Phone, Address, DateOfBirth, Gender, NationalityCountryID, ImagePath);
            else
                return null;

        }

        public static bool DeleteAPerson(int ID)
        {
            return clsPersonDataAccess.DeletePerson(ID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();

        }

        public static DataTable GetFilterdPeopleBy(string AttributeName, string AttributeValue)
        {
            return clsPersonDataAccess.GetFilterdPeople(AttributeName, AttributeValue);

        }

        public static bool IsPersonExist(int ID)
        {

            return clsPersonDataAccess.IsPersonExist(ID);

        }

        public static bool IsNationalNoUnique(string NationalNo)
        {
            return clsPersonDataAccess.IsNationalNoUnique(NationalNo);

        }

        public static int GetPersonAge(int ID)
        {
            return clsPersonDataAccess.GetPersonAge(ID);
        }



    }
}