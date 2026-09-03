using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        public delegate void DataBackEventHndler(object sender, int PersonID);
        public event DataBackEventHndler DataBack;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _PersonID = -1;
        clsPeople _Person;
        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        }
        private void _FillCountriesInComboBox()
        {
            cbCountries.DataSource = clsCountries.GetAllCountries();
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";

        }
        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblAddNew.Text = "Add New Person";
                _Person = new clsPeople();
            }
            else
            {
                lblAddNew.Text = "Update Person";
            }
            if (rbMale.Checked)
            {
                pictureBox1.Image = Resources.male;
            }
            else
            {
                pictureBox1.Image = Resources.female_worker;
            }

            llRemoveImage.Visible = (pictureBox1.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountries.SelectedIndex = cbCountries.FindString("Ethiopia");

            txtFirstName.Text = "";
            txtSecoundName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalID.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

        }
        private void _LoadData()
        {
            _Person = clsPeople.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"Now PersonWith This ID = {_PersonID}", "Person Not Found !", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecoundName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtNationalID.Text = _Person.NationalNo;
            txtNationalID.ReadOnly = true;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            // Display country
            cbCountries.SelectedValue = _Person.NationalityCountryID;
            // Display gender
            if (_Person.Gendor == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
            // Display image
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pictureBox1.ImageLocation = _Person.ImagePath;
            }
            llRemoveImage.Visible = (_Person.ImagePath != "");
        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Field are not Valid !");
                return;
            }

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecoundName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalID.Text;
            _Person.Gendor = rbMale.Checked ? 0 : 1;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = Convert.ToInt32(cbCountries.SelectedValue);
            _Person.Address = txtAddress.Text;
            _Person.ImagePath = pictureBox1.ImageLocation == "" ? "" : pictureBox1.ImageLocation;

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                lblAddNew.Text = "Update Person";
                MessageBox.Show("Data Save Succesfully", "Saved", MessageBoxButtons.OK);

                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data IS Not Saved", "Error", MessageBoxButtons.OK);
            }

        }
        private void ValidatingEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Field is Required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, "");
            }


        }
        private void TxtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == "")
            {
                return;
            }

            if (!clsValidations.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }
        private void NationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalID, "This Field Is Required!");
            }
            else
            {
                errorProvider1.SetError(txtNationalID, "");
            }

            if (txtNationalID.Text.Trim() != _Person.NationalNo && clsPeople.IsNationalIDExist(txtNationalID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalID, "National Number Is   Used For Another Person");
            }
            else
            {
                errorProvider1.SetError(txtNationalID, "");
            }
        }
        private void llSaveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ImagesFolder = @"C:\DVLD Images";

                if (!Directory.Exists(ImagesFolder))
                {
                    Directory.CreateDirectory(ImagesFolder);
                }

                // Delete old image
                if (_Mode == enMode.Update &&
                    !string.IsNullOrEmpty(_Person.ImagePath) &&
                    File.Exists(_Person.ImagePath))
                {
                    File.Delete(_Person.ImagePath);
                }
                // Create new image path
                string FileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(openFileDialog.FileName);
                // Copy new image
                File.Copy(openFileDialog.FileName, Path.Combine(ImagesFolder, FileName));
                // Display new image
                pictureBox1.ImageLocation = Path.Combine(ImagesFolder, FileName);
                // Show remove option
                llRemoveImage.Visible = true;
            }
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            

            if (rbMale.Checked)
            {
                pictureBox1.Image = Resources.male;
            }
            else
            {
                pictureBox1.Image = Resources.female_worker;
            }
            pictureBox1.ImageLocation = "";
            llRemoveImage.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked && string.IsNullOrEmpty(pictureBox1.ImageLocation))
            {
                pictureBox1.Image = Resources.male;
            }
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked && string.IsNullOrEmpty(pictureBox1.ImageLocation))
            {
                pictureBox1.Image = Resources.female_worker;
            }
        }
    }
}
