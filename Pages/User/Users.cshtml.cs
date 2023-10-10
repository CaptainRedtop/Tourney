using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages
{
    public class UsersModel : PageModel
    {
        public List<UserInfo> listUsers = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM User";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo user = new UserInfo();
                                user.userID = reader.GetInt32(0);
                                user.userName = reader.GetString(1);
                                user.password = reader.GetString(2);
                                user.salt = reader.GetString(3);

                                listUsers.Add(user);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        public string DeleteUser(int userID)
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlaner;User ID=Admin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM Users WHERE UserID='{userID}'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            
            catch (Exception ex)
            {

            }
            return "";
        }
    }

    public class UserInfo
    {
        public int userID;
        public string userName;
        public string password;
        public string salt;
    }
}
