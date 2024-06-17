using System;
using System.Data;
using PeopleDataAccessLayer;
using System.Data.SqlClient;

namespace LicenseDataAccessLayer
{
    public class clsLicensesDataAccess
    {

        public static bool GetLicenseInfoByID(int ID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive,
            ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * from Licenses Where LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", ID);

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
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];


                    if (reader["Notes"] == DBNull.Value) Notes = "";
                    else Notes = (string)reader["Notes"];


                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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

        public static bool GetLicenseWithDriverIDAndLicenseClassName(string LicenseClassName, ref int ID, ref int ApplicationID, int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive,
            ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT Licenses.*, LicenseClasses.ClassName " +
                " FROM Licenses INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID " +
                " WHERE (Licenses.DriverID = @DriverID) AND (LicenseClasses.ClassName= @LicenseClassName) ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ID = (int)reader["LicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    LicenseClass = (int)reader["LicenseClass"];


                    if (reader["Notes"] == DBNull.Value) Notes = "";
                    else Notes = (string)reader["Notes"];

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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


        public static bool GetLicenseInfoByApplicationIDAndLicenseClassName(string LicenseClassName, ref int ID, int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive,
            ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT Licenses.*, LicenseClasses.ClassName " +
                " FROM Licenses INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID " +
                " WHERE (Licenses.ApplicationID = @ApplicationID) AND (LicenseClasses.ClassName= @LicenseClassName )";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value) Notes = "";
                    else Notes = (string)reader["Notes"];

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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



        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate
                 , string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "INSERT INTO Licenses " +
            "(ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) " +
            "VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID) " +
            "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes.Trim() == "" || Notes.Trim() == string.Empty)
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Notes", Notes);

            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
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


        public static bool UpdateLicense(int TargetID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate
                 , string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = " UPDATE Licenses " +
                           " SET ApplicationID = @ApplicationID, DriverID = @DriverID, " +
                           " LicenseClass = @LicenseClass, IssueDate = @IssueDate," +
                           " ExpirationDate = @ExpirationDate, Notes = @Notes," +
                           " PaidFees = @PaidFees, IsActive = @IsActive," +
                           " IssueReason = @IssueReason," +
                           " CreatedByUserID = @CreatedByUserID " +
                           " WHERE LicenseID = @LicenseID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes.Trim() == "" || Notes.Trim() == string.Empty)
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Notes", Notes);

            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@LicenseID", TargetID);

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


        public static bool DeleteLicense(int TargetID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "DELETE FROM Licenses WHERE LicenseID = @TargetID;";

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

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "Select * From Licenses ;";


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

        public static DataTable GetAllLocalLicensesForDriver(int TargetDriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = " SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName," +
                           " Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive FROM Licenses INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID " +
                           " WHERE (Licenses.DriverID = @TargetDriverID);";


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

        public static bool IsLicenseExist(int ID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Licenses Where LicenseID = @TargetID ;";
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

        public static bool IsLicenseExistWithDriverIDAndLicenseClassID(int ID, int LicenseClass)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Licenses Where LicenseID = @TargetID and LicenseClass = @LicenseClass ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", ID);
            Command.Parameters.AddWithValue("@LicenseClass", @LicenseClass);


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

        public static bool IsThereLicenseHasThisApplicationID(int ApplicationID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Licenses Where ApplicationID = @TargetID ;";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", ApplicationID);

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

        public static bool IsLicenseExistWithDriverID(int DriverID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Licenses Where DriverID = @TargetID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", DriverID);


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

        public static DataTable GetFilterdLicensesBy(string AttributeName, string AttributeValue)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM Licenses where " + AttributeName + " = @AttributeValue;";

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

        public static bool Deactivate(int TargetID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE Licenses Set IsActive = 0 Where LicenseID = @TargetLicenseID";

            SqlCommand Command = new SqlCommand(query, Connection);

            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@TargetLicenseID", TargetID);


            try
            {
                Connection.Open();
                AffectedRows = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return AffectedRows > 0;

        }

        public static bool DoesDriverHasAValidLicenseWithThisLicenseClass(int DriverID, int LicenseClassID, ref int ValidLicenseID, ref bool IsActive)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT * from Licenses Where (DriverID = @DriverID) and (LicenseClass = @LicenseClass) and (ExpirationDate > GETDATE());";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            bool isExist = false;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isExist = true;

                    ValidLicenseID = (int)reader["LicenseID"];
                    IsActive = (bool)reader["IsActive"];
                }
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
    }
}


