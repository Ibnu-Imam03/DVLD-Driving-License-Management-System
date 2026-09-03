using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcessLayer;
namespace DVLD_BusinessLayer
{
    public class clsUser
    {
        public int UserID {  get; set; }
        public int PersonID {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive {  get; set; }
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = string.Empty;
            IsActive = false;
            _Mode = enMode.AddNew;
        }
        public clsUser(string username, string password, int PersonID , int UserID , bool ISActive)
        {
            this.UserName = username;
            this.Password= password;
            this.PersonID = PersonID;
            this.UserID = UserID;
            this.IsActive = ISActive;
            _Mode = enMode.Update;
        }
        public static clsUser FindByUserNameAndPassword(string username, string password)
        {
            int PersonID = -1;
            int UserID = -1;
            bool IsActive = false;
            if (clsUserData.GetUserInfoByUsernameAndPassword(username, password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(username, password, UserID, PersonID, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUserID
                                (UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserName, Password, PersonID, UserID, IsActive);
            else
                return null;
        }

       
        private  bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return this.UserID != -1;

        }
        
        private  void _UpdateUserInfo()
        {
            clsUserData.UpdateUserInfo(UserID, UserName, Password, IsActive);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            return true;
                            _Mode = enMode.Update;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        _UpdateUserInfo();
                        return true;
                    }
            }
            return false;
        } 

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUser();
        }

    }
}
