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

            dgvUserList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUserList.MultiSelect = false;

            dgvUserList.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;

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
            cbIsActiveFilter.SelectedIndex = 0;
            cbUserFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIsActiveFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            txtFilter.Visible = false;

            lblTotalUser.Text = dgvUserList.Rows.Count.ToString();
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
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUserList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a user.");
                    return;
                }

                int personID = Convert.ToInt32(
                    dgvUserList.SelectedRows[0].Cells["PersonID"].Value
                );

                frmAddEditUser frm = new frmAddEditUser(personID);

                frm.ShowDialog();

                // Refresh the DataGridView after editing
                dgvUserList.DataSource = clsUser.GetAllUsers();

                // Update total users
                lblTotalUser.Text = dgvUserList.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbUserFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserFilter.SelectedItem.ToString() =="None"  )
            {
                cbIsActiveFilter.Visible = false;
                txtFilter.Visible = false;
                dgvUserList.DataSource = clsUser.GetAllUsers();

            }
            else if (cbUserFilter.SelectedItem.ToString() == "IsActive")
            {
                cbIsActiveFilter.Visible = true;
                txtFilter.Visible = false;
            }
            else
            {
                cbIsActiveFilter.Visible = false;
                txtFilter.Visible=true;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "" || cbUserFilter.SelectedIndex==0)
            {
                dgvUserList.DataSource=clsUser.GetAllUsers();
            }
            else
            {
                dgvUserList.DataSource = clsUser.GetUsers(cbUserFilter.SelectedItem.ToString(), txtFilter.Text);
            }
        }
        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUserList.DataSource = clsUser.GetUsers(cbUserFilter.SelectedItem.ToString(),cbIsActiveFilter.SelectedItem.ToString());
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            int personID = Convert.ToInt32(
                dgvUserList.SelectedRows[0].Cells["PersonID"].Value
            );
            frmChangePassword frm = new frmChangePassword(personID);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            int UserID = Convert.ToInt32(dgvUserList.SelectedRows[0].Cells["UserID"].Value);
            if (clsUser.DeleteUSer(UserID))
            {
                MessageBox.Show("User deleted successfully.","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);

                dgvUserList.DataSource = clsUser.GetAllUsers();
                lblTotalUser.Text = dgvUserList.Rows.Count.ToString();
            }
            else
            {
                MessageBox.Show("User was not deleted.","Delete Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
