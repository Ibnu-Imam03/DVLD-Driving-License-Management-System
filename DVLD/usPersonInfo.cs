using DVLD_BusinessLayer;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace DVLD
{
    public partial class usPersonInfo : UserControl
    {
        public usPersonInfo()
        {
            InitializeComponent();
            
        }
        private string _ImagePath = "";
        private clsPeople _Person = new clsPeople();
        public enum enMode { Addnew=0 , Update=1};
        public enMode Mode= enMode.Addnew;
        public delegate void DataEventHandeler(object sender, DataTable People);
        public event DataEventHandeler DataBack;
        public clsPeople LoadPersonData()
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the validation errors.");
                return null;
            }
            else
            {
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
                _Person.ImagePath = _ImagePath;
                return _Person;
            }
        }
        public clsPeople UpdatePersonData(int PersonID)
        {
            Mode = enMode.Update;

            _Person = clsPeople.Find(PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person not found.");
                return null;
            }
            // Display person information
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
            _ImagePath = _Person.ImagePath;
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pictureBox1.ImageLocation = _Person.ImagePath;
            }

           return _Person;
            

        }
        public void DisplayPersonData(int PersonID)
        {          
                _Person = clsPeople.Find(PersonID);

                if (_Person == null)
                {
                    MessageBox.Show("Person not found.");
                    return;
                }

                // Display person information
                txtFirstName.Text = _Person.FirstName;
                txtFirstName.ReadOnly = true;

                txtSecoundName.Text = _Person.SecondName;
                txtSecoundName.ReadOnly = true;

                txtThirdName.Text = _Person.ThirdName;
                txtThirdName.ReadOnly = true;

                txtLastName.Text = _Person.LastName;
                txtLastName.ReadOnly = true;

                txtNationalID.Text = _Person.NationalNo;
                txtNationalID.ReadOnly = true;

                txtPhone.Text = _Person.Phone;
                txtPhone.ReadOnly = true;

                txtEmail.Text = _Person.Email;
                txtEmail.ReadOnly = true;

                txtAddress.Text = _Person.Address;
                txtAddress.ReadOnly = true;

                // Display country
                cbCountries.SelectedValue = _Person.NationalityCountryID;
                cbCountries.Enabled = false;
              
                dtpDateOfBirth.Enabled = false;

                // Display gender
                if (_Person.Gendor == 0)
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }

                rbMale.Enabled = false;
                rbFemale.Enabled = false;

                // Display image
                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                    pictureBox1.ImageLocation = _Person.ImagePath;
                }

                // Hide image options
                llRemoveImage.Visible = false;
                llSaveImage.Visible = false;
            
        }
        private void usPersonInfo_Load(object sender, System.EventArgs e)
        {
                cbCountries.DataSource = clsCountries.GetAllCountries();
                cbCountries.DisplayMember = "CountryName";
                cbCountries.ValueMember = "CountryID";
                cbCountries.SelectedIndex = 0;
                rbMale.Checked = true;
                pictureBox1.Image = imageList1.Images[0];
                llRemoveImage.Visible = false;
                
                    
        }
        private bool _ISValid(string Name)
        {
            foreach (char c in Name)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !char.IsPunctuation(c))
                {
                   
                    return false;
                }
            }                        
           return true;

        }


        private void txtFirstName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

           
            if (txtFirstName.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "First Name is required");
            }
            else if (_ISValid(txtFirstName.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirstName, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "Please enter letters only");
            }
                                                 

        }

        private void txtSecoundName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            if (txtSecoundName.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSecoundName, "Secound Name is required");
            }
            else if (_ISValid(txtSecoundName.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSecoundName, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSecoundName, "Please enter letters only");
            }
        }

        private void txtThirdName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            if (txtThirdName.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtThirdName, "Third Name is required");
            }
            else if (_ISValid(txtThirdName.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtThirdName, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtThirdName, "Please enter letters only");
            }
        }

        private void txtLastName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            if (txtLastName.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Last Name is required");
            }
            else if (_ISValid(txtLastName.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLastName, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Please enter letters only");
            }
        }

        private void txtNationalID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtNationalID.Text =="")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalID, "Please enter UniqueNational Number ");
            }
            else if ((clsPeople.IsNationalIDExist(txtNationalID.Text)) && (Mode == enMode.Addnew))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalID, "National Number already exists.");
            }
            else
            {

                e.Cancel = false;
                errorProvider1.SetError(txtNationalID, "");
            }
        }

        private void txtAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtAddress.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "Please enter An Address");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAddress, "");
            }
        }

        private void txtPhone_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (txtPhone.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Please enter a Phone Number");
                return;
            }

            foreach (char c in txtPhone.Text)
            {
                    if (!char.IsDigit(c))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtPhone, "Invalid phone number ");
                        return;
                    }
            }

            e.Cancel = false;
            errorProvider1.SetError(txtPhone, "");

        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ( txtEmail.Text.EndsWith("@gmail.com") || txtEmail.Text == "")
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Please enter a valid email address.");
            }

        }

        private void rbMale_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbMale.Checked)
            {
                pictureBox1.Image = imageList1.Images[0];
            }
        }

        private void rbFemale_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbFemale.Checked)
            {
                pictureBox1.Image = imageList1.Images[1];
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
                if (Mode == enMode.Update &&
                    !string.IsNullOrEmpty(_Person.ImagePath) &&
                    File.Exists(_Person.ImagePath))
                {
                    File.Delete(_Person.ImagePath);
                }

                // Create new image path
                string FileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(openFileDialog.FileName);

                _ImagePath = Path.Combine(ImagesFolder, FileName);

                // Copy new image
                File.Copy(openFileDialog.FileName, _ImagePath);

                // Display new image
                pictureBox1.ImageLocation = _ImagePath;

                // Show remove option
                llRemoveImage.Visible = true;
            }
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image = imageList1.Images[0];
            rbMale.Checked = true;
            llRemoveImage.Visible = false;
            _ImagePath = "";
        }

        
    }
}
