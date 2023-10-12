using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace TourneyPlaner.Pages.GameType
{
    public class EditModel : PageModel
    {
        Connection con = new Connection();

        [BindProperty]
        public GameTypeEdit GameTypeEdit { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to edit a game type's data, reflects the changes onto the database
        /// </summary>
        /// <returns>Matchups page</returns>
        public IActionResult OnPost()
        {
            string name = GameTypeEdit.name;
            int teamsPerMatch = GameTypeEdit.teamsPerMatch;
            int pointsForDraw = GameTypeEdit.pointsForDraw;
            int pointsForWin = GameTypeEdit.pointsForWin;

            // Extracts and splits URL into substrings by = in an array to get record ID
            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE GameType SET Name = '{name}', TeamsPerMatch = '{teamsPerMatch}', PointsForDraw = '{pointsForDraw}', PointsForWin = '{pointsForWin}' WHERE Id = {iD.AsQueryable().Last() /* Gets last substring (record ID) */ }";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/GameType/GameType");
        }
    }

    /// <summary>
    /// Required attributes for editing a game type
    /// </summary>
    public class GameTypeEdit
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int teamsPerMatch { get; set; }
        [Required]
        public int pointsForDraw { get; set;}
        [Required]
        public int pointsForWin { get; set;}
    }
}