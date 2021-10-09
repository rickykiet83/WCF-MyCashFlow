using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MyCashFlow
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CashRecordService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CashRecordService.svc or CashRecordService.svc.cs at the Solution Explorer and start debugging.
    public class CashRecordService : ICashRecordService
    {
        public void AddRecord(string desc, int amount, string email)
        {
            var connection = GetConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [Finances](Description, Amount, Email) VALUES(@desc,@amount,@email)";
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@email", email);
                
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public void DeleteRecord(int id)
        {
            var connection = GetConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE [Finances] WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public List<Finance> GetAllRecords(string email)
        {
            var LF = new List<Finance>();
            var connection = GetConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, Description, Amount, Email FROM [Finances] WHERE Email=@email";
                cmd.Parameters.AddWithValue("@email", email);

                var dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    LF.Add(new Finance
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Description = dr["Description"].ToString(),
                        Amount = int.Parse(dr["Amount"].ToString()),
                        Email = dr["Email"].ToString()
                    });
                }

                return LF;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public string Login(string email, string password)
        {
            var connection = GetConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 * FROM Users WHERE Email=@email and Password=@password";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", Encrypt(password));

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                var i = Convert.ToInt32(dt.Rows.Count);

                if (i == 1)
                {
                    return "Success";
                }
                else
                {
                    return "Login Failed!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        private static SqlConnection GetConnection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            return new SqlConnection(conStr);
        }

        public string Register(string fullName, string email, string password)
        {
            var connection = GetConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 Email FROM Users WHERE Email=@email";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                var i = Convert.ToInt32(dt.Rows.Count);

                if (i <= 0)
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Users(FullName, Email, Password) VALUES(@fullName,@email,@password)";
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", Encrypt(password));
                    cmd.ExecuteNonQuery();
                    return "Success";
                }
                else
                {
                    return "Email existed!";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        private static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        public void UpdateRecord(int id, string description, int amount)
        {
            var connection = GetConnection();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [Finances] SET Description=@desc, Amount=@amount WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@amount", amount);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }
}
