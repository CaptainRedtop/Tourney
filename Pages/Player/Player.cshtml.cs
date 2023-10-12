using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class PlayerModel : PageModel
    {
        Connection con = new Connection();
        // Holds list of players to display on HTML
        public List<PlayerInfo> listPlayer = new List<PlayerInfo>();
        /// <summary>
        /// Gets every player in the table, adds it to a list to display on the html page
        /// </summary>
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Player";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // For every player in table, creates an object that represents said player, and adds it to a list
                            while (reader.Read())
                            {
                                PlayerInfo player = new PlayerInfo();
                                player.playerID = reader.GetInt32(0);
                                player.firstName = reader.GetString(1);
                                player.lastName = reader.GetString(2);
                                player.teamID = reader.GetInt32(3);

                                listPlayer.Add(player);
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
   /// Required attributes for displaying a player
   /// </summary>
    public class PlayerInfo
    {
        public int playerID;
        public string firstName;
        public string lastName;
        public int teamID;
    }
}
