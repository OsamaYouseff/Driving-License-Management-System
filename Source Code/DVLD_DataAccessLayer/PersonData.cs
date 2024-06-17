using System;
using System.Data;
using System.Data.SqlClient;

namespace PeopleDataAccessLayer
{
    public class clsPersonDataAccess
    {

        public static bool GetPersonInfoByID(int ID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref
            string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth, ref string Gender, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] == DBNull.Value)
                        ThirdName = "";
                    else
                        ThirdName = (string)reader["ThirdName"];

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (string)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] == DBNull.Value)
                        Email = "";
                    else
                        Email = (string)reader["Email"];


                    NationalityCountryID = (int)reader["NationalityCountryID"];



                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";

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

        public static bool GetPersonInfoByNationalNo(ref int ID, string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref
            string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth, ref string Gender, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (string)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "N/A";


                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";

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

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, string Gender, int NationalityCountryID, string ImagePath)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "INSERT INTO People (NationalNo, FirstName,SecondName ,ThirdName , LastName, DateOfBirth ,Gender, Address, Phone , Email ,NationalityCountryID ,ImagePath)  " +
                "VALUES (@NationalNo, @FirstName,@SecondName ,@ThirdName , @LastName, @DateOfBirth ,@Gender, @Address, @Phone , @Email ,@NationalityCountryID ,@ImagePath); " +
                "SELECT SCOPE_IDENTITY();";


            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);

            if (Email.Trim() == "")
                Command.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Email", Email);


            Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            Command.Parameters.AddWithValue("@ImagePath", ImagePath);




            try
            {
                Connection.Open();
                object scopeIdentity = Command.ExecuteScalar();


                Connection.Close();

                if (scopeIdentity != null && int.TryParse(scopeIdentity.ToString(), out int insertedID))
                {
                    //Console.WriteLine($"Person Was Added Succefully with ID {scopeIdentity}\n");
                    return insertedID;

                }
                else
                {
                    //Console.WriteLine($"Person Insertion Failed \n");
                    return -1;

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error" + ex.Message);
                return -1;
            }
        }

        public static bool UpdatePerson(int TargetID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, string Gender, int NationalityCountryID, string ImagePath)
        {


            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "UPDATE People " +
                           "SET NationalNo = @NationalNo," +
                           "FirstName = @FirstName, " +
                           "SecondName = @SecondName, " +
                           "ThirdName = @ThirdName, " +
                           "LastName = @LastName, " +
                           "Email = @Email," +
                           "Phone = @Phone," +
                           "Address = @Address," +
                           "DateOfBirth = @DateOfBirth," +
                           "Gender = @Gender, " +
                           "NationalityCountryID = @NationalityCountryID ," +
                           "ImagePath  = @ImagePath " +
                           "WHERE PersonID = @TargetID ;";

            SqlCommand Command = new SqlCommand(query, Connection);


            int AffectedRows = 0;
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            Command.Parameters.AddWithValue("@TargetID", TargetID);


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

        public static bool DeletePerson(int TargetID)
        {


            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "DELETE FROM People WHERE People.PersonID = @TargetID";

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
                //Console.WriteLine("Error" + ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }


            return AffectedRows > 0;
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT * FROM PersonFullView";
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

        public static DataTable GetFilterdPeople(string AttributeName, string AttributeValue)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);


            ////// handel Gender 
            if (AttributeName == "Gender")
            {
                if (AttributeValue.ToLower() == "m".ToString() || AttributeValue.ToLower() == "male")
                    AttributeValue = "male";
                else if (AttributeValue.ToLower() == "f".ToString() || AttributeValue.ToLower() == "female")
                    AttributeValue = "Female";
                else
                {
                    AttributeValue = "";
                    return dt;
                }
            }

            string query = "SELECT * FROM PersonFullView Where " + AttributeName + " =  @AttributeValue ";


            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@AttributeValue", AttributeValue);


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

        public static bool IsPersonExist(int ID)
        {

            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "SELECT IsFound = 1 FROM PersonFullView Where PersonID = @TargetID ";
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
                //Console.WriteLine("Error" + ex.Message);

                isExist = false;
            }
            finally
            {
                Connection.Close();
            }

            return isExist;


        }

        public static bool IsNationalNoUnique(string NationalNo)
        {
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);

            string query = "SELECT NationalNo FROM People Where NationalNo = @NationalNo";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            bool result = true;
            try
            {
                Connection.Open();
                Object scaler = Command.ExecuteScalar();
                if (scaler != null)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }

                Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }

            return result;

        }

        public static int GetPersonAge(int ID)
        {
            int Age = -1;
            SqlConnection Connection = new SqlConnection(AccessSettingsData.ConnectionString);
            string query = "Select  DATEDIFF (year , People.DateOfBirth,GETDATE() ) as Age  from People where PersonID = @TargetID ;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TargetID", ID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();


                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Age = (int)reader["Age"];

                    }
                }
                else
                    Age = -1;

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                Age = -1;
            }
            finally
            {
                Connection.Close();
            }

            return Age;

        }

    }
}
