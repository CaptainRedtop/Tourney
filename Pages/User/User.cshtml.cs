using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages
{
    public class UsersModel : PageModel
    {
        Connection con = new Connection();

        // Holds list of users to display on HTML
        public List<UserInfo> listUsers = new List<UserInfo>();

        /// <summary>
        /// Gets every user in the table, adds it to a list to display on the html page
        /// </summary>
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                //string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM [User]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // For every user in table, creates an object that represents said user, and adds it to a list
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
    }

    /// <summary>
    /// Required attributes for displaying a user
    /// </summary>
    public class UserInfo
    {
        public int userID;
        public string userName;
        public string password;
        public string salt;
    }
}
