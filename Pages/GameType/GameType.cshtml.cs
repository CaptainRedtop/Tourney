using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TourneyPlaner.Pages.Player;

namespace TourneyPlaner.Pages.GameType
{
    public class GameTypeModel : PageModel
    {
        Connection con = new Connection();

        // Holds list of game types to display on HTML
        public List<GameTypeInfo> gameTypeList = new List<GameTypeInfo>();

        /// <summary>
        /// Gets every game type in the table, adds it to a list to display on the html page
        /// </summary>
        public void OnGet()
        {
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM GameType";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // For every game type in table, creates an object that represents said game type, and adds it to a list
                            while (reader.Read())
                            {
                                GameTypeInfo gameType = new GameTypeInfo();
                                gameType.Id = reader.GetInt32(0);
                                gameType.Name = reader.GetString(1);
                                gameType.TeamsPerMatch = reader.GetInt32(2);
                                gameType.PointsForDraw = reader.GetInt32(3);
                                gameType.PointsForWin = reader.GetInt32(4);

                                gameTypeList.Add(gameType);
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
    /// Required attributes for displaying a game type
    /// </summary>
    public class GameTypeInfo
    {
        public int Id;
        public string Name;
        public int TeamsPerMatch;
        public int PointsForDraw;
        public int PointsForWin;
    }
}
