using System;
using System.Data;
using UsersBusinessLayer;
using TesttBusinessLayer;
using PeopleBusinessLayer;
using DriversBusinessLayer;
using LicenseBusinessLayer;
using TestTypesBusinessLayer;
using ApplicationBusinessLayer;
using LicenseClassBusinessLayer;
using TestAppointmentBusinessLayer;
using DetainedLicenseBusinessLayer;
using InternationalLicenseBusinessLayer;
using LocalDrivingLicenseApplicationBusinessLayer;

namespace DVLDConsoleApp
{
    internal class Program
    {

        //// Person Methods --------------------------------------------------------------

        static void AddNewPerson()
        {
            clsPerson Person1 = new clsPerson();
            Person1.NationalNo = "N13";
            Person1.FirstName = "Fadi";
            Person1.SecondName = "Mohammed";
            Person1.ThirdName = "Ali";
            Person1.LastName = "Mohammed";
            Person1.Email = "A@a.com";
            Person1.Phone = "0100142330";
            Person1.Address = "address1";
            Person1.Gender = "Male";
            Person1.DateOfBirth = new DateTime(1987, 10, 6, 5, 20, 0);
            Person1.NationalityCountryID = 1;
            Person1.ImagePath = "";


            //// check if NationalNum is unique or not
            ///
            if (!clsPerson.IsNationalNoUnique(Person1.NationalNo))
            {
                Console.WriteLine("The National Num is Already Exist before try another one ");
                return;
            }


            if (Person1.Save())
                Console.WriteLine("Person Added Successfully with id = [ " + Person1.ID + " ] \n");
            else
                Console.WriteLine("Faild To add this Person \n");
        }

        static void GetPersonWithID(int ID)

        {
            clsPerson Person1 = clsPerson.GetPerson(ID);

            if (Person1 != null)
            {

                Console.WriteLine(Person1.NationalNo);
                Console.WriteLine(Person1.FirstName + " " + Person1.SecondName + " " + Person1.ThirdName + " " + Person1.LastName);
                Console.WriteLine(Person1.Email);
                Console.WriteLine(Person1.Phone);
                Console.WriteLine(Person1.Address);
                Console.WriteLine(Person1.Gender);
                Console.WriteLine(Person1.DateOfBirth);
                Console.WriteLine(Person1.NationalityCountryID);
                Console.WriteLine(Person1.ImagePath);
            }
            else
            {
                Console.WriteLine("Person [" + ID + "] Not found!");
            }
        }

        static void GetPersonWithNationalNo(string NationalNo)

        {
            clsPerson Person1 = clsPerson.GetPerson(NationalNo);

            if (Person1 != null)
            {

                Console.WriteLine(Person1.ID);
                Console.WriteLine(Person1.NationalNo);
                Console.WriteLine(Person1.FirstName + " " + Person1.SecondName + " " + Person1.ThirdName + " " + Person1.LastName);
                Console.WriteLine(Person1.Email);
                Console.WriteLine(Person1.Phone);
                Console.WriteLine(Person1.Address);
                Console.WriteLine(Person1.Gender);
                Console.WriteLine(Person1.DateOfBirth);
                Console.WriteLine(Person1.NationalityCountryID);
                Console.WriteLine(Person1.ImagePath);
            }
            else
            {
                Console.WriteLine("Person [" + NationalNo + "] Not found!");
            }
        }

