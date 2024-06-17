using System;
using System.Data;
using System.Windows.Forms;
using TestTypesBusinessLayer;
using DVLD_Project.Forms.Tests.Test_Types;

namespace DVLD_Project.Forms.Test.Test_Types
{
    public partial class TestTypesListForm : Form
    {

        DataTable _allTestsTypes;

        public TestTypesListForm()
        {
            InitializeComponent();
        }

        private void _refreshData()
        {
            _allTestsTypes = clsTestType.GetAllTestsTypes();
            dgvTestTypes.DataSource = _allTestsTypes;

            lblRecordsCount.Text = _allTestsTypes.Rows.Count.ToString();

            if (_allTestsTypes.Rows.Count > 0)
            {

                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 70;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 190;

                dgvTestTypes.Columns[2].HeaderText = "Descripation";
                dgvTestTypes.Columns[2].Width = 800;


                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;

            }
        }

        private void TestTypesListForm_Load(object sender, EventArgs e)
        {
            _refreshData();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestTypeForm editTestTypeForm = new EditTestTypeForm((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            editTestTypeForm.ShowDialog();

            _refreshData();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewRow CurrentRow = dgvTestTypes.CurrentRow;

            string Title = CurrentRow.Cells[1].Value.ToString();
            string Description = CurrentRow.Cells[2].Value.ToString();
            float Fees = Convert.ToSingle(CurrentRow.Cells[3].Value);


            ShowTestTypeInfoForm showTestTypeInfoForm = new ShowTestTypeInfoForm(Title, Description, Fees);
            showTestTypeInfoForm.ShowDialog();
        }

        private void CloseBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
