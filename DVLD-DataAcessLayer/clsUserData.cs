using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_DataAcessLayer
{
    public class clsUserData
    {
      
        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT * FROM USERS WHERE UserName = @UserName AND Password =@Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    isFound = true;
                    UserID = (int)Reader["UserID"];
                    PersonID = (int)Reader["PersonID"];
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                }
                else
                {
                    isFound = false;
                }
                Reader.Close();
            }catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
            ref string Password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection (PeopeleDatasettings.ConnectionString);
            string query = "SELECT * FROM USERS WHERE UserID = @UserID";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"]; 
                    IsActive = (bool)reader["IsActive"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception es)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;


        }
        public static bool UpdateUserInfo(int UserID, string UserName, string Password,bool IsActive)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = @"Update  Users  
                            set UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive
                                where UserID = @UserID";
            SqlCommand command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();

            }
            catch(Exception es)
            {

            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }
        public static DataTable GetAllUser()
        {
            DataTable Users = new DataTable();
            SqlConnection connection = new SqlConnection (PeopeleDatasettings.ConnectionString);
            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Users.Load(reader);
                }
                reader.Close();

            }catch(Exception es)
            {

            }
            finally
            {
                connection.Close();
            }
            return Users;

        }
        public static int AddNewUser(int PersonID, string UserName, string Password,  bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
                             VALUES (@PersonID, @UserName,@Password,@IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
                

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }
        public static DataTable GetUsers(string filter, string value)
        {
            DataTable users = new DataTable();

            using (SqlConnection connection =
                new SqlConnection(PeopeleDatasettings.ConnectionString))
            {
                string query = @"
            SELECT 
                Users.UserID,
                Users.PersonID,
                FullName = People.FirstName + ' ' +
                           People.SecondName + ' ' +
                           ISNULL(People.ThirdName, '') + ' ' +
                           People.LastName,
                Users.UserName,
                Users.IsActive
            FROM Users
            INNER JOIN People
                ON Users.PersonID = People.PersonID
            WHERE 1 = 1";

                // Build the WHERE condition depending on the selected filter
                if (filter == "UserID")
                {
                    query += " AND Users.UserID = @Value";
                }
                else if (filter == "UserName")
                {
                    query += " AND Users.UserName LIKE @Value";
                }
                else if (filter == "PersonID")
                {
                    query += " AND Users.PersonID = @Value";
                }
                else if (filter == "FullName")
                {
                    query += @" AND (
                People.FirstName + ' ' +
                People.SecondName + ' ' +
                ISNULL(People.ThirdName, '') + ' ' +
                People.LastName
            ) LIKE @Value";
                }
                else if (filter == "IsActive")
                {
                    if (value == "Yes")
                    {
                        query += " AND Users.IsActive = 1";
                    }
                    else if (value == "No")
                    {
                        query += " AND Users.IsActive = 0";
                    }
                    // All → don't add a condition
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (filter == "UserID" || filter == "PersonID")
                    {
                        command.Parameters.AddWithValue("@Value", Convert.ToInt32(value));

                    }
                    else if (filter == "UserName" || filter == "FullName")
                    {
                        command.Parameters.AddWithValue("@Value", value);
                    }

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            users.Load(reader);
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

            return users;
        }
        public static bool DeleteUserInfo(int UserID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "DELETE FROM USERS WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExisted(string UserName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT FOUND=1 FROM USER WHERE UserName = @UserName";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool IsUserExitedWithPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT 1 FROM USERS WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }




    }
}
