using System;
using System.Windows.Forms;

namespace DVLD_Project.Forms.People
{
    public partial class FindUserForm : Form
    {
        public delegate void DataBack(object sender, int PersonID);
        public event DataBack Databack;


        public FindUserForm()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Databack?.Invoke(this, personCardPlusFiliter_CTRL1.PersonID);

            this.Close();
        }
    }
}
