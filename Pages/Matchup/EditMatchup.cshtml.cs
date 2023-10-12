using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

namespace TourneyPlaner.Pages.Matchup
{
    public class EditModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public MatchupEdit MatchupEdit { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to edit a matchup's data, reflects the changes onto the database
        /// </summary>
        /// <returns>Matchups page</returns>
        public IActionResult OnPost()
        {
            DateTime startDateTime = MatchupEdit.startDateTime;
            int rounds = MatchupEdit.rounds;
            int tournamentId = MatchupEdit.tournamentId;
            int nextMatchupId = MatchupEdit.nextMatchupId;

            // Extracts and splits URL into substrings by = in an array to get record ID
            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Matchup SET StartDateTime = '{startDateTime}', Rounds = '{rounds}', TournamentId = '{tournamentId}', NextMatchupId = '{nextMatchupId}' WHERE Id = {iD.AsQueryable().Last() /* Gets last substring (record ID) */ }";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Matchup/Matchup");
        }
    }
    /// <summary>
    /// Required attributes for editing a matchup
    /// </summary>
    public class MatchupEdit
    {
        [Required]
        public DateTime startDateTime { get; set; }
        [Required]
        public int rounds { get; set; }
        [Required]
        public int tournamentId { get; set; }
        [Required]
        public int nextMatchupId { get; set; }
    }
}