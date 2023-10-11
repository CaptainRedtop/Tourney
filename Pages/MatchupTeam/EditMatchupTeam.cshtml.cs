using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

namespace TourneyPlaner.Pages.MatchupTeam
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public MatchupTeamEdit MatchupTeamEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            int score = MatchupTeamEdit.score;
            int teamId = MatchupTeamEdit.teamId;
            int matchupId = MatchupTeamEdit.matchupId;

            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE MatchupTeam SET Score = '{score}', TeamId = '{teamId}', MatchupId = '{matchupId}' WHERE Id = {iD.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/MatchupTeam/MatchupTeam");
        }
    }
    public class MatchupTeamEdit
    {
        [Required]
        public int score  { get; set; }
        [Required]
        public int teamId { get; set; }
        [Required]
        public int matchupId { get; set; }
    }
}