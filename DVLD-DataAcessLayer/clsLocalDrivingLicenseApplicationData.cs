using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_DataAcessLayer
{
    public class clsLocalDrivingLicenseApplicationData
    {

        public static bool GetLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications  WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try 
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
        return IsFound;        
        }

        public static DataTable GetAllLocalDrivingLicenseInfo()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                              FROM LocalDrivingLicenseApplications_View
                              order by ApplicationDate Desc";




            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetLocalDrivers(string filter, string value)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString))
            {
                string query = @"SELECT * FROM LocalDrivingLicenseApplications_View order by ApplicationDate Desc WHERE 1 = 1 ";

                // Build the WHERE condition depending on the selected filter
                if (filter == "L.D.L.AppID")
                {
                    query += " AND LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  = @Value";
                }
                else if (filter == "National No.")
                {
                    query += " AND People.NationalNo LIKE @Value";
                }
                else if (filter == "Status")
                {
                    query += " AND ApplicationStatus = @Value";
                }
                else if (filter == "Full Name")
                {
                    query += @" AND (People.FirstName + ' ' +People.SecondName + ' ' +ISNULL(People.ThirdName, '') + ' ' +People.LastName) LIKE @Value";
                }
                

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (filter == "L.D.L.AppID" || filter == "ApplicationStatus")
                    {
                        command.Parameters.AddWithValue("@Value", Convert.ToInt32(value));

                    }
                    else if (filter == "National No." || filter == "Full Name")
                    {
                        command.Parameters.AddWithValue("@Value", value);
                    }

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dt;
        }



    }
}
