using System;
using System.Data;
using PeopleBusinessLayer;
using System.Windows.Forms;
using DVLD_Project.Forms.People;

namespace DVLD_Project.Forms
{
    public partial class PeopleListScreen : Form
    {
        private string SearchAttribute = String.Empty;
        string[] FilterTypeArr =
            {
                "PersonID",
                "NationalNo",
                "FirstName",
                "SecondName",
                "ThirdName",
                "LastName",
                "CountryName",
                "Gender",
                "Phone",
                "Email",
                "Address",
        };
        private static DataTable _dtAllPeople;


        public PeopleListScreen()
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

                    _dtAllPeople = clsPerson.GetAllPeople();
                    PeopleGridView.DataSource = _dtAllPeople;
                }
                else
                {
                    if (SearchAttribute == "PersonID")
                        _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", SearchAttribute, Search_Input.Text.Trim());
                    else
                        _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", SearchAttribute, Search_Input.Text.Trim());
                }
            }
            else
            {
                ///// get Updated Data from Database
                _dtAllPeople = clsPerson.GetAllPeople();
                PeopleGridView.DataSource = _dtAllPeople;
                FilterMenu.SelectedIndex = 0;
            }

            People_records_num.Text = (PeopleGridView.Rows.Count).ToString();
        }

        private void PeopleScreenForm_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
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
                Search_Input.Visible = true;

            }


            Search_Input.Text = string.Empty;

        }

        //// restrict entering string in Person ID Filiter
        private void Search_Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && SearchAttribute == "PersonID")
            {
                e.Handled = true; // Cancel the input if it's not a digit
            }
        }

        private void Search_Input_TextChanged(object sender, EventArgs e)
        {
            _RefreshContactsList(true);
        }

        private void Close_People_Form(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowPersonInfoForm showPersonInfo = new ShowPersonInfoForm((int)PeopleGridView.CurrentRow.Cells[0].Value);
            showPersonInfo.ShowDialog();

            _RefreshContactsList();
        }

        private void AddNewPerson_Click(object sender, EventArgs e)
        {
            AddUpdatePersonInfo AddUpdatePersonInfo = new AddUpdatePersonInfo();
            AddUpdatePersonInfo.ShowDialog();

            _RefreshContactsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdatePersonInfo AddUpdatePersonInfo = new AddUpdatePersonInfo((int)PeopleGridView.CurrentRow.Cells[0].Value);

            AddUpdatePersonInfo.ShowDialog();

            _RefreshContactsList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + PeopleGridView.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeleteAPerson((int)PeopleGridView.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person was deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Faild To delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _RefreshContactsList();
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comming Soon🤌", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comming Soon🤌", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void AddNewPerson_Btn_Click(object sender, EventArgs e)
        {
            AddUpdatePersonInfo AddUpdatePersonInfo = new AddUpdatePersonInfo();
            AddUpdatePersonInfo.CallMethod += _RefreshContactsList;
            AddUpdatePersonInfo.ShowDialog();

            _RefreshContactsList();
        }
    }

}

