using System;
using System.Data;
using DVLD.Classes;
using PeopleBusinessLayer;
using System.Windows.Forms;
using ApplicationBusinessLayer;
using LicenseClassBusinessLayer;
using DVLD_Project.Global_Classes;
using LocalDrivingLicenseApplicationBusinessLayer;

namespace DVLD_Project.Forms.Applications.Driving_License.Local_Driving_Licence
{
    public partial class AddUpdateLocalDrivingLicenceForm : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        private clsApplicationTypes _ApplicationTypes = clsApplicationTypes.GetApplicationType("New Local Driving License Service");
        private DataTable _LicenseClassData;
        private int _LocalDrivingApplicationID = -1, _CurrentChoisedLicenseClass = -1, _OldChoisedLicenseClass = -1;
        private clsPerson _PersonInfo = new clsPerson();



        public AddUpdateLocalDrivingLicenceForm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

            try
            {
                _SetDefualtValues();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SaveBtn.Enabled = false;
        }

        public AddUpdateLocalDrivingLicenceForm(int LocalDrivingApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingApplicationID = LocalDrivingApplicationID;

            _SetDefualtValues();
            _LoadData();
            SaveBtn.Enabled = false;

        }

        private void _FillclsLicenseClassData()
        {
            _LicenseClassData = clsLicenseClass.GetAllLicenseClasses();

            if (_LicenseClassData != null)
            {
                foreach (DataRow LicenceRow in _LicenseClassData.Rows)
                {
                    cbLicenseClass.Items.Add(LicenceRow[1].ToString());
                }
            }
            else
                MessageBox.Show("Can't Get License Classes Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void _SetDefualtValues()
        {

            if (_Mode == enMode.AddNew)
            {
                LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

                tpApplicationInfo.Enabled = false;
                personCardPlusFiliter_CTRL.FilterFocus();


                lblLocalDrivingLicenseAppID.Text = "[???]";
                lblApplicationDate.Text = clsFormat.DateToShort2(DateTime.Now);
                lblFees.Text = _ApplicationTypes.ApplicationFees.ToString();

                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

                _FillclsLicenseClassData();

                cbLicenseClass.SelectedIndex = 0;

            }
            else
            {
                tcApplicationInfo.Enabled = true;
                SaveBtn.Enabled = true;

            }

            ///// change title 
            lblTitle.Text = this.Text = ((_Mode == enMode.AddNew) ? "Add New" : "Update") + " Local Driving Licence";

        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            _PersonInfo = personCardPlusFiliter_CTRL.SelectedPersonInfo;

            if (_Mode == enMode.Update)
            {
                SaveBtn.Enabled = false;
                tpApplicationInfo.Enabled = true;
                tpPersonalInfo.Enabled = false;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                _OldChoisedLicenseClass = cbLicenseClass.SelectedIndex;

                return;
            }

            //in case of add new mode.
            if (personCardPlusFiliter_CTRL.PersonID != -1)
            {
                SaveBtn.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tpPersonalInfo.Enabled = false;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please select a person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                personCardPlusFiliter_CTRL.FilterFocus();
            }
        }

        private void _LoadData()
        {

            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(_LocalDrivingApplicationID);

            personCardPlusFiliter_CTRL.FilterEnabled = false;
            SaveBtn.Enabled = false;

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Local Driving License Application with ID = " + _LocalDrivingApplicationID, "Local Driving License Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the lblLocalDrivingLicenseAppID was not found
            lblLocalDrivingLicenseAppID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblFees.Text = LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
            _FillclsLicenseClassData();

            cbLicenseClass.SelectedIndex = LocalDrivingLicenseApplication.LicenseClassID - 1;
            _CurrentChoisedLicenseClass = LocalDrivingLicenseApplication.LicenseClassID - 1;

            try
            {
                personCardPlusFiliter_CTRL.LoadInfo(LocalDrivingLicenseApplication.ApplicantPersonID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : Can't Show Person who owns this Local Driving Application", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            personCardPlusFiliter_CTRL.FilterEnabled = false;


        }

        private void AddUpdateUserInfo_Load(object sender, EventArgs e)
        {
            _SetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            _CurrentChoisedLicenseClass = cbLicenseClass.SelectedIndex;

            SaveBtn.Enabled = _CurrentChoisedLicenseClass != _OldChoisedLicenseClass;

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            SaveBtn.Enabled = false;
            tpApplicationInfo.Enabled = false;
            tpPersonalInfo.Enabled = true;

            tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpPersonalInfo"];
            return;
        }

        private void _FillLocalAppData()
        {
            try
            {
                LocalDrivingLicenseApplication.ApplicantPersonID = _PersonInfo.ID;
                LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
                LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
                LocalDrivingLicenseApplication.ApplicationTypeID = (cbLicenseClass.SelectedIndex + 1);
                LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
                LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
                LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.ID;
                LocalDrivingLicenseApplication.LicenseClassID = cbLicenseClass.SelectedIndex + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message);
                return;
            }

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ///// Ckeck if user Updated the Info to Save
            if (_Mode == enMode.Update && _OldChoisedLicenseClass == _CurrentChoisedLicenseClass)
            {
                MessageBox.Show("No change of data to save", "No change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //// Does Person Already Have A Local Application Active Or Completed
            int FoundedApplicationID = -1;
            if (clsLocalDrivingLicenseApplication.DoesPersonAlreadyHaveALocalApplicationActiveOrCompleted(_PersonInfo.ID, (cbLicenseClass.SelectedIndex + 1), ref FoundedApplicationID))
            {
                MessageBox.Show("This person already holds an active or completed license for this license class with the provided ID = " + FoundedApplicationID, " try to another license class.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ///// Is Person's Age is Below Allowed Age for Choiced License Class
            if (_PersonInfo != null && _CurrentChoisedLicenseClass != -1)
            {

                if (clsLocalDrivingLicenseApplication.IsPersonAgeBelowAllowedAge(_PersonInfo.ID, _CurrentChoisedLicenseClass + 1))
                {
                    MessageBox.Show("The age of this person is below the allowed age for the chosen license class.", "Lower Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("There seems to be something wrong!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _FillLocalAppData();

            if (LocalDrivingLicenseApplication.SaveLocalDrivingLicense())
            {
                lblLocalDrivingLicenseAppID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblTitle.Text = "Update Local Driving License Application";

                if (_Mode == enMode.AddNew)
                    MessageBox.Show("Local Driving License Application Added Successfully with ID = [ " + LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID + " ]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Local Driving License Application with ID = [ " + LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID + " ] Updated Successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _CurrentChoisedLicenseClass = cbLicenseClass.SelectedIndex;

                _Mode = enMode.Update;

            }
            else
                MessageBox.Show("Failed to add this local driving license.", "Failed to add", MessageBoxButtons.OK, MessageBoxIcon.Error);



            //Update UI
            SaveBtn.Enabled = false;
            _OldChoisedLicenseClass = cbLicenseClass.SelectedIndex;

        }
    }
}
