using System;
using System.Windows.Forms;

namespace DVLD_Project.Forms.Applications.Driving_License.Local_Driving_Licence
{
    public partial class ShowLocalDrivingLicenseApplicationInfoForm : Form
    {
        private int _LicenceID = -1;


        public ShowLocalDrivingLicenseApplicationInfoForm()
        {
            InitializeComponent();
        }
        public ShowLocalDrivingLicenseApplicationInfoForm(int LicenceID, int PassedTestsNum)
        {
            InitializeComponent();

            _LicenceID = LicenceID;

            _LoadData(PassedTestsNum);
        }


        private void _LoadData(int PassedTestsNum)
        {
            try
            {
                drivingLicenceinfo_CTRL.LoadData(_LicenceID, PassedTestsNum);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
