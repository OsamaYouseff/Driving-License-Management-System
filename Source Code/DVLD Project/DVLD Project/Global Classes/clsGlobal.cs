using System;
using System.IO;
using Microsoft.Win32;
using UsersBusinessLayer;
using System.Diagnostics;
using System.Windows.Forms;
using DVLD_Project.Forms.Login;

namespace DVLD_Project.Global_Classes
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;
        public static LoginForm LoginForm;


        public static void SetLoginForm(LoginForm NewLoginForm)
        {
            LoginForm = NewLoginForm;
        }

        //// to store login data in txt file
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool RememberUsernameAndPasswordInWindowsRegistry(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Project";

            string UsernameKey = "Username", UsernameValue = Username;
            string PasswordKey = "Password", PasswordValue = Password;


            //// Enycrpt Password 
            PasswordValue = clsUser.EncryptPassword(PasswordValue);


            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, UsernameKey, UsernameValue, RegistryValueKind.String);
                Registry.SetValue(keyPath, PasswordKey, PasswordValue, RegistryValueKind.String);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

            return true;
        }

        //// to get login data in txt file
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredUsernameAndPasswordFromWindowsRegistry(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Project";
            string UsernameKey = "Username";
            string PasswordKey = "Password";


            try
            {
                string RetrivedUsernameValue = Registry.GetValue(keyPath, UsernameKey, null) as string;
                string RetrivedPasswordValue = Registry.GetValue(keyPath, PasswordKey, null) as string;



                if (RetrivedUsernameValue == null || RetrivedPasswordValue == null)
                    // can't Find Data in Windows Registry 
                    return false;
                else
                {
                    Username = RetrivedUsernameValue;

                    ///// DecryptPassword
                    Password = clsUser.DecryptPassword(RetrivedPasswordValue);
                }
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static void SetEventLog(string Message, EventLogEntryType EventLogEntryType)
        {
            string sourceName = "DVLD_Project";

            if (!EventLog.SourceExists(sourceName))
                EventLog.CreateEventSource(sourceName, "Application");


            EventLog.WriteEntry(sourceName, Message, EventLogEntryType);

        }


    }
}
