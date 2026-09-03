using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DVLD_DataAcessLayer
    {
        public class clsPeopleDataAcess

        {
            public  static DataTable GetAllPeople()
            {
                DataTable TbPeople = new DataTable();
                SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
                string query = "SELECT PersonID , NationalNo , FirstName , SecondName , ThirdName , LastName ,DateOfBirth , Gendor , CountryName , Phone , Email ,ImagePath " +
                    "   FROM People   JOIN   Countries   ON Countries.CountryID = NationalityCountryID ";

                SqlCommand command =  new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        TbPeople.Load(Reader);
                    }

                    Reader.Close();

                }
                catch (Exception ex)
                {

                }
                finally { connection.Close(); }

                return TbPeople;

            }
            public static DataTable GetPeopleBy(string Column , string Name= "NationalNo")
        {
            DataTable TbPeople = new DataTable();
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT PersonID , NationalNo , FirstName , SecondName , ThirdName , LastName ,DateOfBirth , Gendor , CountryName , Phone , Email  , ImagePath " +
                $"   FROM People   JOIN   Countries   ON Countries.CountryID = NationalityCountryID Where {Column} = @Data ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Data", Name);


            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    TbPeople.Load(Reader);
                }

                Reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TbPeople;
        }
            public static DataTable GetPeopleBy(string Column, int Name)
        {
            DataTable TbPeople = new DataTable();
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT PersonID , NationalNo , FirstName , SecondName , ThirdName , LastName ,DateOfBirth , Gendor , CountryName , Phone , Email , ImagePath" +
                $"   FROM People   JOIN   Countries   ON Countries.CountryID = NationalityCountryID Where {Column} = @Data ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Data", Name);


            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    TbPeople.Load(Reader);
                }

                Reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TbPeople;
        }
            public static int AddNewPerson (string NationalNo , string FirstName , string SecondName , string ThirdName , string LastName , 
                    DateTime DateOfBirth , int Gendor , string Address , string Phone , string Email , int NationalityCountryID , string ImagePath)
        {

            int PersonID = -1; 
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);

            string query = "INSERT INTO People (NationalNo , FirstName , SecondName , ThirdName , LastName ,  DateOfBirth ,  Gendor ," +
                "  Address ,  Phone ,  Email ,  NationalityCountryID ,  ImagePath) " +
                "  VALUES (@NationalNo , @FirstName , @SecondName , @ThirdName , @LastName ,  @DateOfBirth ,  @Gendor ," +
                "  @Address ,  @Phone ,  @Email , @NationalityCountryID , @ImagePath);  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo); 
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    PersonID = Convert.ToInt32(result);
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
            return PersonID;

        }
            public static bool IsNationalIDExist(string NationalNo)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "SELECT FOUND=1 FROM People  Where NationalNo = @NationalNo"; 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader resault = command.ExecuteReader();

                if (resault.HasRows)
                {
                    IsFound = true;
                }
                

            }
            catch(Exception ex)
            {
                IsFound=false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
            public static bool GetPeopleByPersonID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
                          ref DateTime DateOfBirth, ref int Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
            {
            bool isFound = false;

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            NationalNo = reader["NationalNo"] == DBNull.Value ? "" : reader["NationalNo"].ToString();
                            FirstName = reader["FirstName"] == DBNull.Value ? "" : reader["FirstName"].ToString();
                            SecondName = reader["SecondName"] == DBNull.Value ? "" : reader["SecondName"].ToString();
                            ThirdName = reader["ThirdName"] == DBNull.Value ? "" : reader["ThirdName"].ToString();
                            LastName = reader["LastName"] == DBNull.Value ? "" : reader["LastName"].ToString();

                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gendor = Convert.ToInt32(reader["Gendor"]);

                            Address = reader["Address"] == DBNull.Value ? "" : reader["Address"].ToString();
                            Phone = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString();
                            Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();

                            NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                            ImagePath = reader["ImagePath"] == DBNull.Value
                                ? ""
                                : reader["ImagePath"].ToString();
                        }
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

                 return isFound;
             }

        public static bool GetPeopleByPersonID(ref int PersonID,  string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
                         ref DateTime DateOfBirth, ref int Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            using (SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NationalNo", NationalNo);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            PersonID = reader["PersonID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PersonID"]);
                            FirstName = reader["FirstName"] == DBNull.Value ? "" : reader["FirstName"].ToString();
                            SecondName = reader["SecondName"] == DBNull.Value ? "" : reader["SecondName"].ToString();
                            ThirdName = reader["ThirdName"] == DBNull.Value ? "" : reader["ThirdName"].ToString();
                            LastName = reader["LastName"] == DBNull.Value ? "" : reader["LastName"].ToString();

                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gendor = Convert.ToInt32(reader["Gendor"]);

                            Address = reader["Address"] == DBNull.Value ? "" : reader["Address"].ToString();
                            Phone = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString();
                            Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();

                            NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                            ImagePath = reader["ImagePath"] == DBNull.Value
                                ? ""
                                : reader["ImagePath"].ToString();
                        }
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

            return isFound;
        }
        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                    DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "UPDATE People  SET " + "NationalNo = @NationalNo, " +
                "   FirstName = @FirstName ,SecondName = @SecondName , ThirdName = @ThirdName ,LastName=@LastName,DateOfBirth=@DateOfBirth ," +
                "Gendor = @Gendor , Address = @Address , Phone=@Phone , Email = @Email , NationalityCountryID=@NationalityCountryID , ImagePath=@ImagePath  " +
                "Where  PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }     
            public static bool DeletePerson(int PersonID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(PeopeleDatasettings.ConnectionString);
            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

             try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();

            }catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;

        }
            
    }
}
