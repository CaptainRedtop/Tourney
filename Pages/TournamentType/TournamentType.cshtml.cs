using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.TournamentType
{    /// <summary>
     /// MSSQL connection and query read
     /// </summary>
    public class TournamentTypeModel : PageModel
    {
        Connection con = new Connection();
        public List<TournamentTypeInfo> listTournamentType = new List<TournamentTypeInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM TournamentType";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TournamentTypeInfo tournament = new TournamentTypeInfo();
                                tournament.tournamentTypeID = reader.GetInt32(0);
                                tournament.tournamentTypeName = reader.GetString(1);

                                listTournamentType.Add(tournament);
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
    public class TournamentTypeInfo
    {
        public int tournamentTypeID;
        public string tournamentTypeName;
    }
}
