using System;
using System.Data;
using PeopleDataAccessLayer;
using System.Data.SqlClient;

namespace ApplicationDataAccessLayer
{
    public class clsApplicationDataAccess
    {

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) " +
                           "VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID)" +
                           "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                Connection.Open();
                object scopeIdentity = Command.ExecuteScalar();


                Connection.Close();

                if (scopeIdentity != null && int.TryParse(scopeIdentity.ToString(), out int insertedID))
                {
                    //Console.WriteLine($"Application Was Added Succefully with ID {scopeIdentity}\n");
                    return insertedID;

                }
                else
                {
                    Console.WriteLine($"Application Insertion Failed \n");
                    return -1;

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error  " + ex.Message);
                return -1;
            }

        }

        public static bool UpdateApplication(int TargetApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE Applications " +
                           "SET ApplicantPersonID = @ApplicantPersonID, " +
                           "ApplicationDate = @ApplicationDate, " +
                           "ApplicationTypeID = @ApplicationTypeID, " +
                           "ApplicationStatus = @ApplicationStatus, " +
                           "LastStatusDate = @LastStatusDate, " +
                           "PaidFees = @PaidFees, " +
                           "CreatedByUserID = @CreatedByUserID " +
                           "WHERE ApplicationID = @TargetApplicationID;";

            SqlCommand Command = new SqlCommand(query, Connection);


            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@TargetApplicationID", TargetApplicationID);


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

        public static bool GetApplicationInfoByID(int TargetID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "select * from Applications Where ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", TargetID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);

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

        public static bool DeleteApplication(int TargetID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "DELETE FROM Applications WHERE ApplicationID = @TargetID;";

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

        public static bool IsApplicationExist(int TargetID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Applications Where ApplicationID = @TargetID ";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", TargetID);

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

        public static bool IsApplicationExistWithSpaceficAttributes(int ApplicationID, int ApplicationType, int ApplicationStatus)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Applications Where ApplicationID = @ApplicationID " +
                           "and ApplicationTypeID = @ApplicationType " +
                           "and ApplicationStatus = @ApplicationStatus ;";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

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

        public static bool IsApplicationExistWithApplicationType(int TargetID, int ApplicationType)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM Applications Where ApplicationID = @TargetID and ApplicationTypeID = @ApplicationTypeID";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", TargetID);
            Command.Parameters.AddWithValue("@ApplicationType", ApplicationType);

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

        public static bool DoesPersonAlreadyHaveALocalApplicationActiveOrCompleted(int TargetPersonId, int ApplicationTypeID, ref int AppLicationID)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsExist = ApplicationID FROM Applications Where ApplicantPersonID = @TargetPersonId and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus in (1,3);";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetPersonId", TargetPersonId);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            bool isExist = false;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.HasRows)
                {
                    isExist = true;

                    if (reader.Read())
                    {
                        AppLicationID = (int)reader["IsExist"];

                    }
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

        public static bool UpdateStatus(int TargetApplicationID, short NewStatus)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE Applications " +
                           "Set ApplicationStatus = @NewStatus, " +
                           "LastStatusDate = @LastStatusDate " +
                           "WHERE ApplicationID = @TargetApplicationID;";

            SqlCommand Command = new SqlCommand(query, Connection);


            int AffectedRows = 0;

            Command.Parameters.AddWithValue("@NewStatus", NewStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            Command.Parameters.AddWithValue("@TargetApplicationID", TargetApplicationID);


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

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "select * from Applications order by ApplicationDate desc";


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

        public static DataTable GetFilterdLicenseBy(string AttributeName, string AttributeValue)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM Applications where " + AttributeName + " = @AttributeValue;";

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
