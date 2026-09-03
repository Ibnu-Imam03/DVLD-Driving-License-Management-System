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
    public partial class frmChangePassword : Form
    {
        private clsUser _User ;
        private int _UserID = -1;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }
        private void CurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtCurrentPassword.Text == "" || txtCurrentPassword.Text !=_User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Enter an Exsiting Password!!");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
            usUserInfo1.LoadUserInfo(_UserID);
        }

        private void NewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text == "" )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Enter New Password!!");
            }
                     
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("The Field(s) is/are Required !!");
                return;
            }
           
            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                  "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Enter Same Password!!");
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }
    }
}
