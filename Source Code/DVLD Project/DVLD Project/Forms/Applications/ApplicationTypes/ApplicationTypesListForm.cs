using System;
using System.Data;
using System.Windows.Forms;
using ApplicationBusinessLayer;

namespace DVLD_Project.Forms.ApplicationTypes
{
    public partial class ApplicationTypesListForm : Form
    {

        DataTable _allApplicationsTypes;

        public ApplicationTypesListForm()
        {
            InitializeComponent();
        }

        private void _refreshData()
        {
            _allApplicationsTypes = clsApplicationTypes.GetAllApplicationsTypes();
            dgvApplicationTypes.DataSource = _allApplicationsTypes;

            lblRecordsCount.Text = _allApplicationsTypes.Rows.Count.ToString();

            if (_allApplicationsTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 170;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 480;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 150;
            }

        }

        private void ApplicationTypesForm_Load(object sender, EventArgs e)
        {
            _refreshData();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationTypesForm editApplicationTypesForm = new EditApplicationTypesForm((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            editApplicationTypesForm.ShowDialog();


            _refreshData();
        }
    }
}
