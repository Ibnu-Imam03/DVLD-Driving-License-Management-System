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
    public partial class frmAddEditUser : Form
    {
        private int _PersonID = -1;
        enum enMode {AddNew=0,Update=1 };
        enMode _Mode;
        private clsUser _User;
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditUser(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        }

        private void _AddNewUser()
        {
            _User = new clsUser();
            _User.UserName = txtUserName.Text;
            _User.Password = txtConfirmPassword.Text;
            _User.PersonID = _PersonID;
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                MessageBox.Show("User added successfully.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add user.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                clsPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                lblTitle.Text = "Edit User Info";
                clsPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                clsPersonCardWithFilter1.FilterEnabled=true;
                lblTitle.Text = "Add New User ";
               

            }
        }

        private void tpLoginInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _AddNewUser();
            lblUserID.Text = _User.UserID.ToString();
        }
    }
}
