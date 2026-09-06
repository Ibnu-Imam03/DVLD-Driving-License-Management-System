using DVLD_DataAcessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_BusinessLayer
{
    public class clsApplicationTypes
    {

        public int ID {  get; set; }
        public string Title {  get; set; }
        public decimal Fee { get; set; }
        enum enMode { AddNew=0,Update=1};
        enMode _Mode;
        clsApplicationTypes(int ID,string Title, decimal Fee)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fee = Fee;
            _Mode = enMode.Update;
        }
        public static DataTable GetAllapplicationTypes()
        {
            return ApplicationTypes.GetAllApplicationsTypes();
        }
        public  bool _UpdatelApplicationsType()
        {
            return ApplicationTypes.UpdateApplicationsType(this.ID, this.Title, this.Fee);
        }
        public static clsApplicationTypes GetApplictionTypesByID(int ID)
        {
            string Title = "";
            decimal Fee = -1;

            if (ApplicationTypes.GetApplicationTypesUsingID(ID,ref Title,ref Fee))
            {
                return new clsApplicationTypes(ID, Title, Fee);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            if (_Mode == enMode.Update)
            {
                return _UpdatelApplicationsType();
            }
            return false;
        }

    }
}
