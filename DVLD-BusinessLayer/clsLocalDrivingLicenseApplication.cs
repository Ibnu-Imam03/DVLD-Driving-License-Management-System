using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcessLayer;
namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenseApplication
    {
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseInfo();
        }
        public static DataTable GetLocalUsers(string filter , string Value)
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivers(filter, Value);
        }
    }
}
