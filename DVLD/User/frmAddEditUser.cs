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
    }
}
