using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People;
using DVLD_BusinessLayer;
namespace DVLD
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            dgvManage_People.DataSource = clsPeople.GetAllPeoeple();

            cbFilter.Items.Add("None");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("NationalNo");
            cbFilter.Items.Add("FirstName");
            cbFilter.Items.Add("SecondName");
            cbFilter.Items.Add("ThirdName");
            cbFilter.Items.Add("LastName");
            cbFilter.Items.Add("CountryName");
            cbFilter.Items.Add("Gendor");
            cbFilter.Items.Add("Phone");
            cbFilter.Items.Add("Email");

            cbFilter.SelectedIndex = 0;
            lblTotal.Text = clsPeople.GetAllPeoeple().Rows.Count.ToString();


        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if (cbFilter.SelectedItem.ToString() == "None" || txtFilter.Text == "")
            {
                dgvManage_People.DataSource = clsPeople.GetAllPeoeple();

            }
            else if (cbFilter.SelectedItem.ToString() == "PersonID")
            {
                dgvManage_People.DataSource = clsPeople.GetPerson(cbFilter.SelectedItem.ToString(), int.Parse(txtFilter.Text));

            }
            else
            {
                dgvManage_People.DataSource = clsPeople.GetPerson(cbFilter.SelectedItem.ToString(), txtFilter.Text);

            }

        }


        private void PersonAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += GetFromPreviousAddEditForm;
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += GetFromPreviousAddEditForm;
            frm.ShowDialog();
        }
        public void GetFromPreviousAddEditForm(object sender, int PersonID)
        {
            dgvManage_People.DataSource = clsPeople.GetAllPeoeple();

        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvManage_People.SelectedRows.Count > 0)
            {
                int PersonID = Convert.ToInt32(dgvManage_People.SelectedRows[0].Cells["PersonID"].Value);
                frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
                frm.ShowDialog();
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvManage_People.SelectedRows.Count > 0)
            {
                int personID = Convert.ToInt32(dgvManage_People.SelectedRows[0].Cells["PersonID"].Value);
                frmAddEditPerson frm = new frmAddEditPerson(personID);
                frm.DataBack += GetFromPreviousAddEditForm;
                frm.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvManage_People.SelectedRows.Count > 0)
            {
                int PersonID = Convert.ToInt32(
                    dgvManage_People.SelectedRows[0].Cells["PersonID"].Value);

                if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (clsPeople.DeletePerson(PersonID))
                    {
                        MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh DataGridView
                        dgvManage_People.DataSource = clsPeople.GetAllPeoeple();
                    }
                    else
                    {
                        MessageBox.Show("Person was not deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a person first.");
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;

                dgvManage_People.DataSource = clsPeople.GetAllPeoeple();
            }
            else
            {
                txtFilter.Visible = true;
            }
        }
    }
}
    
