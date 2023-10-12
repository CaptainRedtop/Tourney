using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace TourneyPlaner.Pages.FavoriteMatchup
{
    public class EditModel : PageModel
    {
        Connection con = new Connection();

        [BindProperty]
        public FavoriteMatchupEdit FavoriteMatchupEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            int matchupId = FavoriteMatchupEdit.matchupId;
            int userId = FavoriteMatchupEdit.userId;

            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE FavoriteMatchup SET MatchupId = '{matchupId}', UserId = '{userId}' WHERE Id = {iD.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/FavoriteMatchup/FavoriteMatchup");
        }
    }
    public class FavoriteMatchupEdit
    {
        [Required]
        public int matchupId { get; set; }
        [Required]
        public int userId { get; set; }
    }
}