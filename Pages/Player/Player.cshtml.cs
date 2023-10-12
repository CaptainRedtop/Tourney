using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class PlayerModel : PageModel
    {
        Connection con = new Connection();
        public List<PlayerInfo> listPlayer = new List<PlayerInfo>();
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
   
    public class PlayerInfo
    {
        public int playerID;
        public string firstName;
        public string lastName;
        public int teamID;
    }
}
