using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TourneyPlaner.Pages.Player;

namespace TourneyPlaner.Pages.GameType
{
    public class GameTypeModel : PageModel
    {
        public List<GameTypeInfo> gameTypeList = new List<GameTypeInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM GameType";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
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
    public class GameTypeInfo
    {
        public int Id;
        public string Name;
        public int TeamsPerMatch;
        public int PointsForDraw;
        public int PointsForWin;
    }
}
