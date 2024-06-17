using System;
using UsersBusinessLayer;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Project.Global_Classes;

namespace DVLD_Project.Forms.Users
{
    public partial class ChangePasswordForm : Form
    {
        clsUser _User;
        clsUser _Backup_User = new clsUser();
        private int _UserID = -1;

        public delegate void DataBack(object sender, int UserID);
        public event DataBack Databack;

        public ChangePasswordForm()
        {
            InitializeComponent();
        }
        public ChangePasswordForm(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            _LoadData();

        }


        private void _LoadData()
        {

            _User = clsUser.GetUserWithID(_UserID);
            changePass_personCardPlusFiliter_CTRL.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }

            //the following code will not be executed if the person was not found

            try
            {
                changePass_personCardPlusFiliter_CTRL.LoadInfo(_User.PersonID);


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : Can't Show User Info ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            changePass_personCardPlusFiliter_CTRL.FilterEnabled = false;


            ///// take backup
            _Backup_User.CopyUserInfo(_User);

        }

        private void _SsetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ////// filling new password to User
            _User.Password = txtNewPassword.Text.Trim();


            ///// check if there is no changes
            if (clsUser.AreTheyDuplicated(_User, _Backup_User))
            {
                MessageBox.Show("This password has already been used. Please try another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are incomplete or invalid.", "Incomplete or invalid fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_User.Save())
            {


                MessageBox.Show("User's password has been updated successfully ", "Inforrmation", MessageBoxButtons.OK, MessageBoxIcon.Information);


                _SsetDefaultValues();

                //// update bakcup User Info
                _Backup_User.CopyUserInfo(_User);

                ///// Invoke Method if it's exist
                Databack?.Invoke(this, _User.ID);

            }
            else
                MessageBox.Show("Unable to update the password for this user.", "Faild to update", MessageBoxButtons.OK, MessageBoxIcon.Error);



            ////// this is an important step if the updated user is the current logged user so you have to change login data in login form 

            if (_User.ID == clsGlobal.CurrentUser.ID)
            {
                /// Updata CurrentUser in Global Class
                clsGlobal.CurrentUser = _User;

                /// Refresh Login Fileds' Data 
                try
                {
                    clsGlobal.RememberUsernameAndPasswordInWindowsRegistry(_User.UserName, _User.Password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }

            }


        }


        //// Check if Current Password is valid
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (Temp.Text.Trim() != _Backup_User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Passsword is not correct!");
                return;
            }
            if (Temp.Text.Trim() == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Please Fill Current Password!");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (Temp.Text.Trim() == _Backup_User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Please You a new Password!");
                return;
            }
            if (Temp.Text.Trim() == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Please Fill new Password!");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (Temp.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Passwords are Not Duplicated");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

    }
}
