
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD.Tests
{


    public partial class frmEditTestType: Form
    {

        private int _TestTypeID;
        private clsTestsType TestType;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();
            TestType = clsTestsType.FindTestTypeByID(TestTypeID);
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            if (TestType == null)
            {
                MessageBox.Show("Test Type not found.");
               
                return;
            }
            lblTestTypeID.Text = TestType.TestTypeID.ToString();
            txtTitle.Text = TestType.TestTitle;
            txtDescription.Text = TestType.TestDescribtion;
            txtFees.Text = TestType.fee.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("The Field required!");
                return;
            }
            TestType.TestTitle = txtTitle.Text;
            TestType.TestDescribtion = txtDescription.Text;
            TestType.fee = decimal.Parse(txtFees.Text);
            if (TestType.Save())
            {
                MessageBox.Show("It Save Succesfully");
            }
            else
            {
                MessageBox.Show("It Not Save Succesfully");
            }

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "The Field Is Requred");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "The Field Is Requred");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDescription, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidations.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "The Field Must Be Valid Number");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            }
            ;

        }
    }
}
