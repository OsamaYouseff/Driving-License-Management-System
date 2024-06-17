using System;
using DVLD_Project.Forms;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Project.Forms.Login;
using DVLD_Project.Forms.Users;
using DVLD_Project.Forms.Drivers;
using DVLD_Project.Forms.Licences;
using DVLD_Project.Forms.Test.Test_Types;
using DVLD_Project.Forms.ApplicationTypes;
using DVLD_Project.Forms.Tests.Test_Types;
using DVLD_Project.Forms.Licences.Detain_License;
using DVLD_Project.Forms.Licences.Local_Driving_Licence;
using DVLD_Project.Forms.Applications.Renew_Local_License;
using DVLD_Project.Forms.Applications.Release_Retained_License;
using DVLD_Project.Forms.Licences.International_Driving_Licence;
using DVLD_Project.Forms.Applications.Driving_License.Local_Driving_Licence;

namespace DVLD_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginForm());


            //Application.Run(new MainScreenForm());
            //Application.Run(new AddUpdateUserForm());
            //Application.Run(new ChangePasswordForm());
            //Application.Run(new ApplicationTypesForm());
            //Application.Run(new ShowLocalLicenseInfoForm());
            //Application.Run(new ShowPersonLicenceHistoryForm());
            //Application.Run(new ShowLicenseInfoForm());
            //Application.Run(new AddUpdateLocalDrivingLicenceForm());
            //Application.Run(new ScheduleTestForm());
            //Application.Run(new DetainLicenseApplicationForm());
            //Application.Run(new DriversListForm());
            //Application.Run(new DetainedLicensesListForm());
            //Application.Run(new InternationalDrivingLicenceListForm());
            //Application.Run(new PeopleListScreen());
            //Application.Run(new UsersListScreenForm());
            //Application.Run(new TestTypesListForm());
            //Application.Run(new LocalDrivingLicenceListForm());
        }
    }
}
