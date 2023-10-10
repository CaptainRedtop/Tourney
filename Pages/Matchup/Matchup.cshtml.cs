using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Matchup
{
    public class MatchupModel : PageModel
    {
        public List<MatchupInfo> matchupList = new List<MatchupInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Matchup";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
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
    public class MatchupInfo
    {
        public int iD;
        public DateTime startDateTime;
        public int rounds;
        public int tournamentID;
        public int nextMatchupID;
    }
}