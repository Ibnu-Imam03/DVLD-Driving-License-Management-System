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

namespace DVLD.Applications.AplicationsTypes
{
    public partial class frmListApplicationsTypes : Form
    {
        public frmListApplicationsTypes()
        {
            InitializeComponent();
        }
        private void frmListApplicationsTypes_Load(object sender, EventArgs e)
        {
            dgvApplictionsTypes.DataSource = clsApplicationTypes.GetAllapplicationTypes();
            dgvApplictionsTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvApplictionsTypes.MultiSelect = false;
            dgvApplictionsTypes.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;
            lblRecordsCount.Text = dgvApplictionsTypes.Rows.Count.ToString();

            dgvApplictionsTypes.Columns["ID"].FillWeight = 20;
            dgvApplictionsTypes.Columns["Title"].FillWeight = 50;
            dgvApplictionsTypes.Columns["Fees"].FillWeight = 30;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationsTypes frm = new frmEditApplicationsTypes(Convert.ToInt32(dgvApplictionsTypes.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
            dgvApplictionsTypes.DataSource = clsApplicationTypes.GetAllapplicationTypes();

        }
    }
}
