using System;
using System.Data;
using System.Text;
using UsersDataAccessLayer;
using PeopleDataAccessLayer;
using System.Security.Cryptography;

namespace UsersBusinessLayer

{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public clsUser()
        {
            this.ID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;

        }
        private clsUser(int ID, int PersonID, string UserName, string Password, bool IsActive)

        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;

        }


        private bool _AddNewUser()
        {
            //// enycrpte password before saving data 
            string EnycrptePassword = EncryptPassword(this.Password);

            //// Data Layer Method & get The scopeIdentity key
            this.ID = clsUsersDataAccess.AddNewUser(this.PersonID, this.UserName, EnycrptePassword, this.IsActive);

            return this.ID != -1;
        }
        private bool _UpdateUser()
        {
            //// enycrpte password before saving data 
            string EnycrptePassword = EncryptPassword(this.Password);

            //// Data Layer Method & get The scopeIdentity key
            return clsUsersDataAccess.UpdateUser(this.ID, this.PersonID, this.UserName, EnycrptePassword, this.IsActive);


        }


        public void CopyUserInfo(clsUser User)
        {
            this.ID = User.ID;
            this.PersonID = User.PersonID;
            this.UserName = User.UserName;
            this.Password = User.Password;
            this.IsActive = User.IsActive;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();


            }

            return false;

        }

        //// Encrypt Methods
        public static string EncryptPassword(string Password)
        {
            string Key = AccessSettingsData.Key;

            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);

                /*
                Here, you are setting the IV of the AES algorithm to a block of bytes 
                with a size equal to the block size of the algorithm divided by 8. 
                The block size of AES is typically 128 bits (16 bytes), 
                so the IV size is 128 bits / 8 = 16 bytes.
                 */
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(Password);
                    }

                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        public static string DecryptPassword(string Password)
        {
            string Key = AccessSettingsData.Key;

            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(Password)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    // Read the decrypted data from the StreamReader
                    return srDecrypt.ReadToEnd();
                }
            }
        }


        public static bool AreTheyDuplicated(clsUser User1, clsUser User2)
        {
            if (User1.ID != User2.ID) return false;
            else if (User1.PersonID != User2.PersonID) return false;
            else if (User1.UserName.Trim() != User2.UserName.Trim()) return false;
            else if (User1.Password.Trim() != User2.Password.Trim()) return false;
            else if (User1.IsActive != User2.IsActive) return false;

            return true;
        }
        public static clsUser GetUserWithID(int ID)
        {
            string UserName = "", Password = "";
            int PersonID = -1;
            bool IsActive = false;

            if (clsUsersDataAccess.GetUserInfoByID(ID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                //// DecryptPassword
                Password = DecryptPassword(Password);
                return new clsUser(ID, PersonID, UserName, Password, IsActive);
            }

            else
                return null;

        }
        public static clsUser GetUserWithPersonID(int PersonID)
        {
            int ID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;


            if (clsUsersDataAccess.GetUserInfoByPersonID(ref ID, PersonID, ref UserName, ref Password, ref IsActive))
            {
                //// DecryptPassword
                Password = DecryptPassword(Password);
                return new clsUser(ID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;

        }
        public static clsUser GetUserWithUserNameAndPassword(string UserName, string Password)
        {
            int ID = -1, PersonID = -1;
            bool IsActive = false;

            //// EncryptPassword to find it in database 
            string EncryptedPassword = EncryptPassword(Password);

            if (clsUsersDataAccess.GetUserInfoByUserNameAndPassword(ref ID, ref PersonID, UserName, EncryptedPassword, ref IsActive))

                return new clsUser(ID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }


        public static bool DeleteUser(int ID)
        {
            return clsUsersDataAccess.DeleteUser(ID);
        }
        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();

        }
        public static DataTable GetFilterdUsersBy(string AttributeName, string AttributeValue)
        {
            return clsUsersDataAccess.GetFilterdUsers(AttributeName, AttributeValue);

        }
        public static bool IsUserExist(int ID)
        {
            return clsUsersDataAccess.IsUserExist(ID);
        }
        public static bool IsUserExistWithPersonID(int ID)
        {
            return clsUsersDataAccess.IsUserExistWithPersonID(ID);
        }
        public static bool IsUserExistWithUserName(string USerName)
        {
            return clsUsersDataAccess.IsUserExistWithUserName(USerName);
        }
        public static bool ChangePassword(int UserID, string Password)
        {
            //// Data Layer Method & get The scopeIdentity key

            return clsUsersDataAccess.ChangePassword(UserID, Password);


        }

    }
}
