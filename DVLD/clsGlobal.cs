using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string  username, string password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string FilePath = Path.Combine(currentDirectory, "data.txt");
                if (username =="" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }
                string DataToSave = username + "#//#" + password;

                using (StreamWriter Writer = new StreamWriter(FilePath))
                {
                    Writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredential (ref string Username, ref string Password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string FilePath = currentDirectory + "\\data.txt";

                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string Line;
                        while((Line = reader.ReadLine())!= null)
                        {
                            Console.WriteLine(Line); // Output each line of data to the console
                            string[] result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }


    }
}
