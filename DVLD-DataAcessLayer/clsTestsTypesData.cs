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
    public class clsTestsTypesData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT * FROM TestTypes ";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)

                {
                    dt.Load(reader);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static bool GetTestTypesByID(int TestTypeID,ref string TestTypeTitle,ref string TestTypeDescription,ref decimal  TestTypeFees)
        {
            bool isFound = false;

            SqlConnection connection =new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal )reader["TestTypeFees"];

                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal  TestTypeFees)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection (PeopeleDatasettings.ConnectionString);
            string query = "Update TestTypes SET TestTypeTitle =@TestTypeTitle , TestTypeDescription = @TestTypeDescription , TestTypeFees =@TestTypeFees WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;

        }

        
    }
}
