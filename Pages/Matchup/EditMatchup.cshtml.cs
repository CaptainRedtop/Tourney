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
        [BindProperty]
        public MatchupEdit MatchupEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            DateTime startDateTime = MatchupEdit.startDateTime;
            int rounds = MatchupEdit.rounds;
            int tournamentId = MatchupEdit.tournamentId;
            int nextMatchupId = MatchupEdit.nextMatchupId;

            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Matchup SET StartDateTime = '{startDateTime}', Rounds = '{rounds}', TournamentId = '{tournamentId}', NextMatchupId = '{nextMatchupId}' WHERE Id = {iD.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Matchup/Matchup");
        }
    }
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