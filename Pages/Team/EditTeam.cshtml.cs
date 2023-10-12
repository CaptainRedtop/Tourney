using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Team
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public TeamEdit teamEdit { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to edit a a team's data, reflects the changes onto the database
        /// </summary>
        /// <returns>Teams page</returns>
        public IActionResult OnPost()
        {
            string name = teamEdit.name;

            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Team SET Name = '{name}' WHERE Id = {urlID.AsQueryable().Last()}";
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