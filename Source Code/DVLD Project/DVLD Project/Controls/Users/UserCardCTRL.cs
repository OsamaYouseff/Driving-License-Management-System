using UsersBusinessLayer;
using System.Windows.Forms;

namespace DVLD_Project.Controls.Users
{
    public partial class UserCardCTRL : UserControl
    {

        private clsUser _User;
        private int _UserID = -1;


        public UserCardCTRL()
        {
            InitializeComponent();
        }
        public UserCardCTRL(int PersonID)
        {
            InitializeComponent();

            _UserID = PersonID;

            LoadInfo(PersonID);

        }


        public void LoadInfo(int UserID)
        {

            _User = clsUser.GetUserWithID(UserID);


            if (_User == null)
            {
                MessageBox.Show("There is no person with the provided PersonID = " + _UserID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            personCard_CTRL1.LoadPersonInfo(_User.PersonID);

            lblUserName.Text = _User.UserName;
            lblUserID.Text = _User.ID.ToString();
            lblIsActive.Text = _User.IsActive ? "True" : "False";
        }
    }
}
