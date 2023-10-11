using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.FavoriteMatchup
{
    public class FavoriteMatchupModel : PageModel
    {
        public List<FavoriteMatchupInfo> favoriteMatchupList = new List<FavoriteMatchupInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Matchup";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
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
    public class FavoriteMatchupInfo
    {
        public int iD;
        public int matchupID;
        public int userID;
    }
}
