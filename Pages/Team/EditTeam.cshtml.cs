using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Team
{
    public class EditModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public TeamEdit teamEdit { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to edit a team's data, reflects the changes onto the database
        /// </summary>
        /// <returns>Teams page</returns>
        public IActionResult OnPost()
        {
            string name = teamEdit.name;

            // Extracts and splits URL into substrings by = in an array to get record ID
            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Team SET Name = '{name}' WHERE Id = {urlID.AsQueryable().Last() /* Gets last substring (record ID) */ }";
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
    /// Required attributes for editing a team
    /// </summary>
    public class TeamEdit
    {
        [Required]
        public string name { get; set; }
    }
}