using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Matchup
{
    public class MatchupModel : PageModel
    {
        Connection con = new Connection();

        // Holds list of matchups to display on HTML
        public List<MatchupInfo> matchupList = new List<MatchupInfo>();

        /// <summary>
        /// Gets every matchup in the table, adds it to a list to display on the html page
        /// </summary>
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Matchup";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // For every matchup in table, creates an object that represents said matchup, and adds it to a list
                            while (reader.Read())
                            {
                                MatchupInfo matchup = new MatchupInfo();
                                matchup.iD = reader.GetInt32(0);
                                matchup.startDateTime = reader.GetDateTime(1);
                                matchup.rounds = reader.GetInt32(2);
                                matchup.tournamentID = reader.GetInt32(3);
                                matchup.nextMatchupID = reader.GetInt32(4);

                                matchupList.Add(matchup);
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
    /// Required attributes for displaying a matchup
    /// </summary>
    public class MatchupInfo
    {
        public int iD;
        public DateTime startDateTime;
        public int rounds;
        public int tournamentID;
        public int nextMatchupID;
    }
}