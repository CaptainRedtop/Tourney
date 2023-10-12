using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.FavoriteMatchup
{
    public class CreateFavoriteMatchupModel : PageModel
    {
        Connection con = new Connection();

        [BindProperty]
        public FavoriteMatchupCreate FavoriteMatchupCreate { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            int matchupId = FavoriteMatchupCreate.matchupId;
            int userId = FavoriteMatchupCreate.userId;

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO FavoriteMatchup (MatchupId, UserId) VALUES ('{matchupId}','{userId}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/FavoriteMatchup/FavoriteMatchup");
        }
    }
    public class FavoriteMatchupCreate
    {
        [Required]
        public int matchupId { get; set; }
        [Required]
        public int userId { get; set; }
    }
}