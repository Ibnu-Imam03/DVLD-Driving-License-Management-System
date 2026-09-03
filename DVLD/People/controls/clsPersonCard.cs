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
using System.Windows;
using System.Windows.Forms;

namespace DVLD.People.controls
{
    public partial class clsPersonCard : UserControl
    {
        private clsPeople _Person;
        private int _PersonID=-1;
        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPeople SelectedPersonInfo
        {
            get { return _Person; }
        }
        public clsPersonCard()
        {
            InitializeComponent();
        }
        public clsPersonCard(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        private void _FillPersonInfo()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gendor == 0 ? "Male" : "Female" ;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateofBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsCountries.GetCountryName(_Person.NationalityCountryID);
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                if (File.Exists(_Person.ImagePath))
                {
                    pictureBox1.ImageLocation = _Person.ImagePath;
                }
                else
                {
                }
            }
            else
            {
                pictureBox1.Image =  (_Person.Gendor == 0 ? Resources.male : Resources.female_worker);
            }
            llEditPersonInfo.Visible = false;
        }
        public void ResetPersonInfo()
        {
            lblPersonID.Text = "????";
            lblName.Text = "????";
            lblNationalNo.Text = "????";
            lblGender.Text = "????";
            lblEmail.Text = "????";
            lblAddress.Text = "????";
            lblDateofBirth.Text = "????";
            lblPhone.Text = "????";
            lblCountry.Text = "????";
            pictureBox1.Image = Resources.add_user;
            llEditPersonInfo.Visible = false;


        }
        public void  LoadPersonInfo (int personID)
        {
            _Person = clsPeople.Find(personID);
            if (_Person == null)
            {
                ResetPersonInfo();
                return;
            }
            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPeople.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                return;
            }
            _FillPersonInfo();
        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
        private void clsPersonCard_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
