using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TourneyPlaner.Pages.Player;

namespace TourneyPlaner.Pages.Team
{
    /// <summary>
    /// MSSQL connection and query read
    /// </summary>
    public class TeamModel : PageModel
    {
        Connection con = new Connection();
        public List<TeamInfo> teamList = new List<TeamInfo>();
        public void OnGet()
        {
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
                                TeamInfo team = new TeamInfo();
                                team.Id = reader.GetInt32(0);
                                team.Name = reader.GetString(1);

                                teamList.Add(team);
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
    public class TeamInfo
    {
        public int Id;
        public string Name;
    }
}
