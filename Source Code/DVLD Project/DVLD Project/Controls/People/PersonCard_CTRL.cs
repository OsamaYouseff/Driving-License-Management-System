using System;
using System.IO;
using PeopleBusinessLayer;
using System.Windows.Forms;
using DVLD_Project.Properties;
using DVLD_Project.Forms.People;

namespace DVLD_Project.Controls.People
{
    public partial class PersonCard_CTRL : UserControl
    {
        clsPerson _Person;
        private int _PersonID = -1;


        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public PersonCard_CTRL()
        {
            InitializeComponent();

        }



        public void LoadPersonInfo(int PersonID)
        {

            _PersonID = PersonID;
            _Person = clsPerson.GetPerson(PersonID);


            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Unable to locate information for this person.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(clsPerson CurrentPerson)
        {

            _PersonID = CurrentPerson.ID;
            _Person = CurrentPerson;

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Unable to locate information for this person", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.GetPerson(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Unable to locate information for this person", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

            //// Update PersonID 
            _PersonID = _Person.ID;
        }

        private void PersonCard_CTRL_Load(int PersonID)
        {
            _Person = clsPerson.GetPerson(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Unable to locate information for this person", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void _FillPersonInfo()
        {

            llEditPersonInfo.Enabled = true;

            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblPersonID.Text = _Person.ID.ToString();
            lblFullName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblEmail.Text = _Person.Email;
            lblGendor.Text = _Person.Gender;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsCountry.GetCountry(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;

            if (_Person.ImagePath == "")
            {
                pbPersonImage.ImageLocation = "";
                if (_Person.Gender == "Male")
                    pbPersonImage.Image = Resources.icons8_person_94;
                else
                    pbPersonImage.Image = Resources.icons8_businesswoman_94;
            }

            if (_Person.ImagePath != "")
            {
                try
                {
                    pbPersonImage.Load(_Person.ImagePath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The profile images cannot be found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }




        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == "Male")
                pbPersonImage.Image = Resources.icons8_person_94;
            else
                pbPersonImage.Image = Resources.icons8_businesswoman_94;


            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image : " + ImagePath, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);



            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.icons8_person_94;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.icons8_person_94;
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (_PersonID == -1)
            {
                MessageBox.Show("The provided ID is invalid.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddUpdatePersonInfo addUpdatePersonInfoForm = new AddUpdatePersonInfo(_PersonID);

            addUpdatePersonInfoForm.ShowDialog();

            PersonCard_CTRL_Load(_PersonID);

        }

    }
}
