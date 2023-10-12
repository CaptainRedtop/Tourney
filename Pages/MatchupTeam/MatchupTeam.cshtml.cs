using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.MatchupTeam
{
    public class MatchupTeamModel : PageModel
    {
        Connection con = new Connection();

        // Holds list of matchup team to display on HTML
        public List<MatchupTeamInfo> matchupTeamList = new List<MatchupTeamInfo>();

        /// <summary>
        /// Gets every matchup team in the table, adds it to a list to display on the html page
        /// </summary>
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM MatchupTeam";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // For every matchup team in table, creates an object that represents said matchup team, and adds it to a list
                            while (reader.Read())
                            {
                                MatchupTeamInfo matchupTeam = new MatchupTeamInfo();
                                matchupTeam.iD = reader.GetInt32(0);
                                matchupTeam.score = reader.GetInt32(1);
                                matchupTeam.teamId = reader.GetInt32(2);
                                matchupTeam.matchupId = reader.GetInt32(3);

                                matchupTeamList.Add(matchupTeam);
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
    /// Required attributes for displaying a matchup team
    /// </summary>
    public class MatchupTeamInfo
    {
        public int iD;
        public int score;
        public int teamId;
        public int matchupId;
    }
}
