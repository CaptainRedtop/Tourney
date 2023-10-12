using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.TournamentType
{
    public class TournamentTypeModel : PageModel
    {
        // Holds list of tournament types to display on HTML
        public List<TournamentTypeInfo> listTournamentType = new List<TournamentTypeInfo>();

        /// <summary>
        /// Gets every tournament type in the table, adds it to a list to display on the html page
        /// </summary>
        public void OnGet()
        {
            try
            {
                //string connectionString = "Data Source=ZBC-S-NICK9281;Initial Catalog=HjemmeTest;User ID=HjemmeLogin;Password=Kode1234!";
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
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
