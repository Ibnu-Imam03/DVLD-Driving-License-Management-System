using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAcessLayer
{
    public class ApplicationTypes
    {
        public static DataTable GetAllApplicationsTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection (PeopeleDatasettings.ConnectionString);
            string query = "SELECT ApplicationTypeID As ID , ApplicationTypeTitle As Title , ApplicationFees As Fees FROM ApplicationTypes";
            SqlCommand command = new SqlCommand (query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                
            }catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static bool UpdateApplicationsType(int ID ,string Title , decimal Fee)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection (PeopeleDatasettings.ConnectionString);
            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @Title , ApplicationFees = @Fee WHERE ApplicationTypeID = @ID"; 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fee", Fee);
            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();

            }catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }
        public static bool GetApplicationTypesUsingID(int ID,ref string Title,ref decimal Fee)
        {
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT ApplicationTypeTitle  , ApplicationFees  FROM ApplicationTypes WHERE ApplicationTypeID = @ID";
            SqlCommand command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            bool isFound = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                     isFound = true;

                    Title = reader["ApplicationTypeTitle"].ToString();
                    Fee = Convert.ToDecimal(reader["ApplicationFees"]);
                }
                else
                {
                     isFound = false;

                }
                reader.Close();

            }catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

    }
}
