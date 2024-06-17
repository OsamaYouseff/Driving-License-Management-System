using System;
using System.Data;
using PeopleDataAccessLayer;
using System.Data.SqlClient;

namespace ApplicationDataAccessLayer
{
    public class clsApplicationTypesDataAccess
    {

        public static bool GetApplicationTypeWithName(string TargetApplicationName, ref int ApplicationID, ref float ApplicationFees)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeTitle = @TargetApplicationName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TargetApplicationName", TargetApplicationName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {


                    // The record was found
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationTypeID"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
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

        public static bool GetApplicationTypeWithID(int ApplicationID, ref string ApplicationName, ref float ApplicationFees)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {


                    // The record was found
                    isFound = true;

                    ApplicationName = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
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

        public static bool IsApplicationTypeExist(string TargetApplicationName)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM ApplicationTypes Where ApplicationTypeTitle = @ApplicationName ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("ApplicationName", TargetApplicationName);

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

        public static bool IsApplicationTypeExist(int TargetApplicationID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM ApplicationTypes Where ApplicationTypeID = @TargetApplicationID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TargetApplicationID", TargetApplicationID);

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

        public static int AddNewApplicationTypeWithName(string TargetApplicationName, float ApplicationFees)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "INSERT INTO ApplicationTypes VALUES (@TargetApplicationName , @ApplicationFees ) SELECT SCOPE_IDENTITY();";


            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetApplicationName", TargetApplicationName);
            Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);


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

        public static bool UpdateApplicationType(int TargetApplicationID, string ApplicationName, float ApplicationFees)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE ApplicationTypes " +
                "SET ApplicationTypeTitle = @ApplicationName ," +
                "ApplicationFees = @ApplicationFees " +
                "WHERE ApplicationTypeID = @TargetApplicationID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
            Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            Command.Parameters.AddWithValue("@TargetApplicationID", TargetApplicationID);


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

        public static bool DeleteApplicationType(int TargetApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = @"Delete ApplicationTypes 
                                where ApplicationTypeID = @TargetApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TargetApplicationID", TargetApplicationID);

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

        public static DataTable GetAllApplicationsTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeTitle";

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

        public static DataTable GetFilterdApplicationsBy(string AttributeName, string AttributeValue)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes where " + AttributeName + " = @AttributeValue  order by ApplicationTypeTitle";

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
