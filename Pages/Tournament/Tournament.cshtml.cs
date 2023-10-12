using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Tournament
{
    /// <summary>
    /// MSSQL connection and query read
    /// </summary>
    public class TournamentModel : PageModel
    {
        Connection con = new Connection();
        public List<TournamentInfo> tournaments = new List<TournamentInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Tournament";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TournamentInfo tournament = new TournamentInfo();
                                tournament.tournamentID = reader.GetInt32(0);
                                tournament.tournamentName = reader.GetString(1);
                                tournament.startDate = reader.GetDateTime(2);
                                tournament.endDate = reader.GetDateTime(3);
                                tournament.gameTypeID = reader.GetInt32(4);
                                tournament.tournementTypeID = reader.GetInt32(5);
                                tournament.userID = reader.GetInt32(6);

                                tournaments.Add(tournament);
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
    public class TournamentInfo
    {
        public int tournamentID;
        public string tournamentName;
        public DateTime startDate;
        public DateTime endDate;
        public int gameTypeID;
        public int tournementTypeID;
        public int userID;
    }
}
