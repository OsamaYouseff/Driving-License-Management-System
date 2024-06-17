using System.Data;
using TestTypesDataAccessLayer;

namespace TestTypesBusinessLayer
{
    public class clsTestType
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };



        public int TestID { set; get; }
        public string TestName { set; get; }
        public string TestDescription { set; get; }
        public float TestFees { set; get; }



        public clsTestType()
        {
            TestID = -1;
            TestName = "";
            TestDescription = "";
            TestFees = 0;

            Mode = enMode.AddNew;
        }
        private clsTestType(int TestID, string TestName, string TestDescription, float TestFees)
        {
            this.TestID = TestID;
            this.TestName = TestName;
            this.TestDescription = TestDescription;
            this.TestFees = TestFees;

            Mode = enMode.Update;
        }



        private bool _AddNewTestType(string TargetTestName, string TestDescription, float TestFees)
        {

            this.TestID = clsTestTypeDataAccess.AddNewTestTypeWithName(TargetTestName, TestDescription, TestFees);

            return TestID != -1;
        }
        private bool _UpdateTestType()
        {
            return clsTestTypeDataAccess.UpdateTestType(this.TestID, this.TestName, this.TestDescription, this.TestFees);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType(this.TestName, this.TestDescription, this.TestFees))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();


            }

            return false;

        }
        public void CopyData(clsTestType FromApp)
        {
            this.TestID = FromApp.TestID;
            this.TestName = FromApp.TestName;
            this.TestDescription = FromApp.TestDescription;
            this.TestFees = FromApp.TestFees;
        }

        public static clsTestType GetTestType(string TargetTestName)
        {
            int TestID = -1;
            float TestFees = 0;
            string TestDescription = "";

            if (clsTestTypeDataAccess.GetTestTypeWithName(TargetTestName, ref TestID, ref TestDescription, ref TestFees))

                return new clsTestType(TestID, TargetTestName, TestDescription, TestFees);

            else
                return null;
        }
        public static clsTestType GetTestType(int TestID)
        {
            string TestName = "", TestDescription = "";
            float TestFees = 0;

            if (clsTestTypeDataAccess.GetTestTypeWithID(TestID, ref TestName, ref TestDescription, ref TestFees))

                return new clsTestType(TestID, TestName, TestDescription, TestFees);
            else
                return null;

        }
        public static bool IsTestTypeExistWithName(string TargetTestName)
        {

            return clsTestTypeDataAccess.IsTestTypeExist(TargetTestName);

        }
        public static bool IsTestTypeExistWithID(int ID)
        {

            return clsTestTypeDataAccess.IsTestTypeExist(ID);

        }
        public static bool DeleteTestType(int ID)
        {
            return clsTestTypeDataAccess.DeleteTestType(ID);
        }
        public static DataTable GetAllTestsTypes()
        {
            return clsTestTypeDataAccess.GetAllTestsTypes();

        }
        public static DataTable GetFilterdUsersBy(string AttributeName, string AttributeValue)
        {
            return clsTestTypeDataAccess.GetFilterdUsersBy(AttributeName, AttributeValue);
        }
        public static bool AreTheyDuplicated(clsTestType App1, clsTestType App2)
        {
            if (App1.TestID != App2.TestID) return false;
            else if (App1.TestName != App2.TestName) return false;
            else if (App1.TestDescription != App2.TestDescription) return false;
            else if (App1.TestFees != App2.TestFees) return false;

            return true;
        }



    }
}
