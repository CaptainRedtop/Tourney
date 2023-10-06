using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class PlayerModel : PageModel
    {
        public List<PlayerInfo> listPlayer = new List<PlayerInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlaner;User ID=Admin;Password=Kode1234!";
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
                                player.teamID = reader.GetInt32(1);
                                player.firstName = reader.GetString(2);
                                player.lastName = reader.GetString(3);

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
