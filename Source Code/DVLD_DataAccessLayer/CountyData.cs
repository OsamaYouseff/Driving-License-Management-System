using System;
using System.Data;
using System.Data.SqlClient;

namespace PeopleDataAccessLayer
{
    public class clsCountryDataAccess
    {
        public static bool GetCountryWithName(ref string TargetCountryName, ref int CountryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", TargetCountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {


                    // The record was found
                    isFound = true;

                    CountryID = (int)reader["CountryID"];

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

        public static bool GetCountryWithID(int CountryID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    CountryName = (string)reader["CountryName"];
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

        public static bool IsCountryExist(string TargetCountryName)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Countries Where CountryName = @TargetCountryName ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TargetCountryName", TargetCountryName);

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

        public static bool IsCountryExist(int TargetID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Countries Where CountryID = @TargetID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TargetID", TargetID);

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

        public static int AddNewCountryWithName(string CountryName, string CountryCode = "", string CountryPhoneCode = "")
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "INSERT INTO Countries VALUES (@CountryName , @CountryCode ,@CountryPhoneCode) SELECT SCOPE_IDENTITY();";


            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryName);


            if (CountryCode == "")
            {
                Command.Parameters.AddWithValue("@CountryCode", DBNull.Value);

            }
            else
            {
                Command.Parameters.AddWithValue("@CountryCode", CountryCode);
            }

            if (CountryPhoneCode == "")
            {
                Command.Parameters.AddWithValue("@CountryPhoneCode", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@CountryPhoneCode", CountryPhoneCode);

            }


            try
            {
                Connection.Open();
                object scopeIdentity = Command.ExecuteScalar();

                Connection.Close();

                if (scopeIdentity != null && int.TryParse(scopeIdentity.ToString(), out int insertedID))
                {
                    //Console.WriteLine($"Country Was Added Succefully with ID {scopeIdentity}\n");
                    return insertedID;

                }
                else
                {
                    //Console.WriteLine($"Country Insertion Failed \n");
                    return -1;

                }
            }
            catch (Exception ex)
            {

                //Console.WriteLine("Error" + ex.Message);
                return -1;
            }
        }

        public static bool UpdateCountry(int TargetCountryID, string CountryName)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE Countries " +
                "SET CountryName = @CountryName " +
                "WHERE CountryID = @TargetCountryID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@CountryName", CountryName);
            Command.Parameters.AddWithValue("@TargetCountryID", TargetCountryID);
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

        public static bool DeleteCountry(int CountryID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = @"Delete Countries 
                                where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

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

        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM Countries order by CountryName";

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

    }
}
