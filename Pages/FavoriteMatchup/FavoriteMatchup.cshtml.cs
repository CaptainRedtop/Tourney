using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.FavoriteMatchup
{
    public class FavoriteMatchupModel : PageModel
    {
        Connection con = new Connection();


        // Holds list of favorite matchups to display on HTML
        public List<FavoriteMatchupInfo> favoriteMatchupList = new List<FavoriteMatchupInfo>();

        /// <summary>
        /// Gets every favorite matchup in the table, adds it to a list to display on the html page
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
                            // For every favorite matchup in table, creates an object that represents said favorite matchup, and adds it to a list
                            while (reader.Read())
                            {
                                FavoriteMatchupInfo favoriteMatchup = new FavoriteMatchupInfo();
                                favoriteMatchup.iD = reader.GetInt32(0);
                                favoriteMatchup.matchupID = reader.GetInt32(1);
                                favoriteMatchup.userID = reader.GetInt32(2);

                                favoriteMatchupList.Add(favoriteMatchup);
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
    /// Required attributes for displaying a favorite matchup
    /// </summary>
    public class FavoriteMatchupInfo
        {
            public int iD;
            public int matchupID;
            public int userID;
        }
    }
