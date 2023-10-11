using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.MatchupTeam
{
    public class MatchupTeamModel : PageModel
    {
        public List<MatchupTeamInfo> matchupTeamList = new List<MatchupTeamInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM MatchupTeam";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
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
    public class MatchupTeamInfo
    {
        public int iD;
        public int score;
        public int teamId;
        public int matchupId;
    }
}
