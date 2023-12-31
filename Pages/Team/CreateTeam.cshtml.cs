using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Team
{
    public class CreateTeamModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public TeamCreate teamCreate { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to create a team, inserts the team's attributes as a row for the database table
        /// </summary>
        /// <returns>Tournaments page</returns>
        public IActionResult OnPost()
        {
            string name = teamCreate.name;

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO Team(Name) VALUES('{name}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Team/Team");
        }
    }
    /// <summary>
    /// Required attributes to create a team
    /// </summary>
    public class TeamCreate
    {
        [Required]
        public string name { get; set; }
    }
}
