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

        // Holds list of tournament types to display on HTML
        public List<TournamentTypeInfo> listTournamentType = new List<TournamentTypeInfo>();

        /// <summary>
        /// Gets every tournament type in the table, adds it to a list to display on the html page
        /// </summary>
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
                            // For every tournament type in table, creates an object that represents said tournament type, and adds it to a list
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
    /// <summary>
    /// Required attributes for creating a tournament type
    /// </summary>
    public class TournamentTypeInfo
    {
        public int tournamentTypeID;
        public string tournamentTypeName;
    }
}