        static void UpdatePerson(int ID)
        {

            clsPerson Person1 = clsPerson.GetPerson(ID);



            if (clsPerson.IsPersonExist(ID))
            {
                string OldNationalNum = Person1.NationalNo;



                Person1.NationalNo = "N55";
                Person1.FirstName = "Donia";
                Person1.SecondName = "Seif";
                Person1.ThirdName = "Ali";
                Person1.LastName = "Mohammed";
                Person1.Email = "SA@a.com";
                Person1.Phone = "03434343443";
                Person1.Gender = "Female";
                Person1.Address = "address2";
                Person1.DateOfBirth = new DateTime(1987, 10, 6, 5, 20, 0);
                Person1.NationalityCountryID = 90;
                Person1.ImagePath = "C:\\Photos\\Mohammed AbuHadhoud.JPG";


                //// check if NationalNum is unique or not in case the user updated it
                if (Person1.NationalNo != OldNationalNum)
                {
                    if (!clsPerson.IsNationalNoUnique(Person1.NationalNo))
                    {
                        Console.WriteLine("The National Num is Already Exist before try another one ");
                        return;
                    }
                }


                if (Person1.Save())
                {
                    Console.WriteLine("Person Updated with id = [ " + Person1.ID + " ] Successfully\n");
                }
            }
            else
            {
                Console.WriteLine("Faild To update Person with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void DeletePerson(int ID)
        {
            if (clsPerson.IsPersonExist(ID))
            {
                if (clsPerson.DeleteAPerson(ID))
                    Console.WriteLine("Person with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete Person with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }

            else
                Console.WriteLine("Person with id [ " + ID + " ] is not exist :-( ");





        }

        static void ListPersons()
        {
            DataTable dataTable = clsPerson.GetAllPeople();

            Console.WriteLine("People Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["PersonID"]} | {row["NationalNo"]} | {row["FirstName"]} | {row["SecondName"]} | {row["ThirdName"]} | {row["LastName"]} " +
                    $"| {row["Email"]} | {row["DateOfBirth"]}  | {row["Gender"]} | {row["Address"]} ");

            }

        }

        static void IsPersonExist(int ID)
        {
            if (clsPerson.IsPersonExist(ID))
                Console.WriteLine("Person with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("Person with id [ " + ID + " ] is not exist :-( ");
        }

        static void GetPersonAge(int ID)
        {
            if (clsPerson.IsPersonExist(ID))
            {
                Console.WriteLine("Person Age : " + clsPerson.GetPersonAge(ID));
            }

            else
                Console.WriteLine("Person with id [ " + ID + " ] is not exist :-( ");
        }


        ////// -------- Filter Persons By -------------------------------

        static void ListPersonsFilterdPersons(string AttributeName, string AttributeValue)
        {
            DataTable dataTable = clsPerson.GetFilterdPeopleBy(AttributeName, AttributeValue);


            Console.WriteLine("People Data:");



            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["PersonID"]} | {row["NationalNo"]} | {row["FirstName"]} | {row["SecondName"]} | {row["ThirdName"]} | {row["LastName"]} " +
                    $"| {row["Email"]} | {row["DateOfBirth"]}  | {row["Gender"]} | {row["Address"]} ");

            }
        }



        //// User Methods --------------------------------------------------------------

        static void AddNewUser()
        {
            clsUser User1 = new clsUser();
            User1.PersonID = 2062;
            User1.UserName = "Mohammed";
            User1.Password = "1234";
            User1.IsActive = false;

            ////// check if User Id is exist and not used before

            if (!clsPerson.IsPersonExist(User1.PersonID))
            {
                Console.Write("Person With ID [ " + User1.PersonID + " ] is not exist in system ,so you can't use it.\n");
                return;
            }

            if (clsUser.IsUserExistWithPersonID(User1.PersonID))
            {
                Console.Write("You Can't use this Person ID Because it's Belong to another User in system.\n");
                return;
            }

            //// save to Database
            if (User1.Save())
                Console.WriteLine("User Added Successfully with id = [ " + User1.ID + " ] \n");
            else
                Console.WriteLine("Faild To add this Person \n");
        }

        static void GetUserWithID(int ID)

        {
            clsUser User1 = clsUser.GetUserWithID(ID);

            if (User1 != null)
            {

                Console.WriteLine(User1.PersonID);
                Console.WriteLine(User1.UserName);
                Console.WriteLine(User1.Password);
                Console.WriteLine(User1.IsActive);
            }
            else
            {
                Console.WriteLine("User [" + ID + "] Not found!");
            }
        }

        static void GetUserWithPersonID(int PersonID)

        {
            clsUser User1 = clsUser.GetUserWithPersonID(PersonID);

            if (User1 != null)
            {

                Console.WriteLine(User1.PersonID);
                Console.WriteLine(User1.UserName);
                Console.WriteLine(User1.Password);
                Console.WriteLine(User1.IsActive);
            }
            else
            {
                Console.WriteLine("User with Person ID [" + PersonID + "] Not found!");
            }
        }

        static void GetUserWithUserNameAndPassword(string UserName, string Password)

        {
            clsUser User1 = clsUser.GetUserWithUserNameAndPassword(UserName, Password);

            if (User1 != null)
            {

                Console.WriteLine(User1.PersonID);
                Console.WriteLine(User1.UserName);
                Console.WriteLine(User1.Password);
                Console.WriteLine(User1.IsActive);
            }
            else
            {
                Console.WriteLine("User with Person ID [" + User1.PersonID + "] Not found!");
            }
        }

        static void UpdateUser(int ID)
        {

            clsUser User1 = clsUser.GetUserWithID(ID);


            if (clsUser.IsUserExist(ID))
            {
                int OriginalPersonID = User1.PersonID;

                User1.PersonID = 2058;
                User1.UserName = "Donia";
                User1.Password = "3333";
                User1.IsActive = true;


                ////// Make sure that Updated person id is not used before in case if it was updated 
                if (OriginalPersonID != User1.PersonID)
                {
                    ////check if User Id is exist and not used before

                    if (!clsPerson.IsPersonExist(User1.PersonID))
                    {
                        Console.Write("Person With ID [ " + User1.PersonID + " ] is not exist in system ,so you can't use it.\n");
                        return;
                    }

                    if (clsUser.IsUserExistWithPersonID(User1.PersonID))
                    {
                        Console.Write("You Can't use this Person ID Because it's Belong to another User in system.\n");
                        return;
                    }
                }



                if (User1.Save())
                {
                    Console.WriteLine("User Updated with id = [ " + User1.ID + " ] Successfully\n");
                }
            }
            else
            {
                Console.WriteLine("Faild To update User with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void DeleteUser(int ID)
        {
            if (clsUser.IsUserExist(ID))
            {
                if (clsUser.DeleteUser(ID))
                    Console.WriteLine("User with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete User with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("User with id [ " + ID + " ] is not exist :-( ");

        }

        static void ListUsers()
        {
            DataTable dataTable = clsUser.GetAllUsers();

            Console.WriteLine("Users Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["UserID"]} | {row["PersonID"]} | {row["FullName"]} | {row["IsActive"]} ");
            }

        }

        static void IsUserExist(int ID)
        {
            if (clsUser.IsUserExist(ID))
                Console.WriteLine("User with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("User with id [ " + ID + " ] is not exist :-( ");
        }

        static void IsUserExistWithUserName(string UserName)
        {
            if (clsUser.IsUserExistWithUserName(UserName))
                Console.WriteLine("User with UserName [ " + UserName + " ] is exist :-) ");

            else
                Console.WriteLine("User with UserName [ " + UserName + " ] is not exist :-( ");
        }

        static void ChangePassword(int UserID, string Password)
        {
            if (clsUser.ChangePassword(UserID, Password))
                Console.WriteLine("Password for  UserID [ " + UserID + " ] is Updated Scuucessfully :-) ");

            else
                Console.WriteLine("User with UserID [ " + UserID + " ] is not exist :-( ");
        }

        static void ListUsersFilterdUsers(string AttributeName, string AttributeValue)
        {
            DataTable dataTable = clsUser.GetFilterdUsersBy(AttributeName, AttributeValue);

            Console.WriteLine("Users Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["UserID"]} | {row["PersonID"]} | {row["FullName"]} | {row["IsActive"]} ");
            }
        }


        //// Applications Types Methods --------------------------------------------------------------


        static void AddNewApplicationType()
        {
            clsApplicationTypes ApplicationType = new clsApplicationTypes();
            ApplicationType.ApplicationName = "Test";
            ApplicationType.ApplicationFees = 20.4f;


            //// check if ApplicationsType Name is unique or not
            if (clsApplicationTypes.IsApplicationTypeExistWithName(ApplicationType.ApplicationName))
            {
                Console.WriteLine("The ApplicationType is Already Exist before try another one ");
                return;
            }


            if (ApplicationType.Save())
                Console.WriteLine("ApplicationType Added Successfully with id = [ " + ApplicationType.ApplicationID + " ] \n");
            else
                Console.WriteLine("Faild To add this ApplicationType \n");
        }

        static void GetAnApplicationTypeWithID(int ID)

        {
            clsApplicationTypes ApplicationType = clsApplicationTypes.GetApplicationType(ID);

            if (ApplicationType != null)
            {

                Console.WriteLine(ApplicationType.ApplicationID);
                Console.WriteLine(ApplicationType.ApplicationName);
                Console.WriteLine(ApplicationType.ApplicationFees);
            }
            else
            {
                Console.WriteLine("ApplicationType [" + ID + "] Not found!");
            }
        }

        static void GetAnApplicationTypeWithTitle(string TargetApplicationName)

        {
            clsApplicationTypes ApplicationType = clsApplicationTypes.GetApplicationType(TargetApplicationName);

            if (ApplicationType != null)
            {

                Console.WriteLine(ApplicationType.ApplicationID);
                Console.WriteLine(ApplicationType.ApplicationName);
                Console.WriteLine(ApplicationType.ApplicationFees);
            }
            else
            {
                Console.WriteLine("ApplicationType [" + TargetApplicationName + "] Not found!");
            }
        }

        static void UpdateApplicationType(int ID)
        {

            clsApplicationTypes ApplicationType = clsApplicationTypes.GetApplicationType(ID);



            if (clsApplicationTypes.IsApplicationTypeExistWithID(ID))
            {

                int OriginalPersonID = ApplicationType.ApplicationID;

                ApplicationType.ApplicationName = "Test1";
                ApplicationType.ApplicationFees = 30.9f;


                //////  Make sure that Updated ApplicationType id is not used before in case if it was updated 
                if (OriginalPersonID != ApplicationType.ApplicationID && clsApplicationTypes.IsApplicationTypeExistWithID(ApplicationType.ApplicationID))
                {
                    Console.Write("You Can't use this ApplicationID Because it's Belong to another ApplicationType in system.\n");
                    return;
                }
                ////check if ApplicationType NAme is Not exist before
                if (clsApplicationTypes.IsApplicationTypeExistWithName(ApplicationType.ApplicationName))
                {
                    Console.Write("You Can't use this ApplicationName Because it's Belong to another ApplicationType in system.\n");
                    return;
                }



                if (ApplicationType.Save())
                {
                    Console.WriteLine("ApplicationType Updated with id = [ " + ApplicationType.ApplicationID + " ] Successfully\n");
                }
            }
            else
            {
                Console.WriteLine("Faild To update ApplicationType with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void IsApplicationTypeExistWithName(string ApplicationName)
        {
            if (clsApplicationTypes.IsApplicationTypeExistWithName(ApplicationName))
                Console.WriteLine("ApplicationType with ApplicationName [ " + ApplicationName + " ] is exist :-) ");

            else
                Console.WriteLine("ApplicationType with ApplicationName [ " + ApplicationName + " ] is not exist :-( ");
        }

        static void DeleteApplicationType(int ID)
        {
            if (clsApplicationTypes.IsApplicationTypeExistWithID(ID))
            {
                if (clsApplicationTypes.DeleteApplicationType(ID))
                    Console.WriteLine("ApplicationType with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete ApplicationType with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("ApplicationType with id [ " + ID + " ] is not exist :-( ");

        }

        static void ApplicationTypesList()
        {
            DataTable dataTable = clsApplicationTypes.GetAllApplicationsTypes();

            Console.WriteLine("ApplicationTypes Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ApplicationTypeID"]} | {row["ApplicationTypeTitle"]} | {row["ApplicationFees"]}");

            }

        }

        static void FilterdApplicationTypesList(string AttributeName, string AttributeValue)
        {
            DataTable dataTable = clsApplicationTypes.GetFilterdApplicationsBy(AttributeName, AttributeValue);

            Console.WriteLine("Application Types Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ApplicationTypeID"]} | {row["ApplicationTypeTitle"]} | {row["ApplicationFees"]}");

            }
        }



        //// Test Types Methods --------------------------------------------------------------


        static void AddNewTestType()
        {
            clsTestType TestType = new clsTestType();
            TestType.TestName = "Test";
            TestType.TestDescription = "This is a description";
            TestType.TestFees = 20.4f;


            //// check if NationalNum is unique or not
            if (clsTestType.IsTestTypeExistWithName(TestType.TestName))
            {
                Console.WriteLine("The TestType is Already Exist before try another one ");
                return;
            }


            if (TestType.Save())
                Console.WriteLine("TestType Added Successfully with id = [ " + TestType.TestID + " ] \n");
            else
                Console.WriteLine("Faild To add this ApplicationType \n");
        }

        static void GetAnTestTypeWithID(int ID)

        {
            clsTestType TestType = clsTestType.GetTestType(ID);

            if (TestType != null)
            {

                Console.WriteLine(TestType.TestID);
                Console.WriteLine(TestType.TestName);
                Console.WriteLine(TestType.TestDescription);
                Console.WriteLine(TestType.TestFees);
            }
            else
            {
                Console.WriteLine("TestType [" + ID + "] Not found!");
            }
        }

        static void GetAnTestTypeWithTitle(string TargetTestName)

        {
            clsTestType TestType = clsTestType.GetTestType(TargetTestName);

            if (TestType != null)
            {

                Console.WriteLine(TestType.TestID);
                Console.WriteLine(TestType.TestName);
                Console.WriteLine(TestType.TestDescription);
                Console.WriteLine(TestType.TestFees);
            }
            else
            {
                Console.WriteLine("Test Type [" + TargetTestName + "] Not found!");
            }
        }

        static void UpdateTestType(int ID)
        {

            clsTestType TestType = clsTestType.GetTestType(ID);



            if (clsTestType.IsTestTypeExistWithID(ID))
            {
                TestType.TestName = "Test2";
                TestType.TestDescription = "Desc 1";
                TestType.TestFees = 30.9f;


                ////check if TestType Name is Not exist before
                if (clsTestType.IsTestTypeExistWithName(TestType.TestName))
                {
                    Console.Write("You Can't use this Test Name Because it's Belong to another TestType in system.\n");
                    return;
                }



                if (TestType.Save())
                {
                    Console.WriteLine("TestType Updated with id = [ " + TestType.TestID + " ] Successfully\n");
                }
            }
            else
            {
                Console.WriteLine("Faild To update TestType with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void IsTestTypeExistWithTitle(string TestName)
        {
            if (clsTestType.IsTestTypeExistWithName(TestName))
                Console.WriteLine("TestType with TestName [ " + TestName + " ] is exist :-) ");

            else
                Console.WriteLine("TestType with TestName [ " + TestName + " ] is not exist :-( ");
        }

        static void DeleteTestType(int ID)
        {
            if (clsTestType.IsTestTypeExistWithID(ID))
            {
                if (clsTestType.DeleteTestType(ID))
                    Console.WriteLine("TestType with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete TestType with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("TestType with id [ " + ID + " ] is not exist :-( ");

        }

        static void TestTypesList()
        {
            DataTable dataTable = clsTestType.GetAllTestsTypes();

            Console.WriteLine("TestTypes Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["TestTypeID"]} | {row["TestTypeTitle"]} | {row["TestTypeDescription"]} | {row["TestTypeFees"]}");

            }

        }

        static void FilterdTestTypesList(string AttributeName, string AttributeValue)
        {
            DataTable dataTable = clsTestType.GetFilterdUsersBy(AttributeName, AttributeValue);

            Console.WriteLine("Test Types Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["TestTypeID"]} | {row["TestTypeTitle"]} | {row["TestTypeDescription"]} | {row["TestTypeFees"]}");

            }
        }


        //// Applications Methods --------------------------------------------------------------
        static void AddNewApplication()
        {
            clsApplication application = new clsApplication();
            application.ApplicationTypeID = 1;
            application.ApplicantPersonID = 2066;
            application.PaidFees = 20.4f;
            application.CreatedByUserID = 1;


            ///// Check if there is an Application for the same person and it is in progress or not completed
            if (!DoesUserHaveAnActiveApplication(application.ApplicantPersonID, (byte)application.ApplicationTypeID))
            {
                if (application.Save())
                    Console.WriteLine("TestApplication Added Successfully with id = [ " + application.ApplicationID + " ] \n");
                else
                    Console.WriteLine("Faild To add this ApplicationType \n");

            }

        }

        static void UpdateApplication(int ID)
        {

            clsApplication application = clsApplication.GetApplicationInfoByID(ID);


            if (clsApplication.IsApplicationExist(ID))
            {

                int OriginalApplicantPersonID = application.ApplicantPersonID;
                application.ApplicationTypeID = 3;
                application.ApplicantPersonID = 1025;
                application.PaidFees = 35.4f;
                application.CreatedByUserID = 1;


                int LoadedApplicationID = -1;

                /////  Make sure that Updated ApplicantPersonID is not used before in active Application or completed application with the same type
                if (OriginalApplicantPersonID != application.ApplicantPersonID
                    && clsApplication.DoesPersonAlreadyHaveALocalApplicationActiveOrCompleted(application.ApplicantPersonID, application.ApplicationTypeID, ref LoadedApplicationID))
                {
                    Console.Write("You Can't use this ApplicantPersonID Because it's Belong to another Active application in system.\n");
                    return;
                }




                if (application.Save())
                {
                    Console.WriteLine("application Updated with id = [ " + application.ApplicationID + " ] Successfully\n");
                }
            }
            else
            {
                Console.WriteLine("Faild To update application with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void DeleteApplication(int ID)
        {
            if (clsApplication.IsApplicationExist(ID))
            {
                if (clsApplication.DeleteApplication(ID))
                    Console.WriteLine("Application with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete Application with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("Application with id [ " + ID + " ] is not exist :-( \n");

        }

        static void ApplicationsList()
        {
            DataTable dataTable = clsApplication.GetAllApplications();

            Console.WriteLine("Applications Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ApplicationID"]} | {row["ApplicantPersonID"]} | {row["ApplicationDate"]} " +
                    $"| {row["ApplicationTypeID"]} | {row["ApplicationStatus"]} | {row["LastStatusDate"]} | {row["PaidFees"]} | {row["CreatedByUserID"]} ");

            }

        }

        static void CancelApplications(int ID)
        {

            clsApplication application = clsApplication.GetApplicationInfoByID(ID);

            if (clsApplication.IsApplicationExist(ID))
            {

                if (application.Cancel())
                {
                    Console.WriteLine("application with id = [ " + application.ApplicationID + " ] has been Canceled Successfully\n");
                }


            }
            else
            {
                Console.WriteLine("Faild To Cancel application with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void SetApplicationsCompleted(int ID)
        {

            clsApplication application = clsApplication.GetApplicationInfoByID(ID);


            if (clsApplication.IsApplicationExist(ID))
            {


                if (application.SetCompleted())
                {
                    Console.WriteLine("application with id = [ " + application.ApplicationID + " ] Completed Successfully\n");
                }


            }
            else
            {
                Console.WriteLine("Faild To set application Completed  with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static bool DoesUserHaveAnActiveApplication(int PersontId, byte ApplicationTypeID)
        {
            int x = -1;
            if (clsApplication.DoesPersonAlreadyHaveALocalApplicationActiveOrCompleted(PersontId, ApplicationTypeID, ref x))
            {
                Console.WriteLine("User aleardy has an active Application So You Can't Add a new One \n");
                return true;
            }
            else
            {
                Console.WriteLine("User Doesn't have an active Application with this type \n");
                return false;
            }
        }

        static void IsApplicationExist(int ID)
        {

            if (clsApplication.IsApplicationExist(ID))
                Console.WriteLine("Exist :-)");
            else
                Console.WriteLine("Not Exist :-)");


        }


        //// Driver Methods --------------------------------------------------------------

        static void AddNewDriver()
        {
            clsDriver driver = new clsDriver();
            driver.PersonID = 2066;
            driver.CreatedByUserID = 1;
            driver.CreatedDate = DateTime.Now;


            if (!clsDriver.IsDriverExistWithPersonID(driver.PersonID))
            {
                if (driver.Save())
                    Console.WriteLine("Driver was Added Successfully with id = [ " + driver.ID + " ] \n");
                else
                    Console.WriteLine("Faild To add this Driver \n");

            }
            else
            {
                Console.WriteLine("This Driver is Exist Before \n");

            }

        }

        static void UpdateDriver(int ID)
        {

            clsDriver driver = clsDriver.GetDriverWithID(ID);





            if (driver != null)
            {
                int OriginalPersonID = driver.PersonID;


                driver.PersonID = 2063;
                driver.CreatedByUserID = 1;
                driver.CreatedDate = DateTime.Now;




                ////  Make sure that Person ID Is Not Uesd Before 
                if (driver.PersonID != OriginalPersonID && clsDriver.IsDriverExistWithPersonID(driver.PersonID))
                {
                    Console.Write("This Person is already a driver in system so you can't add him again ");
                    return;
                }



                if (driver.Save())
                    Console.WriteLine("driver Updated with id = [ " + driver.ID + " ] Successfully\n");
            }
            else
                Console.WriteLine("Faild To update driver with id = [ " + ID + " ] because it's not exist \n");

        }

        static void DeleteDriver(int ID)
        {
            if (clsDriver.IsDriverExist(ID))
            {
                if (clsDriver.DeleteADriver(ID))
                    Console.WriteLine("Driver with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete Driver with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("Driver with id [ " + ID + " ] is not exist :-( \n");

        }

        static void DriversList()
        {
            DataTable dataTable = clsDriver.GetAllDrivers();

            Console.WriteLine("Drivers Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["DriverID"]} | {row["PersonID"]} | {row["NationalNo"]} | {row["Full Name"]} | {row["CreatedDate"]}");

            }

        }

        static void IsDriverExist(int ID)
        {
            if (clsDriver.IsDriverExist(ID))
                Console.WriteLine("Driver with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("Driver with id [ " + ID + " ] is not exist :-( ");
        }

        static void IsDriverExistWithPersonID(int ID)
        {
            if (clsDriver.IsDriverExistWithPersonID(ID))
                Console.WriteLine("Driver with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("Driver with id [ " + ID + " ] is not exist :-( ");
        }

        static void GetDriverWithNationalNo(string NationalNo)

        {
            clsDriver User1 = clsDriver.GetDriverWithNationalNo(NationalNo);

            if (User1 != null)
            {

                Console.WriteLine(User1.ID);
                Console.WriteLine(User1.PersonID);
                Console.WriteLine(User1.CreatedDate);
                Console.WriteLine(User1.CreatedByUserID);
            }
            else
            {
                Console.WriteLine("User with National Number [ " + NationalNo + " ] Not found!");
            }
        }



        //// Licenses class Methods --------------------------------------------------------------

        static void AddNewLicenseClass()
        {
            clsLicenseClass LicenseClass = new clsLicenseClass();
            LicenseClass.ClassName = "Test";
            LicenseClass.ClassDescription = "Test des";
            LicenseClass.MinimumAllowedAge = 18;
            LicenseClass.DefaultValidityLength = 5;
            LicenseClass.ClassFees = 20.4f;



            //// check if Class Name is unique or not
            if (clsLicenseClass.IsLicenseClassExistWithName(LicenseClass.ClassName))
            {
                Console.WriteLine("The LicenseClass is Already Exist before try another one ");
                return;
            }


            if (LicenseClass.Save())
                Console.WriteLine("LicenseClass Added Successfully with id = [ " + LicenseClass.LicenseClassID + " ] \n");
            else
                Console.WriteLine("Faild To add this ApplicationType \n");
        }

        static void GetALicenseClassWithID(int ID)

        {
            clsLicenseClass LicenseClass = clsLicenseClass.GetALicenseClassByID(ID);

            if (LicenseClass != null)
            {

                Console.WriteLine(LicenseClass.ClassName);
                Console.WriteLine(LicenseClass.ClassDescription);
                Console.WriteLine(LicenseClass.MinimumAllowedAge);
                Console.WriteLine(LicenseClass.DefaultValidityLength);
                Console.WriteLine(LicenseClass.ClassFees);
            }
            else
            {
                Console.WriteLine("LicenseClass [" + ID + "] Not found!");
            }
        }

        static void GetALicenseClassWithName(string TargetApplicationName)

        {
            clsLicenseClass LicenseClass = clsLicenseClass.GetALicenseClassByClassName(TargetApplicationName);

            if (LicenseClass != null)
            {
                Console.WriteLine(LicenseClass.ClassName);
                Console.WriteLine(LicenseClass.ClassDescription);
                Console.WriteLine(LicenseClass.MinimumAllowedAge);
                Console.WriteLine(LicenseClass.DefaultValidityLength);
                Console.WriteLine(LicenseClass.ClassFees);
            }
            else
            {
                Console.WriteLine("LicenseClass [" + TargetApplicationName + "] Not found!");
            }
        }

        static void UpdateLicenseClass(int ID)
        {

            clsLicenseClass LicenseClass = clsLicenseClass.GetALicenseClassByID(ID);



            if (clsLicenseClass.IsLicenseClassExistWithID(ID))
            {
                string OriginalClassName = LicenseClass.ClassName;


                LicenseClass.ClassName = "Test";
                LicenseClass.ClassDescription = "Test dessssssssss";
                LicenseClass.MinimumAllowedAge = 18;
                LicenseClass.DefaultValidityLength = 5;
                LicenseClass.ClassFees = 20.4f;


                ////check if LicenseClass Name is Not exist before
                if (OriginalClassName != LicenseClass.ClassName && clsLicenseClass.IsLicenseClassExistWithName(LicenseClass.ClassName))
                {
                    Console.Write("You Can't use this ClassName Because it's Belong to another LicenseClass in system.\n");
                    return;
                }


                if (LicenseClass.Save())
                {
                    Console.WriteLine("LicenseClass Updated with id = [ " + LicenseClass.LicenseClassID + " ] Successfully\n");
                }
            }
            else
            {
                Console.WriteLine("Faild To update ApplicationType with id = [ " + ID + " ] because it's not exist \n");
            }

        }

        static void IsLicenseClassExistWithName(string LicenseClassName)
        {
            if (clsLicenseClass.IsLicenseClassExistWithName(LicenseClassName))
                Console.WriteLine("LicenseClass with LicenseClassName [ " + LicenseClassName + " ] is exist :-) ");

            else
                Console.WriteLine("LicenseClass with LicenseClassName [ " + LicenseClassName + " ] is not exist :-( ");
        }

        static void DeleteLicenseClass(int ID)
        {
            if (clsLicenseClass.IsLicenseClassExistWithID(ID))
            {
                if (clsLicenseClass.DeleteLicenseClass(ID))
                    Console.WriteLine("LicenseClass with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete LicenseClass with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("LicenseClass with id [ " + ID + " ] is not exist :-( ");

        }

        static void LicenseClassesList()
        {
            DataTable dataTable = clsLicenseClass.GetAllLicenseClasses();

            Console.WriteLine("License Classes Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["LicenseClassID"]} | {row["ClassName"]} | {row["ClassDescription"]} | {row["MinimumAllowedAge"]} | {row["DefaultValidityLength"]} | {row["ClassFees"]}");

            }

        }

        static void FilterdLicenseClassesList(string AttributeName, string AttributeValue)
        {
            DataTable dataTable = clsLicenseClass.GetFilterdLicenseClassesBy(AttributeName, AttributeValue);

            Console.WriteLine("License Classes Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["LicenseClassID"]} | {row["ClassName"]} | {row["ClassDescription"]} | {row["MinimumAllowedAge"]} | {row["DefaultValidityLength"]} | {row["ClassFees"]}");

            }

        }

        static void GetMinimumAllowedAge(int ID)
        {
            if (clsLicenseClass.IsLicenseClassExistWithID(ID))
            {
                Console.WriteLine("LicenseClass Minimum Allowed Age : " + clsLicenseClass.GetMinimumAllowedAge(ID));
            }

            else
                Console.WriteLine("LicenseClass with id [ " + ID + " ] is not exist :-( ");
        }


        //// License Methods --------------------------------------------------------------

        static bool ValidateDriverIDLicenseClassAndApplication(int DriverID, int LicenseClass, clsLicense license)
        {

            //// If DriverID is updated make sure that DriverID IS Exist in the sysytem
            if (!clsDriver.IsDriverExist(DriverID))
            {
                Console.Write("This Driver is not exist in system so you can't use this driverID \n");
                return true;
            }

            ////  Make sure that Driver ID dosen't have this License with this class already Before 
            if (clsLicense.IsLicenseExistWithDriverIDAndLicenseClassID(DriverID, LicenseClass))
            {
                Console.Write("This Driver already has this license in system so you use this driverID \n");
                return true;
            }

            ///// make sure that application is vaild for this License
            if (!clsApplication.IsApplicationExistWithSpaceficAttributes(license.ApplicationID, (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService,
                                                                         (int)clsApplication.enApplicationStatus.Completed))
            {
                Console.WriteLine("Inalid Application ID please add a vaild one\n");
                return true;
            }

            ///// Make sure that ApplicationID isn't used Before with any License
            if (clsLicense.IsThereLicenseHasThisApplicationID(license.ApplicationID))
            {
                Console.WriteLine("This Application ID is already used for another liceense you cant't use it again \n");
                return true;
            }




            return false;

        }

        static void AddNewLicense()
        {
            clsLicense license = new clsLicense();
            license.DriverID = 1012;
            license.ApplicationID = 139;
            license.LicenseClass = 1;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now;
            license.Notes = "";
            license.PaidFees = 15.00f;
            license.IsActive = true;
            license.IssueReason = clsLicense.enIssueReason.FirstTime;
            license.CreatedByUserID = 1;


            ///// make sure that DriverID & LicenseClass & Application are Valid
            if (ValidateDriverIDLicenseClassAndApplication(license.DriverID, license.LicenseClass, license)) return;

            if (license.Save())
                Console.WriteLine("License was Added Successfully with id = [ " + license.ID + " ] \n");
            else
                Console.WriteLine("Faild To add this License \n");


        }

        static void UpdateLicense(int ID)
        {

            clsLicense license = clsLicense.GetLicenseWithID(ID);

            if (license != null)
            {

                int OriginalDriverID = license.DriverID;
                int OriginalApplicationID = license.ApplicationID;


                license.DriverID = 1012;
                license.ApplicationID = 139;
                license.LicenseClass = 2;
                license.IssueDate = DateTime.Now;
                license.ExpirationDate = DateTime.Now;
                license.Notes = "FFFFFFFFFFFFFFFF";
                license.PaidFees = 25.00f;
                license.IsActive = true;
                license.IssueReason = clsLicense.enIssueReason.FirstTime;
                license.CreatedByUserID = 1;




                ///// in case we changed the driverID & LicenseClass
                if (license.DriverID != OriginalDriverID)
                {
                    //// If DriverID is updated make sure that DriverID IS Exist in the sysytem
                    if (!clsDriver.IsDriverExist(license.DriverID))
                    {
                        Console.Write("This Driver is not exist in system so you can't use this driverID \n");
                        return;
                    }

                }

                ///// in case we changed the ApplicationID 

                if (OriginalApplicationID != license.ApplicationID)
                {
                    ///// make sure that application is vaild for this License
                    if (!clsApplication.IsApplicationExistWithSpaceficAttributes(license.ApplicationID, (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService,
                                                                                 (int)clsApplication.enApplicationStatus.New))
                    {
                        Console.WriteLine("Inalid Application ID please add a vaild one\n");
                        return;
                    }

                    ///// Make sure that ApplicationID isn't used Before with any License
                    if (clsLicense.IsThereLicenseHasThisApplicationID(license.ApplicationID))
                    {
                        Console.WriteLine("This Application ID is already used for another liceense you cant't use it again \n");
                        return;
                    }

                }

                ////  Make sure that Driver ID dosen't have this License with this class already Before 
                if (clsLicense.IsLicenseExistWithDriverIDAndLicenseClassID(license.DriverID, license.LicenseClass))
                {
                    Console.Write("This Driver already has this license in system so you use this driverID \n");
                    return;
                }


                if (license.Save())
                    Console.WriteLine("license Updated with id = [ " + license.ID + " ] Successfully\n");
            }
            else
                Console.WriteLine("Faild To update license with id = [ " + ID + " ] because it's not exist \n");

        }

        static void DeleteLicense(int ID)
        {
            if (clsLicense.IsLicenseExist(ID))
            {
                if (clsLicense.DeleteALicense(ID))
                    Console.WriteLine("License with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete License with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("License with id [ " + ID + " ] is not exist :-( \n");

        }

        static void LicensesList()
        {
            DataTable dataTable = clsLicense.GetAllLicenses();

            Console.WriteLine("Licenses Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["LicenseID"]} | {row["ApplicationID"]} | {row["DriverID"]} | {row["LicenseClass"]} | {row["IssueDate"]}  " +
                    $"| {row["ExpirationDate"]} | {row["Notes"]} | {row["PaidFees"]} | {row["IsActive"]} | {row["IssueReason"]} | {row["CreatedByUserID"]} ");

            }

        }

        static void IsLicenseExist(int ID)
        {
            if (clsLicense.IsLicenseExist(ID))
                Console.WriteLine("License with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("License with id [ " + ID + " ] is not exist :-( ");
        }

        static void GetLicenseWithDriverIDAndLicenseClassName(int DriverID, string LicenseClassName)
        {
            clsLicense License = clsLicense.GetLicenseWithDriverIDAndLicenseClass(DriverID, LicenseClassName);


            if (License != null)
            {
                Console.WriteLine(License.ID);
                Console.WriteLine(License.DriverID);
                Console.WriteLine(License.CreatedByUserID);
                Console.WriteLine(License.ExpirationDate);
                Console.WriteLine(License.IssueDate);

            }

            else
                Console.WriteLine("License with Driver  ID [ " + DriverID + " ] is not exist :-( ");
        }

        static void IsLicenseExistWithDriverIDAndLicenseClassID(int ID, int LicenseClassID)
        {
            if (clsLicense.IsLicenseExistWithDriverIDAndLicenseClassID(ID, LicenseClassID))
                Console.WriteLine("License with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("License with id [ " + ID + " ] is not exist :-( ");
        }

        static void GetAllDriverLocalLicensesListForDriver(int DriverID)
        {
            DataTable dataTable = clsLicense.GetAllLocalLicensesForDriver(DriverID);

            Console.WriteLine("Drivers Local Licenses' Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["LicenseID"]} | {row["ApplicationID"]} | {row["ClassName"]} | {row["IssueDate"]} | {row["ExpirationDate"]}");

            }

        }

        static void DoesDriverHaveAValidLicense(int DriverID, int LicenseClassID)
        {
            int ValidLicenseID = -1;
            bool isActive = false;

            if (clsLicense.DoesDriverHasAValidLicenseWithThisLicenseClass(DriverID, LicenseClassID, ref ValidLicenseID, ref isActive))
                Console.WriteLine("There is An Active License with id [ " + ValidLicenseID + " ] is Valid and it's Active Status is : " + isActive);

            else
                Console.WriteLine("There Is No Active License For This Driver With This Class");

        }


        //// International Licenses class Methods --------------------------------------------------------------

        static void AddNewInternationalLicense()
        {
            clsInternationalLicense Internationallicense = new clsInternationalLicense();

            Internationallicense.DriverID = 10;
            Internationallicense.ApplicationID = 114;
            Internationallicense.IssuedUsingLocalLicenseID = 24;
            Internationallicense.IssueDate = DateTime.Now;
            Internationallicense.ExpirationDate = DateTime.Now;
            Internationallicense.IsActive = true;
            Internationallicense.CreatedByUserID = 1;


            if (!clsInternationalLicense.IsInternationalLicenseExistWithDriverID(Internationallicense.DriverID))
            {
                if (Internationallicense.Save())
                    Console.WriteLine("International License was Added Successfully with id = [ " + Internationallicense.ID + " ] \n");
                else
                    Console.WriteLine("Faild To add this International License \n");

            }
            else
            {
                Console.WriteLine("This International License is Exist Before \n");

            }

        }

        static void UpdateInternationalLicense(int ID)
        {

            clsInternationalLicense Internationallicense = clsInternationalLicense.GetInternationalLicenseWithID(ID);

            if (Internationallicense != null)
            {

                int OriginalDriverID = Internationallicense.DriverID;

                Internationallicense.DriverID = 10;
                Internationallicense.ApplicationID = 114;
                Internationallicense.IssuedUsingLocalLicenseID = 24;
                Internationallicense.IssueDate = DateTime.Now;
                Internationallicense.ExpirationDate = DateTime.Now;
                Internationallicense.IsActive = false;
                Internationallicense.CreatedByUserID = 1;



                ////  Make sure that Driver ID Is Not Uesd Before 
                if (Internationallicense.DriverID != OriginalDriverID && clsInternationalLicense.IsInternationalLicenseExistWithDriverID(Internationallicense.DriverID))
                {
                    Console.Write("This Driver already has this International license in system so you use this driverID ");
                    return;
                }



                if (Internationallicense.Save())
                    Console.WriteLine("International license Updated with id = [ " + Internationallicense.ID + " ] Successfully\n");
            }
            else
                Console.WriteLine("Faild To update International license with id = [ " + ID + " ] because it's not exist \n");

        }

        static void DeleteInternationalLicense(int ID)
        {
            if (clsInternationalLicense.IsInternationalLicenseExist(ID))
            {
                if (clsInternationalLicense.DeleteAnInternationalLicense(ID))
                    Console.WriteLine("International License with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete International License with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("International License with id [ " + ID + " ] is not exist :-( \n");

        }

        static void InternationalLicensesList()
        {
            DataTable dataTable = clsInternationalLicense.GetAllInternationalLicenses();

            Console.WriteLine("International Licenses Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["InternationalLicenseID"]} | {row["ApplicationID"]} | {row["DriverID"]} | {row["IssuedUsingLocalLicenseID"]} | {row["IssueDate"]}  | {row["ExpirationDate"]} | {row["IsActive"]} | {row["CreatedByUserID"]}");

            }

        }

        static void IsInternationalLicenseExist(int ID)
        {
            if (clsInternationalLicense.IsInternationalLicenseExist(ID))
                Console.WriteLine("International License with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("International License with id [ " + ID + " ] is not exist :-( ");
        }

        static void IsInternationalLicenseExistWithDriverID(int ID)
        {
            if (clsInternationalLicense.IsInternationalLicenseExistWithDriverID(ID))
                Console.WriteLine("International License with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("International License with id [ " + ID + " ] is not exist :-( ");
        }

        static void IsInternationalLicenseExistWithLocalLicenseID(int ID)
        {
            int InternationalLicenseID = -1;

            if (clsInternationalLicense.IsInternationalLicenseExistWithLocalLicenseIDActive(ID, ref InternationalLicenseID))
                Console.WriteLine("International License with id [ " + ID + " ] is exist :-) ");

            else
                Console.WriteLine("International License with id [ " + ID + " ] is not exist :-( ");
        }

        static void GetAllInternationalLicensesListForDriver(int DriverID)
        {
            DataTable dataTable = clsInternationalLicense.GetAllInternationalLicensesForDriver(DriverID);

            Console.WriteLine("Drivers International Licenses' Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["InternationalLicenseID"]} | {row["ApplicationID"]} | {row["IssueDate"]} | {row["ExpirationDate"]}");

            }

        }


        //// Local Driving License Application Methods --------------------------------------------------------------

        static void GetLocalDrivingLicenseApplicationInfoByID(int ID)

        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(ID);
            if (LocalDrivingLicenseApplication != null)
            {
                Console.WriteLine(LocalDrivingLicenseApplication.ApplicationID);
                Console.WriteLine(LocalDrivingLicenseApplication.ApplicantPersonID);
                Console.WriteLine(LocalDrivingLicenseApplication.FullName);
                Console.WriteLine(LocalDrivingLicenseApplication.ApplicationDate);
                Console.WriteLine(LocalDrivingLicenseApplication.ApplicationStatus);

            }
            else
            {
                Console.WriteLine("LicenseClass [" + ID + "] Not found!");
            }
        }

        static void IsLocalDrivingLicenseApplicationExist(int ID)
        {

            if (clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(ID))
                Console.WriteLine("Exist :-)");
            else
                Console.WriteLine("Not Exist :-)");


        }

        static void AddNewLocalDrivingLicenseApplication()
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            LocalDrivingLicenseApplication.ApplicationID = 113;
            LocalDrivingLicenseApplication.LicenseClassID = 1;

            ///// Check if there is an Application for the same person and it is in progress or not completed
            if (clsApplication.IsApplicationExistWithSpaceficAttributes(LocalDrivingLicenseApplication.ApplicationID, (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService,
                                                                       (int)clsApplication.enApplicationStatus.New))
            {
                if (LocalDrivingLicenseApplication.Save())
                {
                    Console.WriteLine("Local Driving License Application Added Successfully with id = [ " + LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID + " ] \n");
                    LocalDrivingLicenseApplication.SetCompleted();
                }
                else
                    Console.WriteLine("Faild To add this Local Driving License Application \n");
            }
        }

        static void UpdateLocalDrivingLicenseApplication(int ID)
        {

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(ID);


            if (clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(ID))
            {

                int OriginalApplicationID = LocalDrivingLicenseApplication.ApplicationID;


                Console.WriteLine(LocalDrivingLicenseApplication.ApplicationID.ToString());



                LocalDrivingLicenseApplication.ApplicationID = 172;
                LocalDrivingLicenseApplication.LicenseClassID = 1;
                LocalDrivingLicenseApplication.PaidFees = 12;


                ///// Make sure that Updated Application ID is Valid
                if (OriginalApplicationID != LocalDrivingLicenseApplication.ApplicationID)
                {
                    if (!clsApplication.IsApplicationExistWithSpaceficAttributes(LocalDrivingLicenseApplication.ApplicationID, (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService,
                                                                       (int)clsApplication.enApplicationStatus.New))
                    {
                        Console.Write("Invalid Application ID please add a valid data.\n");
                        return;
                    }
                }

                if (LocalDrivingLicenseApplication.SaveLocalDrivingLicense())
                    Console.WriteLine("LocalDrivingLicenseApplication Updated with id = [ " + LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID + " ] Successfully\n");
                else
                    Console.WriteLine("Faild To update application with id = [ " + ID + " ] \n");
            }
            else
                Console.WriteLine("Faild To update application with id = [ " + ID + " ] because it's not exist \n");


            Console.WriteLine("XXXXX:" + LocalDrivingLicenseApplication.ApplicationID.ToString());
        }

        static void DeleteLocalDrivingLicenseApplication(int ID)
        {
            if (clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(ID))
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(ID))
                    Console.WriteLine("Application with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete Application with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("Application with id [ " + ID + " ] is not exist :-( \n");

        }

        static void LocalDrivingLicenseApplicationList()
        {
            DataTable dataTable = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            Console.WriteLine("Local Driving License Application Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["LocalDrivingLicenseApplicationID"]} | {row["ClassName"]} | {row["NationalNo"]} " +
                    $"| {row["FullName"]} | {row["ApplicationDate"]} | {row["PassedTestCount"]} | {row["Status"]} ");

            }

        }

        //// Test Appointment Methods --------------------------------------------------------------

        static void AddNewTestAppointment()
        {
            clsTestAppointment testAppointment = new clsTestAppointment();
            testAppointment.TestTypeID = clsTestType.enTestType.VisionTest;
            testAppointment.LocalDrivingLicenseApplicationID = 37;
            testAppointment.AppointmentDate = DateTime.Now;
            testAppointment.PaidFees = 15.00f;
            testAppointment.CreatedByUserID = 1;
            testAppointment.IsLocked = false;
            testAppointment.RetakeTestApplicationID = -1;


            ///// make sure that LocalDrivingLicenseApplicationID is Exist
            if (!clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(testAppointment.LocalDrivingLicenseApplicationID)) return;

            if (testAppointment.Save())
                Console.WriteLine("Test Appointment was Added Successfully with id = [ " + testAppointment.ID + " ] \n");
            else
                Console.WriteLine("Faild To add this Test Appointment \n");


        }

        static void UpdateTestAppointment(int ID)
        {

            clsTestAppointment testAppointment = clsTestAppointment.GetTestAppointmentByID(ID);

            if (testAppointment != null)
            {

                int OriginalLocalDrivingLicenseApplicationID = testAppointment.LocalDrivingLicenseApplicationID;


                testAppointment.TestTypeID = clsTestType.enTestType.WrittenTest;
                testAppointment.LocalDrivingLicenseApplicationID = 37;
                testAppointment.AppointmentDate = DateTime.Now;
                testAppointment.PaidFees = 1.50f;
                testAppointment.CreatedByUserID = 1;
                testAppointment.IsLocked = false;
                testAppointment.RetakeTestApplicationID = 125;


                //// Make sure that LocalDrivingLicenseApplicationID is exist and used in active test Appointment   

                //if (OriginalLocalDrivingLicenseApplicationID != testAppointment.LocalDrivingLicenseApplicationID)
                //{
                //    if (clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(testAppointment.LocalDrivingLicenseApplicationID))
                //        Console.WriteLine("This Local Driving License Application ID is used before \n");

                //}

                if (testAppointment.Save())
                    Console.WriteLine("Test Appointment Updated with id = [ " + testAppointment.ID + " ] Successfully\n");
            }
            else
                Console.WriteLine("Faild To update Test Appointment with id = [ " + ID + " ] because it's not exist \n");

        }

        static void GetTestAppointmentInfoByID(int ID)

        {
            clsTestAppointment testAppointment = clsTestAppointment.GetTestAppointmentByID(ID);
            if (testAppointment != null)
            {

                Console.WriteLine(testAppointment.ID);
                Console.WriteLine(testAppointment.LocalDrivingLicenseApplicationID);
                Console.WriteLine(testAppointment.RetakeTestApplicationID);
                Console.WriteLine(testAppointment.AppointmentDate);
                Console.WriteLine(testAppointment.PaidFees);
                Console.WriteLine(testAppointment.CreatedByUserID);
                Console.WriteLine(testAppointment.IsLocked);

            }

            else
            {
                Console.WriteLine("test Appointment [" + ID + "] Not found!");
            }
        }

        static void DeleteTestAppointment(int ID)
        {


            if (clsTestAppointment.IsTestAppointmentExist(ID))
            {
                if (clsTestAppointment.DeleteTestAppointment(ID))
                    Console.WriteLine("Test Appointment with id = " + ID + " Deleted Successfully \n");

                else
                {
                    Console.WriteLine("Faild To Delete Test Appointment with id = [ " + ID + " ] because it's not exist \n");
                    return;
                }

            }
            else
                Console.WriteLine("Test Appointment with id [ " + ID + " ] is not exist :-( \n");

        }

        static void TestAppointmentsList()
        {
            DataTable dataTable = clsTestAppointment.GetAllTestAppointments();

            Console.WriteLine("TestAppointments Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["TestAppointmentID"]} | {row["TestTypeID"]} | {row["LocalDrivingLicenseApplicationID"]} | {row["AppointmentDate"]}" +
                    $" | {row["PaidFees"]} | {row["CreatedByUserID"]} | {row["IsLocked"]} | {row["RetakeTestApplicationID"]}");
            }

        }

        static void GetAllTestAppointmentsForLocalAppAndTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            DataTable dataTable = clsTestAppointment.GetAllTestAppointmentsForLocalAppAndTestType(LocalDrivingLicenseApplicationID, TestType);

            Console.WriteLine("TestAppointments Data for LocalDrivingLicenseApplicationID " + LocalDrivingLicenseApplicationID + " : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["TestAppointmentID"]} | {row["TestTypeID"]} | {row["LocalDrivingLicenseApplicationID"]} | {row["AppointmentDate"]}" +
                    $" | {row["PaidFees"]} | {row["CreatedByUserID"]} | {row["IsLocked"]} | {row["RetakeTestApplicationID"]}");
            }
        }

        static void IsTestAppointmentsExistWithID(int ID)
        {

            if (clsTestAppointment.IsTestAppointmentExist(ID))
                Console.WriteLine("Exist :-)");
            else
                Console.WriteLine("Not Exist :-(");
        }

        static void IsTestAppointmentsExistWithAppIDAndType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {

            if (clsTestAppointment.IsTestAppointmentExist(LocalDrivingLicenseApplicationID, TestType))
                Console.WriteLine("Exist :-)");
            else
                Console.WriteLine("Not Exist :-(");
        }

        static void DoesPersonHaveAnActiveAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            if (clsTestAppointment.DoesPersonHaveAnActiveAppointment(LocalDrivingLicenseApplicationID, TestType))
                Console.WriteLine("Yes, They Have An Active Appointment :-)");
            else
                Console.WriteLine("Not They Haven't Active Appointment :-(");
        }

        static void GetTriesNumForTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            int Tries = 0;

            Tries = clsTestAppointment.GetTriesNumForTestAppointment(LocalDrivingLicenseApplicationID, TestType);

            Console.Write("Number of Tries for this Test Appointment is : " + Tries + " \n");


        }



        //// Test Methods --------------------------------------------------------------
        static void AddNewTest()
        {

            clsTest test = new clsTest();
            test.TestAppointmentID = 126;
            test.TestResult = false;
            test.Notes = "Good";
            test.CreatedByUserID = 1;


            ///// make sure that TestAppointmentID is Exist
            if (!clsTestAppointment.IsTestAppointmentExist(test.TestAppointmentID)) return;

            if (test.Save())
                Console.WriteLine("Test was Added Successfully with id = [ " + test.ID + " ] \n");
            else
                Console.WriteLine("Faild To add this Test \n");


        }
        static void UpdateTest(int ID)
        {

            clsTest test = clsTest.GetTestByID(ID);

            if (test != null)
            {

                int OriginalTestAppointmentID = test.TestAppointmentID;


                test.TestAppointmentID = 126;
                test.TestResult = false;
                test.Notes = "Good Edited";
                test.CreatedByUserID = 1;


                //// Make sure that TestAppointmentID is exist and active 

                //if (OriginalTestAppointmentID != test.TestAppointmentID)
                //{
                //}

                if (test.Save())
                    Console.WriteLine("Test Updated with id = [ " + test.ID + " ] Successfully\n");
            }
            else
                Console.WriteLine("Faild To update Test with id = [ " + ID + " ] because it's not exist \n");

        }
        static void GetTestInfoByID(int ID)

        {
            clsTest testAppointment = clsTest.GetTestByID(ID);
            if (testAppointment != null)
            {
                Console.WriteLine(testAppointment.TestAppointmentID);
                Console.WriteLine(testAppointment.TestResult);
                Console.WriteLine(testAppointment.Notes);
                Console.WriteLine(testAppointment.CreatedByUserID);

            }

            else
            {
                Console.WriteLine("test [" + ID + "] Not found!");
            }
        }
        static void TestList()
        {
            DataTable dataTable = clsTest.GetAllTests();

            Console.WriteLine("Test Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["TestID"]} | {row["TestAppointmentID"]} | {row["TestResult"]} | {row["Notes"]} | {row["CreatedByUserID"]}");
            }

        }
        static void DoesPersonPassedThisTestBefore(int TestAppointmentID, clsTestType.enTestType TestType)
        {
            if (clsTest.DoesPersonPassedThisTestBefore(TestAppointmentID, TestType))
            {
                Console.WriteLine("Yes, They Passed");
            }
            else
                Console.WriteLine("No, They Didn't Passed");
        }

        //// Detained License Methods --------------------------------------------------------------

        static void AddNewDetainedLicense()
        {

            clsDetainedLicense DetainedLicense = new clsDetainedLicense();
            DetainedLicense.LicenseID = 25;
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = 140;
            DetainedLicense.CreatedByUserID = 1;


            /// make sure that TestAppointmentID is Exist
            if (!clsDetainedLicense.IsLicenseDetained(DetainedLicense.LicenseID)) return;

            if (DetainedLicense.Save())
                Console.WriteLine("Test was Added Successfully with id = [ " + DetainedLicense.DetainID + " ] \n");
            else
                Console.WriteLine("Faild To add this Test \n");


        }

        static void UpdateDetainedLicense(int DetainID)
        {

            clsDetainedLicense DetainedLicense = clsDetainedLicense.GetDetainedLicenseByID(DetainID);

            if (DetainedLicense != null)
            {
                int OriginalLicenseID = DetainedLicense.LicenseID;

                DetainedLicense.LicenseID = 25;
                DetainedLicense.DetainDate = DateTime.Now;
                DetainedLicense.FineFees = 170;
                DetainedLicense.CreatedByUserID = 1;

                if (OriginalLicenseID != DetainedLicense.LicenseID)
                {
                    ///// make sure the license is not detained before and it's exist in system
                    if (!clsLicense.IsLicenseExist(DetainedLicense.LicenseID))
                    {
                        Console.WriteLine("Invaild License ID \n");
                        return;
                    }

                    if (clsDetainedLicense.IsLicenseDetained(DetainedLicense.LicenseID))
                    {
                        Console.WriteLine("This License is Already Detained \n");
                        return;
                    }

                }

                if (DetainedLicense.Save())
                    Console.WriteLine("DetainedLicense Updated with id = [ " + DetainID + " ] Successfully\n");
                else
                    Console.WriteLine("Faild To update Detained License with id = [ " + DetainID + " ] \n");

            }
            else
                Console.WriteLine("Faild To update Detained License with id = [ " + DetainID + " ] because it's not exist \n");

        }

        static void TestDetainedLicense()
        {
            DataTable dataTable = clsDetainedLicense.GetAllDetainedLicenses();

            Console.WriteLine("Detained License Data : ");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["DetainID"]} | {row["LicenseID"]} | {row["IsReleased"]} | {row["FineFees"]} | {row["ReleaseDate"]} | " +
                    $" {row["FullName"]} | {row["NationalNo"]} | {row["ReleaseApplicationID"]} ");
            }

        }

        static void IsLicenseDetained(int LicenseID)
        {
            if (clsDetainedLicense.IsLicenseDetained(LicenseID))
                Console.WriteLine("Yes, License Is Detained");
            else
                Console.WriteLine("No, License Is Not Detained");
        }

        static void DetainLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            clsDetainedLicense DetainedLicense = clsDetainedLicense.GetDetainedLicenseByID(DetainID);

            if (DetainedLicense != null)
            {

                if (!clsDetainedLicense.IsLicenseDetained(DetainedLicense.LicenseID))
                {
                    Console.WriteLine("License is not Detained To release");
                    return;
                }

                if (DetainedLicense.ReleaseDetainedLicense(ReleasedByUserID, ReleaseApplicationID))
                    Console.WriteLine("License Is Released Successfully");
                else
                    Console.WriteLine("License Is Not Released Successfully");
            }
            else Console.WriteLine("Faild To Get Detained License with id = [ " + DetainID + " ] because it's not exist \n");



        }

        ///-----------------------------///-----------------------------///-----------------------------///-----------------------------

        //static void GetPersonFullNameFromApplication()
        //{
        //    Console.WriteLine(clsApplication.GetApplicationInfoByID(250).ApplicantFullName);
        //}


        static void Main(string[] args)
        {


            //AddNewDetainedLicense();
            //UpdateDetainedLicense(12);
            //TestDetainedLicense();
            //IsLicenseDetained(25);
            //DetainLicense(17, 1, 131);

            ///-----------------------------

            //AddNewTest();
            //GetTestInfoByID(85);
            //UpdateTest(85);
            //TestList();
            //DoesPersonPassedThisTestBefore(158, clsTestType.enTestType.VisionTest);

            ///-----------------------------

            //GetTestAppointmentInfoByID(143);
            //AddNewTestAppointment();
            //UpdateTestAppointment(143);
            //TestAppointmentsList();
            //DeleteTestAppointment(128);
            //IsTestAppointmentsExistWithID(109);
            //IsTestAppointmentsExistWithAppIDAndType(36, clsTestType.enTestType.VisionTest);
            //GetAllTestAppointmentsForLocalAppAndTestType(36);
            //DoesPersonHaveAnActiveAppointment(38, clsTestType.enTestType.VisionTest);
            //GetTriesNumForTestAppointment(36,clsTestType.enTestType.VisionTest);

            ///-----------------------------


            //GetLocalDrivingLicenseApplicationInfoByID(36);
            //IsLocalDrivingLicenseApplicationExist(43);
            //AddNewLocalDrivingLicenseApplication();
            //DeleteLocalDrivingLicenseApplication(56);
            //UpdateLocalDrivingLicenseApplication(63);
            //LocalDrivingLicenseApplicationList();


            ///-----------------------------

            //AddNewInternationalLicense();
            //UpdateInternationalLicense(19);
            //DeleteInternationalLicense(19);
            //InternationalLicensesList();
            //IsInternationalLicenseExist(18);
            //IsInternationalLicenseExistWithDriverID(9);
            //IsInternationalLicenseExistWithLocalLicenseID(21);

            ///-----------------------------


            //GetPersonWithID(1);
            //GetPersonWithNationalNo("N55");
            //AddNewPerson();
            //UpdatePerson(1055);
            //DeletePerson(1026);
            //IsPersonExist(11);
            //ListPersons();
            //ListPersonsFilterdPersons("CountryName", "Jordan");
            //GetPersonAge(1);

            ///-----------------------------
            //GetCountryByName("Canada");
            //GetCountryByID(4);
            //IsCountryExistByID(1);
            //IsCountryExistByName("United Kingdom");
            //AddNewCountry("Egypt", "20", "20");
            //UpdateCountryWithName("Egypto", "Egypt");
            //DeleteCountry(18);
            //ListCountries();
            //Console.WriteLine(int.Parse("1").GetType() == 1.GetType());

            ///-----------------------------

            //AddNewUser();
            //GetUserWithID(15);
            //GetUserWithUserNameAndPassword("Omar1", "12");
            //IsUserExistWithUserName("Kahled");
            //ChangePassword(1, "1234s");
            //GetUserWithPersonID(1025);
            //UpdateUser(19);
            //DeleteUser(21);
            //ListUsers();
            //IsUserExist(1);
            //ListUsersFilterdUsers("UserName", "user4");


            ///-----------------------------

            //AddNewApplicationType();
            //GetAnApplicationTypeWithID(8);
            //UpdateApplicationType(8);
            //GetAnApplicationTypeWithID(8);
            //GetAnApplicationTypeWithTitle("Renew Driving License Service");
            //IsApplicationTypeExistWithName("New Local Driving License Service");
            //DeleteApplicationType(8);
            //ApplicationTypesList();
            //FilterdApplicationTypesList("ApplicationTypeTitle", "New International License");

            ///-----------------------------


            //AddNewTestType();
            //GetAnTestTypeWithID(4);
            //UpdateTestType(4);
            //GetAnTestTypeWithID(4);
            //GetAnTestTypeWithTitle("Vision Test");
            //IsTestTypeExistWithTitle("Vision Test");
            //DeleteTestType(4);
            //TestTypesList();
            //FilterdTestTypesList("TestTypeTitle", "Vision Test");

            ///-----------------------------


            //AddNewApplication();
            //UpdateApplication(136);
            //DeleteApplication(136);
            //ApplicationsList();
            //CancelApplications(138);
            //SetApplicationsCompleted(138);
            //DoesUserHaveAnActiveApplication(2066, 1);
            //IsApplicationExist(139);


            ///-----------------------------


            //AddNewDriver();
            //UpdateDriver(1012);
            //DeleteDriver(1013);
            //DriversList();
            //IsDriverExist(1012);
            //IsDriverExistWithPersonID(2063);


            ///-----------------------------

            //AddNewLicense();
            //UpdateLicense(38);
            //DeleteLicense(30);
            //LicensesList();
            //IsLicenseExist(30);
            //IsLicenseExistWithDriverID(8);
            //GetAllDriverLocalLicensesListForDriver(11);
            //GetAllInternationalLicensesListForDriver(11);
            //GetLicenseWithApplicationID(127);
            //GetLicenseWithDriverIDAndLicenseClassName(8, "Class 1 - Small Motorcycle");
            //DoesDriverHaveAValidLicense(11, 3);


            ///-----------------------------


            //AddNewLicenseClass();
            //GetALicenseClassWithID(1);
            //GetALicenseClassWithName("Class 1 - Small Motorcycle");
            //UpdateLicenseClass(9);
            //IsLicenseClassExistWithName("Class 1 - Small Motorcycle");
            //DeleteLicenseClass(11);
            //LicenseClassesList();
            //FilterdLicenseClassesList("ClassName", "Class 1 - Small Motorcycle");
            //GetMinimumAllowedAge(1);

            ///-----------------------------


            Console.ReadKey();

        }
    }
}

