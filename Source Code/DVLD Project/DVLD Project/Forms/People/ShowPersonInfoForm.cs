using System;
using PeopleBusinessLayer;
using System.Windows.Forms;

namespace DVLD_Project.Forms.People
{
    public partial class ShowPersonInfoForm : Form
    {
        public ShowPersonInfoForm(int CurrentPersonId)
        {
            InitializeComponent();

            personCard_CTRL1.LoadPersonInfo(CurrentPersonId);

        }

        public ShowPersonInfoForm(clsPerson CurrentPerson)
        {
            InitializeComponent();

            personCard_CTRL1.LoadPersonInfo(CurrentPerson);

        }

        public ShowPersonInfoForm(string NationalNo)
        {
            InitializeComponent();
            personCard_CTRL1.LoadPersonInfo(NationalNo);

        }

        private void Close_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
