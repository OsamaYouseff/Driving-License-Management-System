using System;
using UsersBusinessLayer;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Project.Global_Classes;

namespace DVLD_Project.Forms.Users
{
    public partial class AddUpdateUserForm : Form
    {

        public delegate void CallMethodHandeler(bool Filiter);
        public delegate void DataBack(object sender, int UserID);
        public event CallMethodHandeler CallMethod;
        public event DataBack Databack;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        clsUser _User = new clsUser();
        clsUser _Backup_User = new clsUser();
        private int _UserID = -1;



        //// Current Info
        private string _OldUserName = string.Empty;
        private string _OldPassword = string.Empty;
        private string _OldConfirmPassword = string.Empty;
        private bool _OldIsActive = false;



        public AddUpdateUserForm()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;

            _SetDefualtValues();
        }

        public AddUpdateUserForm(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.Update;

            _UserID = UserID;

            _SetDefualtValues();

            _LoadData();

            SaveBtn.Enabled = false;

        }


        private void _fillUserData()
        {
            _User.PersonID = AddUserPersonCardPlusFiliter_CTRL.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;
        }

        private void _SetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tpLoginInfo.Enabled = false;

                AddUserPersonCardPlusFiliter_CTRL.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;


            }


            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
            SaveBtn.Enabled = false;


        }

        private void AddUpdateUserInfo_Load(object sender, EventArgs e)
        {
            _SetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();

        }

        private void _LoadData()
        {

            _User = clsUser.GetUserWithID(_UserID);

            AddUserPersonCardPlusFiliter_CTRL.FilterEnabled = false;


            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblUserID.Text = _User.ID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;

            try
            {
                AddUserPersonCardPlusFiliter_CTRL.LoadInfo(_User.PersonID);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : Can't Show User Info ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AddUserPersonCardPlusFiliter_CTRL.FilterEnabled = false;


            ////// Update _Backup_User
            _Backup_User.CopyUserInfo(_User);


        }

        private void tcUserInfo_TabIndexChanged(object sender, EventArgs e)
        {
            if (tcUserInfo.SelectedTab.Name == "tpPersonalInfo")
            {
                SaveBtn.Enabled = false;
                return;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ///// make sure that all fileds are valid and fullfilled  
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are incomplete or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _fillUserData();

            ///// check if there is no changes
            if (clsUser.AreTheyDuplicated(_User, _Backup_User))
            {
                MessageBox.Show("There are no changes in the information to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SaveBtn.Enabled = false;
                return;
            }

            _FillOldInfo();


            if (_User.Save())
            {
                lblUserID.Text = _User.ID.ToString();
                lblTitle.Text = "Update User";


                if (_Mode == enMode.AddNew)
                    MessageBox.Show("User Added Successfully with id = [ " + _User.ID + " ]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("User with id = [ " + _User.ID + " ] Updated Successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //// update bakcup User Info
                _Backup_User.CopyUserInfo(_User);

                ///// Invoke Method if it's exist
                Databack?.Invoke(this, _User.ID);

                _Mode = enMode.Update;
                SaveBtn.Enabled = false;



                //////// Important ////////
                /// this is an important step if the updated user is the current logged user so you have to change login data in login form 

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

                //////// Important ////////

            }
            else
                MessageBox.Show("Unable to add this user.", "Faild to add", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void ValidateEmptyString(object sender, CancelEventArgs e)
        {

            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            ValidateEmptyString(sender, e);
        }

        private void _FillOldInfo()
        {
            _OldUserName = txtUserName.Text.Trim();
            _OldPassword = txtPassword.Text.Trim();
            _OldConfirmPassword = txtConfirmPassword.Text.Trim();
            _OldIsActive = chkIsActive.Checked;

        }

        private void Next_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                SaveBtn.Enabled = false;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                _FillOldInfo();
                return;
            }

            //in case of add new mode.
            if (AddUserPersonCardPlusFiliter_CTRL.PersonID != -1)
            {
                if (clsUser.IsUserExistWithPersonID(AddUserPersonCardPlusFiliter_CTRL.PersonID))
                {
                    MessageBox.Show("The selected person already has a user account. Please choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddUserPersonCardPlusFiliter_CTRL.FilterFocus();
                }
                else
                {
                    SaveBtn.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddUserPersonCardPlusFiliter_CTRL.FilterFocus();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyString(sender, e);


            TextBox Temp = ((TextBox)sender);

            if (clsUser.IsUserExistWithUserName(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This UserName is used by another person try another one.");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            ValidateEmptyString(sender, e);

            TextBox Temp = ((TextBox)sender);

            if (Temp.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Passwords are Not Duplicated");
                return;
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

        //// TODO Password Validating
        private void passwordValidation()
        {

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            SaveBtn.Enabled = false;
            tpLoginInfo.Enabled = false;
            tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpPersonalInfo"];
            return;
        }

        private bool IsThereAnyChangeOfData()
        {
            if (_OldUserName != txtUserName.Text.Trim()) return true;
            if (_OldPassword != txtPassword.Text.Trim()) return true;
            if (_OldConfirmPassword != txtConfirmPassword.Text.Trim()) return true;
            if (_OldIsActive != chkIsActive.Checked) return true;

            return false;
        }

        private void FiledsTextChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = IsThereAnyChangeOfData();
        }

    }
}
