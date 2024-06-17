using System;
using DVLD_Project.Forms;
using UsersBusinessLayer;
using System.Windows.Forms;
using DVLD_Project.Forms.Users;
using DVLD_Project.Forms.Login;
using DVLD_Project.Forms.Drivers;
using DVLD_Project.Global_Classes;
using DVLD_Project.Forms.Test.Test_Types;
using DVLD_Project.Forms.ApplicationTypes;
using DVLD_Project.Forms.Licences.Detain_License;
using DVLD_Project.Forms.Applications.Renew_Local_License;
using DVLD_Project.Forms.Applications.Release_Retained_License;
using DVLD_Project.Forms.Licences.International_Driving_Licence;
using DVLD_Project.Forms.Applications.ReplaceLostedOrDamagedLicense;
using DVLD_Project.Forms.Applications.Driving_License.Local_Driving_Licence;

namespace DVLD_Project
{
    public partial class MainScreenForm : Form
    {

        private clsUser _LoggedUser;
        private LoginForm _loginForm;


        public MainScreenForm()
        {
            InitializeComponent();
        }
        public MainScreenForm(LoginForm loginForm)
        {
            InitializeComponent();

            _LoggedUser = clsGlobal.CurrentUser;

            _loginForm = loginForm;

        }

        private void PeopleMenu_Click(object sender, EventArgs e)
        {
            PeopleListScreen peopleScreenForm = new PeopleListScreen();
            peopleScreenForm.ShowDialog();

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsUser CurrentUser = clsGlobal.CurrentUser;
            clsGlobal.CurrentUser = null;

            ///// Update LoginForm Login Data if it's Changed
            _loginForm.RefreshLoginData(CurrentUser.UserName, CurrentUser.Password);
            _loginForm.Show();


            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfoForm userInfoForm = new UserInfoForm(_LoggedUser.ID);
            userInfoForm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(_LoggedUser.ID);

            changePasswordForm.ShowDialog();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersListScreenForm usersListScreenForm = new UsersListScreenForm();
            usersListScreenForm.ShowDialog();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestTypesListForm testTypesListForm = new TestTypesListForm();
            testTypesListForm.ShowDialog();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationTypesListForm applicationTypesForm = new ApplicationTypesListForm();
            applicationTypesForm.ShowDialog();
        }

        private void MainScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _loginForm.Visible = true;
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateLocalDrivingLicenceForm addUpdateLocalDrivingLicenceForm = new AddUpdateLocalDrivingLicenceForm();

            addUpdateLocalDrivingLicenceForm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DriversListForm driversListForm = new DriversListForm();
            driversListForm.ShowDialog();
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenceListForm localDrivingLicenceListForm = new LocalDrivingLicenceListForm();

            localDrivingLicenceListForm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenceListForm localDrivingLicenceListForm = new LocalDrivingLicenceListForm();

            localDrivingLicenceListForm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLocalDrivingLicenseForm renewLocalDrivingLicenseForm = new RenewLocalDrivingLicenseForm();
            renewLocalDrivingLicenseForm.ShowDialog();
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceLostOrDamagedLicenseForm replaceLostOrDamagedLicenseForm = new ReplaceLostOrDamagedLicenseForm();

            replaceLostOrDamagedLicenseForm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddInternationalDrivingLicenceForm addInternationalDrivingLicenceForm = new AddInternationalDrivingLicenceForm();
            addInternationalDrivingLicenceForm.ShowDialog();
        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InternationalDrivingLicenceListForm internationalDrivingLicenceListForm = new InternationalDrivingLicenceListForm();
            internationalDrivingLicenceListForm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseRetainedLicenseForm retainedLicenseForm = new ReleaseRetainedLicenseForm();
            retainedLicenseForm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseRetainedLicenseForm retainedLicenseForm = new ReleaseRetainedLicenseForm();
            retainedLicenseForm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicenseApplicationForm detainLicenseApplicationForm = new DetainLicenseApplicationForm();
            detainLicenseApplicationForm.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DetainedLicensesListForm detainedLicensesListForm = new DetainedLicensesListForm();
            detainedLicensesListForm.ShowDialog();
        }
    }
}
