using System;
using PeopleBusinessLayer;
using System.Windows.Forms;
using DVLD_Project.Forms.People;

namespace DVLD_Project.Controls.People
{
    public partial class PersonCardPlusFiliter_CTRL : UserControl
    {

        public int PersonID
        {
            get { return PersonInfo_Card.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return PersonInfo_Card.SelectedPersonInfo; }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                AddPerson_Btn.Visible = _ShowAddPerson;
            }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }



        public PersonCardPlusFiliter_CTRL()
        {
            InitializeComponent();

        }
        public PersonCardPlusFiliter_CTRL(int PersonID)
        {
            InitializeComponent();

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            PersonInfo_Card.LoadPersonInfo(PersonID);

        }

        public void LoadInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            PersonInfo_Card.LoadPersonInfo(PersonID);
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void PersonCardPlusFiliter_CTRL_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }

        private void SearchPerson_Btn_Click(object sender, EventArgs e)
        {

            if (txtFilterValue.Text.Trim() == string.Empty) return;

            if (cbFilterBy.SelectedIndex == 0) //// if SelectedIndex is National No
                PersonInfo_Card.LoadPersonInfo(txtFilterValue.Text.Trim());
            else //// if SelectedIndex is Person ID
                PersonInfo_Card.LoadPersonInfo(int.Parse(txtFilterValue.Text.Trim()));
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                SearchPerson_Btn.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void AddPerson_Btn_Click(object sender, EventArgs e)
        {
            AddUpdatePersonInfo addUpdatePersonInfo = new AddUpdatePersonInfo();

            addUpdatePersonInfo.Databack += DataBackEvent;

            addUpdatePersonInfo.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            //// Handle the data receive
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            PersonInfo_Card.LoadPersonInfo(PersonID);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterValue.Text.Trim() != string.Empty && !SearchPerson_Btn.Enabled) SearchPerson_Btn.Enabled = true;
            if (txtFilterValue.Text.Trim() == string.Empty && SearchPerson_Btn.Enabled) SearchPerson_Btn.Enabled = false;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = String.Empty;
        }
    }
}
