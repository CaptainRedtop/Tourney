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

        // Holds list of teams to display on HTML
        public List<TeamInfo> teamList = new List<TeamInfo>();

        /// <summary>
        /// Gets every team in the table, adds it to a list to display on the html page
        /// </summary>
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
                            // For every team in table, creates an object that represents said team, and adds it to a list
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
    /// <summary>
    /// Required attributes for displaying a team
    /// </summary>
    public class TeamInfo
    {
        public int Id;
        public string Name;
    }
}
