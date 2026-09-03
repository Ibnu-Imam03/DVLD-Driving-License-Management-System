using DVLD_DataAcessLayer;
using System;
using System.Data;
using System.Net;

namespace DVLD_BusinessLayer
{
    public class clsPeople
    {

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get { return FirstName +"   " + SecondName + "   "+ThirdName +"  "+ LastName; } }
        public enum enMode { AddNew = 0, Update = 1 };
        public  enMode Mode = enMode.AddNew;


        public clsPeople()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }

        private clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
             string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;

        }

        public static DataTable GetAllPeoeple()
        {
            return clsPeopleDataAcess.GetAllPeople();
        }

        public static DataTable GetPerson(string Column, string Data)
        {
            return clsPeopleDataAcess.GetPeopleBy(Column, Data);
        }
        public static DataTable GetPerson(string Column, int Data)
        {
            return clsPeopleDataAcess.GetPeopleBy(Column, Data);
        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleDataAcess.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor,
                                                             Address, Phone, Email, NationalityCountryID, ImagePath);

            return this.PersonID != -1;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                case enMode.Update:
                    {
                        return _UpdatePerson();
                    }

            }
            return false;
        }

        public static bool IsNationalIDExist(string NID)
        {
            return clsPeopleDataAcess.IsNationalIDExist(NID);
        }

        public static clsPeople Find(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gendor = 0, NationalityCountryID = -1;

            if (clsPeopleDataAcess.GetPeopleByPersonID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            }

            return null;
        }
        public static clsPeople Find(string NationalNo)
        {
            int PersonID = -1; string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gendor = 0, NationalityCountryID = -1;

            if (clsPeopleDataAcess.GetPeopleByPersonID(ref PersonID,  NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            }

            return null;
        }

        private  bool _UpdatePerson()
        {
            return clsPeopleDataAcess.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                     this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleDataAcess.DeletePerson(PersonID);
        }

    }
}
