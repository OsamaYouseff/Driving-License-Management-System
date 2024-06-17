using System;
using System.Data;
using PeopleDataAccessLayer;
using System.Data.SqlClient;

namespace InternationalLicensesDataAccessLayer
{
    public class clsInternationalLicensesDataAccess
    {

        public static bool GetInternationalLicenseInfoByID(int ID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
            ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * from InternationalLicenses Where InternationalLicenseID = @InternationalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;


                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];


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
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool GetInternationalLicenseInfoByDriverID(ref int ID, ref int ApplicationID, int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
            ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * from InternationalLicenses Where DriverID = @DriverID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;


                    ID = (int)reader["InternationalLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
             bool IsActive, int CreatedByUserID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "Update InternationalLicenses set IsActive = 0 where DriverID = @DriverID;" + //// this Line is for Deactivate any active Licenses exist before for this deiver
                "INSERT INTO InternationalLicenses " +
                "(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID) " +
                "VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID)" +
                "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




            try
            {
                Connection.Open();
                object scopeIdentity = Command.ExecuteScalar();


                Connection.Close();

                if (scopeIdentity != null && int.TryParse(scopeIdentity.ToString(), out int insertedID))
                {
                    Console.WriteLine($"License Was Added Succefully with ID {scopeIdentity}\n");
                    return insertedID;

                }
                else
                {
                    Console.WriteLine($"License Insertion Failed \n");
                    return -1;

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error  " + ex.Message);
                return -1;
            }
        }


        public static bool UpdateInternationalLicense(int TargetID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
             bool IsActive, int CreatedByUserID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = " UPDATE InternationalLicenses " +
                "SET ApplicationID = @ApplicationID , " +
                " DriverID = @DriverID , " +
                " IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID , " +
                " IssueDate = @IssueDate , " +
                " ExpirationDate = @ExpirationDate , " +
                " IsActive = @IsActive , " +
                " CreatedByUserID = @CreatedByUserID " +
                "WHERE InternationalLicenseID = @TargetInternationalLicenseID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@TargetInternationalLicenseID", TargetID);




            try
            {
                Connection.Open();
                AffectedRows = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return AffectedRows > 0;
        }


        public static bool DeleteInternationalLicense(int TargetID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "DELETE FROM InternationalLicenses WHERE InternationalLicenseID = @TargetID";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@TargetID", TargetID);

            try
            {
                Connection.Open();
                AffectedRows = Command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }


            return AffectedRows > 0;
        }

        public static bool IsInternationalLicenseExist(int ID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM InternationalLicenses Where InternationalLicenseID = @TargetID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", ID);


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
                Console.WriteLine("Error" + ex.Message);

                isExist = false;
            }
            finally
            {
                Connection.Close();
            }

            return isExist;


        }

        public static bool IsInternationalLicenseExistWithDriverID(int DriverID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM InternationalLicenses Where DriverID = @TargetID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TargetID", DriverID);


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
                Console.WriteLine("Error" + ex.Message);

                isExist = false;
            }
            finally
            {
                Connection.Close();
            }

            return isExist;


        }

        public static bool IsInternationalLicenseExistWithLocalLicenseID(int LocalLicenseID, ref int InternationalLicenseID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = InternationalLicenseID FROM InternationalLicenses Where IssuedUsingLocalLicenseID = @TargetID and IsActive = 1 ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("TargetID", LocalLicenseID);


            InternationalLicenseID = -1;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.Read())
                {
                    InternationalLicenseID = (int)reader["IsFound"];
                }

                else
                    InternationalLicenseID = -1;

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);

                InternationalLicenseID = -1;
            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseID != -1;

        }

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT * from InternationalLicenses order by IsActive, ExpirationDate desc";


            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllInternationalLicensesForDriver(int TargetDriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT InternationalLicenseID, ApplicationID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive " +
                " FROM InternationalLicenses " +
                " Where DriverID = @TargetDriverID ;";


            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetDriverID", TargetDriverID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static DataTable GetFilterdInternationalLicensesBy(string AttributeName, string AttributeValue)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses where " + AttributeName + " = @AttributeValue;";

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


    }
}
