using System;
using System.Data;
using PeopleDataAccessLayer;
using System.Data.SqlClient;

namespace TestTypesDataAccessLayer
{
    public class clsTestTypeDataAccess
    {
        public static bool GetTestTypeWithName(string TargetTestName, ref int TestID, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeTitle = @TargetTestName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TargetTestName", TargetTestName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {


                    // The record was found
                    isFound = true;

                    TestID = (int)reader["TestTypeID"];
                    TestDescription = (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToSingle(reader["TestTypeFees"]);
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool GetTestTypeWithID(int TestID, ref string TestName, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {


                    // The record was found
                    isFound = true;

                    TestName = (string)reader["TestTypeTitle"];
                    TestDescription = (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToSingle(reader["TestTypeFees"]);
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsTestTypeExist(string TargetTestName)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM TestTypes Where TestTypeTitle = @TestName ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TestName", TargetTestName);

            bool isExist = false;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.HasRows)
                    isExist = true;

                else
                    isExist = false;

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error" + ex.Message);

                isExist = false;
            }
            finally
            {
                Connection.Close();
            }

            return isExist;
        }

        public static bool IsTestTypeExist(int TargetTestID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM TestTypes Where TestTypeID = @TargetTestID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TargetTestID", TargetTestID);

            bool isExist = false;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.HasRows)
                    isExist = true;

                else
                    isExist = false;
                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error" + ex.Message);

                isExist = false;
            }
            finally
            {
                Connection.Close();
            }

            return isExist;
        }

        public static int AddNewTestTypeWithName(string TargetTestName, string TestDescription, float TestFees)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "INSERT INTO TestTypes VALUES (@TargetTestName , @TestDescription , @TestFees ) SELECT SCOPE_IDENTITY();";


            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetTestName", TargetTestName);
            Command.Parameters.AddWithValue("@TestDescription", TestDescription);
            Command.Parameters.AddWithValue("@TestFees", TestFees);


            try
            {
                Connection.Open();
                object scopeIdentity = Command.ExecuteScalar();

                Connection.Close();

                if (scopeIdentity != null && int.TryParse(scopeIdentity.ToString(), out int insertedID))
                {
                    return insertedID;

                }
                else
                {
                    return -1;

                }
            }
            catch (Exception ex)
            {

                //Console.WriteLine("Error" + ex.Message);
                return -1;
            }
        }

        public static bool UpdateTestType(int TargetTestID, string TestName, string TestDescription, float TestFees)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE TestTypes " +
                "SET TestTypeTitle = @TestName ," +
                "TestTypeFees = @TestFees ," +
                "TestTypeDescription = @TestDescription " +
                "WHERE TestTypeID = @TargetTestID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@TestName", TestName);
            Command.Parameters.AddWithValue("@TestDescription", TestDescription);
            Command.Parameters.AddWithValue("@TestFees", TestFees);
            Command.Parameters.AddWithValue("@TargetTestID", TargetTestID);


            try
            {
                Connection.Open();
                AffectedRows = Command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error" + ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return AffectedRows > 0;

        }

        public static bool DeleteTestType(int TargetTestID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = @"Delete TestTypes 
                                where TestTypeID = @TargetTestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TargetTestID", TargetTestID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static DataTable GetAllTestsTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM TestTypes order by TestTypeTitle";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static DataTable GetFilterdUsersBy(string AttributeName, string AttributeValue)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM TestTypes where " + AttributeName + " = @AttributeValue  order by TestTypeTitle";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@AttributeValue", AttributeValue);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

    }
}
