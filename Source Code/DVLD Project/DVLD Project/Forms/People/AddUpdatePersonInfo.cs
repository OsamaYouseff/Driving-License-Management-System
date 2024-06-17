using System;
using System.IO;
using System.Data;
using PeopleBusinessLayer;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Project.Properties;
using DVLD_Project.Global_Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DVLD_Project.Forms.People
{
    public partial class AddUpdatePersonInfo : Form
    {
        public delegate void CallMethodHandeler(bool Filiter);
        public delegate void DataBack(object sender, int PersonID);
        public event CallMethodHandeler CallMethod;
        public event DataBack Databack;


        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        clsPerson _Person = new clsPerson();
        clsPerson _Backup_Person = new clsPerson();

        public AddUpdatePersonInfo()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;

        }
        public AddUpdatePersonInfo(int CurrentPersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;

            _Person.ID = CurrentPersonID;

        }


        private void _fillPersonData()
        {
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Gender = (rbMale.Checked ? "Male" : "Female").Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = cbCountry.SelectedIndex + 1;
            _Person.ImagePath = pbPersonImage.Location.ToString();

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = (string)pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";
        }

        private void getAllContriesData()
        {

            DataTable CountriesDT = clsCountry.GetAllCountries();

            foreach (DataRow country in CountriesDT.Rows)
            {
                cbCountry.Items.Add(country["CountryName"]);
            }
        }

        private void _LoadAllData()
        {
            getAllContriesData();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = this.Text = "Add new person";
                _Person = new clsPerson();
                return;

            }
            else
            {
                _Person = clsPerson.GetPerson(_Person.ID);

                this.Text = "Edit person info";
                lblTitle.Text = "Edit " + _Person.FirstName + "'s info";
            }




            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because there is no contact with the provided ID = " + _Person.ID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.ID.ToString();
            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            cbCountry.SelectedIndex = _Person.NationalityCountryID - 1;

            if (_Person.Gender == "Male")
                rbMale.Select();
            else
                rbFemale.Select();

            if (_Person.ImagePath != "")
            {
                try
                {
                    pbPersonImage.Load(_Person.ImagePath);

                }
                catch (Exception ex) { }
                {
                    //MessageBox.Show("Profile Images Can not be Found ");
                }
            }
            llRemoveImage.Visible = (_Person.ImagePath != "");


            ///// create backup form current user
            _Backup_Person.CopyPersonInfo(_Person);

        }

        private void _SetDefualtValues()
        {
            //this will initialize the reset the defaule values


            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            //set default image for the person.
            if (rbMale.Checked)
                pbPersonImage.Image = Resources.icons8_person_941;
            else
                pbPersonImage.Image = Resources.icons8_businesswoman_941;

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to Egypt.
            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";


        }

        private void AddUpdatePersonInfo_Load(object sender, EventArgs e)
        {
            _SetDefualtValues();

            if (_Person != null)
            {
                _LoadAllData();
            }

        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtilities.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void Save_Btn_Click(object sender, EventArgs e)
        {
            if (!_HandlePersonImage())
            {
                return;
            }

            _fillPersonData();

            ///// check if there is no changes
            if (clsPerson.IsPersonsInfoDuplicated(_Person, _Backup_Person))
            {
                MessageBox.Show("There is no change of inforamtion to Save!!", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //// check if NationalNo is unique or not
            if (_Person.NationalNo != _Backup_Person.NationalNo)
            {
                if (!clsPerson.IsNationalNoUnique(_Person.NationalNo))
                {
                    MessageBox.Show("The national number already exists. Please try another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are incomplete or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.ID.ToString();

                lblTitle.Text = "Edit " + _Person.FirstName + "'s info";

                if (_Mode == enMode.AddNew)
                    MessageBox.Show("Person Added Successfully with id = [ " + _Person.ID + " ]", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Person with id = [ " + _Person.ID + " ] Updated Successfully", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //// update bakcup person Info
                _Backup_Person.CopyPersonInfo(_Person);

                ///// Invoke Method if it's exist
                Databack?.Invoke(this, _Person.ID);

            }
            else
                MessageBox.Show("Failed to add this person.", "Failed to add", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;

                pbPersonImage.Load(selectedFilePath);

                llRemoveImage.Visible = true;

            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            llRemoveImage.Visible = false;

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {

                pbPersonImage.Image = Resources.icons8_person_94;
            }

            _Person.Gender = "Male";
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.icons8_businesswoman_94;

            _Person.Gender = "Female";
        }

        private void ValidateEmptyString(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
                return;
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            ValidateEmptyString(sender, e);
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }


            //// check if NationalNo is unique or not
            //if (txtNationalNo.Text.Trim() != _Backup_Person.NationalNo //// check if the national number has been changed or not 
            //    && !clsPerson.IsNationalNoUnique(txtNationalNo.Text.Trim())) ///// N1000
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
            //}
            //else
            //    errorProvider1.SetError(txtNationalNo, null);


            if (txtNationalNo.Text.Trim() != _Backup_Person.NationalNo)  // check if the national number has been changed or not 
            {
                if (!clsPerson.IsNationalNoUnique(txtNationalNo.Text.Trim())) // check if NationalNo is unique or not
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
                }
            }
            else
                errorProvider1.SetError(txtNationalNo, null);



        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            if (txtEmail.Text.Trim().Length == 0)
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };

        }

    }
}
