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
    public partial class frmEditApplicationsTypes : Form
    {
        private int _ApplicationID;
        private clsApplicationTypes App;
        public frmEditApplicationsTypes(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationsTypes_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _ApplicationID.ToString();
            App = clsApplicationTypes.GetApplictionTypesByID(_ApplicationID);
            if (App == null)
            {
                MessageBox.Show("Select Appliction Type Firts", "Error");
                return;
            }
            txtFees.Text = App.Fee.ToString();
            txtTitle.Text = App.Title.ToString();

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (txtTitle.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This Field Required!");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, "");
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
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


            if (!clsValidations.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
            ;
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            App.Title = txtTitle.Text;
            if (decimal.TryParse(txtFees.Text, out decimal fee))
            {
                App.Fee = fee;
            }
            else
            {
                MessageBox.Show("Please enter a valid fee.");
            }
            if (App.Save())
            {
                MessageBox.Show("Data Save Succesfully", "Saved", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error: Data IS Not Saved", "Error", MessageBoxButtons.OK);
            }

        }
    }
}
