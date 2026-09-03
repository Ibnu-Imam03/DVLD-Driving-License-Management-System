using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcessLayer;

namespace DVLD_BusinessLayer
{
    public class clsCountries
    {
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
        public static string GetCountryName(int countryId)
        {
            return clsCountryData.GetCountryName(countryId);
        }
    }
}
