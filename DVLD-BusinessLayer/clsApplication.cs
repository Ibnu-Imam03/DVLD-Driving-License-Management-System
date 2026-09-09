using DVLD_DataAcessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_BusinessLayer
{
    public class clsApplication
    {
        public enum enMode { AddNew=0, Upadate=1 };
        public enum enApplicationType
        {   NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3, ReplaceDamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7   
        };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        private enMode _Mode = enMode.AddNew;
        public int ApplicationID {  get; set; }
        public int ApplicationPersonID {  get; set; }
        public clsPeople PersonInfo { get; set; }
        public string ApplicationFullName
        {
            get
            {
                return clsPeople.Find(ApplicationPersonID).FullName;
            }
        } 
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID {  get; set; }
        public clsApplicationTypes ApplicationInfo;
        public enApplicationStatus ApplicationStatus { get; set; }
        public string Status
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "NEW";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "UnKnown";
                }
            }
        }
        public DateTime LastStatusDate;
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo;
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicationPersonID = -1;
            this.ApplicationDate =DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }
        private clsApplication(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationPersonID = ApplicationPersonID;
            this.PersonInfo = clsPeople.Find(ApplicationPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationInfo = clsApplicationTypes.GetApplictionTypesByID(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            _Mode = enMode.Upadate;
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate, this.ApplicationTypeID
                , (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return this.ApplicationID != -1;
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate, this.ApplicationTypeID
                , (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }
        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            if (clsApplicationData.GetApplicationInfoByID( ApplicationID, ref  ApplicantPersonID, ref  ApplicationDate, ref  ApplicationTypeID, ref  ApplicationStatus, ref  LastStatusDate, ref  PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID,  ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,  (enApplicationStatus)ApplicationStatus,  LastStatusDate,  PaidFees,  CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
                        {
                            _Mode = enMode.Upadate;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Upadate:
                    {
                        return _UpdateApplication();
                    }
            }
            return false;


        }
        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
        public bool IsApplicationExist(int applicationID)
        {
            return clsApplicationData.IsApplicationExist(applicationID);
        }
        public static bool DoesPersonHaveActiveApplication(int  PersonID,int ApplicationType)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationType);
        }
        public bool DoesPersonHaveActiveApplication(int ApplicationType)
        {
            return DoesPersonHaveActiveApplication(this.ApplicationPersonID, ApplicationType);
        }
        public static int GetActiveApplicationID(int PersonID , enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (byte)ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID , enApplicationType applicationTypeID , int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)applicationTypeID, LicenseClassID);
        }
        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicationPersonID, ApplicationTypeID);
        }




    }
}
