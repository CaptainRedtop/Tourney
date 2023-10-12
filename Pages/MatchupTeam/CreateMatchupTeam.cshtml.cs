using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace TourneyPlaner.Pages.MatchupTeam
{
    public class CreateMatchupTeamModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public MatchupTeamCreate MatchupTeamCreate { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            int score = MatchupTeamCreate.score;
            int teamId = MatchupTeamCreate.teamId;
            int matchupId = MatchupTeamCreate.matchupId;

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO MatchupTeam (Score, TeamId, MatchupId) VALUES ('{score}','{teamId}', '{matchupId}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/MatchupTeam/MatchupTeam");
        }
    }
    public class MatchupTeamCreate
    {
        [Required]
        public int score { get; set; }
        [Required]
        public int teamId { get; set; }
        [Required]
        public int matchupId { get; set; }
    }
}