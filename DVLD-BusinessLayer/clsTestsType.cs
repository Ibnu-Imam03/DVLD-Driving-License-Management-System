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
    public class clsTestsType
    {

        public int TestTypeID {  get; set; }
        public string TestTitle {  get; set; }
        public string TestDescribtion { get; set; }
        public decimal  fee {  get; set; }
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public static DataTable GetAllTestType()
        {
            return clsTestsTypesData.GetAllTestTypes();
        }

        public clsTestsType (int testTypeID, string TestTitle, string TestDescribtion, decimal  fee)
        {
            this.TestTypeID = testTypeID;
            this.TestTitle = TestTitle;
            this.TestDescribtion = TestDescribtion;
            this.fee = fee;

        }

        public  static clsTestsType FindTestTypeByID(int ID)
        {
            string TestTitle = "";
            string TestDescribtion = "";
            decimal  fee = -1;

            if (clsTestsTypesData.GetTestTypesByID(ID, ref TestTitle, ref TestDescribtion, ref fee))
            {
                return new clsTestsType(ID, TestTitle, TestDescribtion, fee);
            }
            else
            {
                return null;
            }
        }
        private bool _UpdateTestTypes()
        {
            return clsTestsTypesData.UpdateTestType(this.TestTypeID,this.TestTitle,this.TestDescribtion,this.fee);
        }


        public bool Save()
        {
            return _UpdateTestTypes();
        }
    }
}
