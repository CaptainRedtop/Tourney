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
        /// <summary>
        /// Upon submitting a form to edit a favorite matchup's data, reflects the changes onto the database
        /// </summary>
        /// <returns>Matchups page</returns>
        public IActionResult OnPost()
        {
            int matchupId = FavoriteMatchupEdit.matchupId;
            int userId = FavoriteMatchupEdit.userId;

            // Extracts and splits URL into substrings by = in an array to get record ID
            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE FavoriteMatchup SET MatchupId = '{matchupId}', UserId = '{userId}' WHERE Id = {iD.AsQueryable().Last() /* Gets last substring (record ID) */ }";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/FavoriteMatchup/FavoriteMatchup");
        }
    }
    /// <summary>
    /// Required attributes for editing a favorite matchup
    /// </summary>
    public class FavoriteMatchupEdit
    {
        [Required]
        public int matchupId { get; set; }
        [Required]
        public int userId { get; set; }
    }
}