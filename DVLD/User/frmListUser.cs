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

namespace DVLD.User
{
    public partial class frmListUser : Form
    {
        public frmListUser()
        {
            InitializeComponent();
        }

        private void frmListUser_Load(object sender, EventArgs e)
        {
            dgvUserList.DataSource = clsUser.GetAllUsers();
            cbUserFilter.Items.Add("None");
            cbUserFilter.Items.Add("UserID");
            cbUserFilter.Items.Add("UserName");
            cbUserFilter.Items.Add("PersonID");
            cbUserFilter.Items.Add("FullName");
            cbUserFilter.Items.Add("IsActive");
            cbUserFilter.SelectedIndex = 0;
            cbIsActiveFilter.Items.Add("All");
            cbIsActiveFilter.Items.Add("No");
            cbIsActiveFilter.Items.Add("Yes");
            txtFilter.Visible = false;
            //cbIsActiveFilter.Visible = false;
            cbIsActiveFilter.SelectedIndex = 0;

            dgvUserList.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            lblTotalUser.Text = clsUser.GetAllUsers().Rows.Count.ToString();

          

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count > 0)
            {
                int PersonID = Convert.ToInt32(dgvUserList.SelectedRows[0].Cells["UserID"].Value);

                frmUserInfo frm = new frmUserInfo(PersonID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("88888888888888888");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewUSerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
