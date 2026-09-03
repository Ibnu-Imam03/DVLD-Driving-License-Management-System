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

namespace DVLD.People.controls
{
    public partial class clsPersonCardWithFilter : UserControl
    {

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(personID);
            }
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { 
                return _ShowAddPerson; 
                }
            set { 
                _ShowAddPerson = value;
                btnAddNew.Visible = _ShowAddPerson;
                }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get {  return _FilterEnabled; }

            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        public clsPersonCardWithFilter()
        {
            InitializeComponent();
        }
        private int _PersonID = -1;

        public int PersonID
        {
            get { return clsPersonCard1.PersonID; }
        }
        public clsPeople SelectedPersonInfo
        {
            get { return clsPersonCard1.SelectedPersonInfo;}
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();

        }

        private void _FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    {
                        if (int.TryParse(txtFilter.Text, out int personID))
                        {
                            clsPersonCard1.LoadPersonInfo(personID);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Please enter a valid Person ID.",
                                "Invalid Person ID",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            return;
                        }
                        break;
                    }
                case "National No":
                    {
                        clsPersonCard1.LoadPersonInfo(txtFilter.Text);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
                    if(OnPersonSelected != null && FilterEnabled)
                    {
                        OnPersonSelected(clsPersonCard1.PersonID);
                    }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void clsPersonCardWithFilter_Load(object sender, EventArgs e)
        { 
            cbFilter.Items.Add("Person ID");
            cbFilter.Items.Add("National No");          
            cbFilter.SelectedIndex = 0;
            txtFilter.Focus();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();
            clsPersonCard1.LoadPersonInfo(PersonID);
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the highlighted errors before continuing.","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }
                _FindNow();
        }

        private void txtFilterValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "Please enter a value.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilter, "");
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
            if (cbFilter.Text == "Person ID ")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

        }
    }
}
