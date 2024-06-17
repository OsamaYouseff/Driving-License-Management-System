using System;
using System.IO;
using System.Linq;
using System.Configuration;

namespace PeopleDataAccessLayer
{
    public static class AccessSettingsData
    {
        ///// Key = "1234123412341234"
        public static readonly string Key = "1234123412341234";

        ////// To Connect to local database if your are using sql management studio

        //public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;";

        ///-------------------------------------------------------------------------------------------------------------------------

        /*
        public static string GetConnectionString()
        {
            // Get the directory where the executable is located
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Assuming your database file is named "DVLD.mdf" and is located in the executable directory
            string databaseFilePath = Path.Combine(executableDirectory, "DVLD.mdf");

            // Check if the database file exists
            if (File.Exists(databaseFilePath))
            {
                // Construct the connection string
                string ConnectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{databaseFilePath}\";Integrated Security=True";
                return ConnectionString;
            }
            else
            {
                throw new FileNotFoundException("Database file not found.");
            }
        }

        


        public static string GetConnectionString()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            return connectionString;
        }

        public static string ConnectionString = GetConnectionString();
            
         */

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DVLD-Database\DVLD.mdf;Integrated Security=True";


    }

}

