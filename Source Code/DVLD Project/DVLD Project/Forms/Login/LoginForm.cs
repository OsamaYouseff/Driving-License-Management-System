using System;
using UsersBusinessLayer;
using System.Diagnostics;
using System.Windows.Forms;
using DVLD_Project.Global_Classes;

namespace DVLD_Project.Forms.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

        }

        private void _ResetLogin()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;

            chkRememberMe.Checked = false;
        }

        public void RefreshLoginData(string UserName, string Password)
        {

            if (chkRememberMe.Checked)
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
            }
            else
                _ResetLogin();


        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string UserName = string.Empty, Password = string.Empty;

            if (clsGlobal.GetStoredUsernameAndPasswordFromWindowsRegistry(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                _ResetLogin();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            clsUser User1 = clsUser.GetUserWithUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User1 == null)
            {
                MessageBox.Show("The username or password is incorrect.", "Wrong Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobal.SetEventLog("The username or password is incorrect.", EventLogEntryType.Error);
                return;
            }


            //incase the user is not active
            if (!User1.IsActive)
            {

                txtUserName.Focus();
                MessageBox.Show("Your account is not active. Please contact the administrator.", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobal.SetEventLog("Your account is not active. Please contact the administrator.", EventLogEntryType.Error);

                return;
            }


            ///// Remeber UserName and Password
            if (chkRememberMe.Checked)
            {
                try
                {
                    clsGlobal.RememberUsernameAndPasswordInWindowsRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                    clsGlobal.SetEventLog("Error : " + ex.Message, EventLogEntryType.Error);

                }
            }
            else
            {
                _ResetLogin();
            }


            clsGlobal.CurrentUser = User1;
            clsGlobal.SetLoginForm(this);
            this.Visible = false;
            MainScreenForm mainForm = new MainScreenForm(this);
            mainForm.Show();

        }
    }
}
