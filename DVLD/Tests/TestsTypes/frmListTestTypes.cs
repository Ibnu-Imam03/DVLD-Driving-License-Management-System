using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.TestsTypes
{
    public partial class frmListTestTypes : Form
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.DataSource = clsTestsType.GetAllTestType();
            dgvTestTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestTypes.MultiSelect = false;
            dgvTestTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            dgvTestTypes.Columns[0].FillWeight = 10;
            dgvTestTypes.Columns[1].FillWeight = 15;
            dgvTestTypes.Columns[2].FillWeight = 30;
            dgvTestTypes.Columns[3].FillWeight = 10;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = Convert.ToInt32(dgvTestTypes.SelectedRows[0].Cells["TestTypeID"].Value);
            frmEditTestType frm = new frmEditTestType(TestTypeID);
            frm.ShowDialog();
            dgvTestTypes.DataSource = clsTestsType.GetAllTestType();
        }
    }
}
