using System;
using System.Windows.Forms;

namespace DVLD_Project.Forms.Users
{
    public partial class UserInfoForm : Form
    {
        private int _UserID = -1;

        public UserInfoForm()
        {
            InitializeComponent();
        }
        public UserInfoForm(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

        }


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserInfoForm_Load(object sender, EventArgs e)
        {
            UserInfoCardCTRL.LoadInfo(_UserID);

        }
    }
}
