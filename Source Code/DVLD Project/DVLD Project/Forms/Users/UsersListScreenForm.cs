using System;
using System.Data;
using UsersBusinessLayer;
using System.Windows.Forms;
using DVLD_Project.Global_Classes;

namespace DVLD_Project.Forms.Users
{
    public partial class UsersListScreenForm : Form
    {

        //// Variables
        private string SearchAttribute = String.Empty;
        string[] FilterTypeArr =
            {
                "UserID",
                "UserName",
                "PersonID",
                "FullName",
                "IsActive",
        };
        private static DataTable _dtAllUsers;

        public UsersListScreenForm()
        {
            InitializeComponent();
        }



        private void _RefreshContactsList(bool filter = false)
        {
            if (filter)
            {

                ///// if input is empty 
                if (Search_Input.Text.ToString().Trim() == string.Empty)
                {

                    _dtAllUsers = clsUser.GetAllUsers();
                    UsersGridView.DataSource = _dtAllUsers;
                }
                else
                {
                    if (SearchAttribute == "UserID" || SearchAttribute == "PersonID")
                        _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", SearchAttribute, Search_Input.Text.Trim());
                    else
                        _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", SearchAttribute, Search_Input.Text.Trim());

                }
            }
            else
            {
                ///// get Updated Data from Database

                _dtAllUsers = clsUser.GetAllUsers();
                UsersGridView.DataSource = _dtAllUsers;

                FilterMenu.SelectedIndex = 0;
                Search_Input.Visible = false;

                cbIsActive.SelectedIndex = 0;
                cbIsActive.Visible = false;

            }

            Users_records_num.Text = (UsersGridView.Rows.Count).ToString();
        }

        private void UsersListScreenForm_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();

            if (UsersGridView.Rows.Count > 0)
            {
                UsersGridView.Columns[0].HeaderText = "User ID";
                UsersGridView.Columns[0].Width = 150;

                UsersGridView.Columns[1].HeaderText = "Person ID";
                UsersGridView.Columns[1].Width = 150;

                UsersGridView.Columns[2].HeaderText = "Full Name";
                UsersGridView.Columns[2].Width = 410;

                UsersGridView.Columns[3].HeaderText = "UserName";
                UsersGridView.Columns[3].Width = 160;

                UsersGridView.Columns[4].HeaderText = "Is Active";
                UsersGridView.Columns[4].Width = 170;
            }


        }

        private void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterMenu.SelectedIndex == 0)
            {
                _RefreshContactsList();
                Search_Input.Visible = false;
            }
            else
            {
                SearchAttribute = FilterTypeArr[FilterMenu.SelectedIndex - 1];

                if (SearchAttribute != "IsActive")
                {
                    Search_Input.Visible = true;
                    cbIsActive.Visible = false;

                }
                else
                {
                    cbIsActive.Visible = true;
                    Search_Input.Visible = false;
                }

            }
            Search_Input.Text = string.Empty;
        }

        private void Search_Input_KeyPress(object sender, KeyPressEventArgs e) ////restrict entering string in Person ID Filiter
        {
            if (SearchAttribute == "PersonID" || SearchAttribute == "UserID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true; // Cancel the input if it's not a digit
                }
            }
        }

        private void Search_Input_TextChanged(object sender, EventArgs e)
        {
            _RefreshContactsList(true);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            Users_records_num.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void Close_People_Form(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UserInfoForm showUserInfo = new UserInfoForm((int)UsersGridView.CurrentRow.Cells[0].Value);

            showUserInfo.ShowDialog();

            _RefreshContactsList();
        }

        private void AddNewUsertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateUserForm NewUserInfo = new AddUpdateUserForm();
            NewUserInfo.ShowDialog();

            _RefreshContactsList();
        }

        private void EditPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateUserForm NewUserInfo = new AddUpdateUserForm((int)UsersGridView.CurrentRow.Cells[0].Value);

            NewUserInfo.ShowDialog();

            _RefreshContactsList();


        }

        private void DeletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentRowUserID = (int)UsersGridView.CurrentRow.Cells[0].Value;


            //// if you want to delete the last user in system that will be un acceptable the system must have at least one user
            if (UsersGridView.Rows.Count == 1)
            {
                MessageBox.Show("You only have one and the system must have at least one user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //// if you want to delete the current logged user
            if (currentRowUserID == clsGlobal.CurrentUser.ID)
            {
                MessageBox.Show("You can't delete this user because he is the current logged in user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete Person [" + currentRowUserID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(currentRowUserID))
                    MessageBox.Show("User was deleted successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _RefreshContactsList();
            }
        }

        private void ChangePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm((int)UsersGridView.CurrentRow.Cells[0].Value);
            changePasswordForm.ShowDialog();
        }

        private void SendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comming Soon🤌", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PhoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comming Soon🤌", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewUser_Btn_Click_1(object sender, EventArgs e)
        {
            AddUpdateUserForm NewUserInfo = new AddUpdateUserForm();
            NewUserInfo.ShowDialog();

            _RefreshContactsList();
        }


    }
}
