using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages
{
    public class Index2Model : PageModel
    {
        private readonly ILogger<Index2Model> _logger;

        Connection con = new Connection();
        public int userCount;
        public int teamCount;
        public int gameTypeCount;

        public Index2Model(ILogger<Index2Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            //Getting number of users to show on site
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM [User]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userCount++;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            //Getting number of teams to show on site
            }
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Team";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                teamCount++;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }

            //Getting number of gametypes to show on site
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM GameType";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                gameTypeCount++;
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
}
