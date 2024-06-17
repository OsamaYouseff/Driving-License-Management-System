using System;
using PeopleBusinessLayer;
using DriversBusinessLayer;
using System.Windows.Forms;

namespace DVLD_Project.Forms.Licences
{
    public partial class ShowPersonLicenceHistoryForm : Form
    {

        public clsDriver Driver = new clsDriver();


        public ShowPersonLicenceHistoryForm()
        {
            InitializeComponent();
        }
        public ShowPersonLicenceHistoryForm(int DriverID)
        {
            InitializeComponent();

            Driver = clsDriver.GetDriverWithID(DriverID);

            if (Driver == null)
            {
                MessageBox.Show("Can't find the information ", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            personCardPlusFiliter_CTRL.LoadInfo(Driver.PersonID);

            driverLicenceHistory_CTRL.LoadInfo(DriverID);

        }
        public ShowPersonLicenceHistoryForm(string NationalNo)
        {
            InitializeComponent();

            try
            {
                Driver = clsDriver.GetDriverWithNationalNo(NationalNo);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Driver != null)
            {

                clsPerson person;

                try
                {
                    person = clsPerson.GetPerson(NationalNo);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                personCardPlusFiliter_CTRL.LoadInfo(person.ID);

            }
            else
            {
                MessageBox.Show("This person is not a driver, so they do not have any license history yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            personCardPlusFiliter_CTRL.LoadInfo(Driver.PersonID);
            driverLicenceHistory_CTRL.LoadInfo(Driver.ID);

        }


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowPersonLicenceHistoryForm_Load(object sender, EventArgs e)
        {
            personCardPlusFiliter_CTRL.FilterEnabled = false;
        }

    }
}
