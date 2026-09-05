using DVLD.People.controls;
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
        private int _UserID = -1;
        enum enMode {AddNew=0,Update=1 };
        private enMode _Mode;
        private clsUser _User;
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
        }

        private void _ResetDeafualtValue()
        {
            if (_Mode == enMode.AddNew)
            {
                _User = new clsUser();
                lblTitle.Text = "Add New User";
                this.Text = lblTitle.Text;
                tpLoginInfo.Enabled = false;

            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }
        private void _LoadData()
        {
            _User = clsUser.FindByUserID( _UserID );
            clsPersonCardWithFilter1.FilterEnabled = false;

            if ( _User == null )
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text =_User.UserName;
            txtPassword.Text=_User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            clsPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDeafualtValue();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                   "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a person first.");
                return;
            }

            _User.PersonID = clsPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;
          

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void UserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                }
            }
        }
        private void Password_Valditaing(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
            ;
        }
        private void ConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
            ;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }
            if (clsPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExitedWithPersonID(clsPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }
            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsPersonCardWithFilter1.FilterFocus();

            }
        }

        private void frmAddEditUser_Activated(object sender, EventArgs e)
        {
            clsPersonCardWithFilter1.FilterFocus();
        }
    }
}
