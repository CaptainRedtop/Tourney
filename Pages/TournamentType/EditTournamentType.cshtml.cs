using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.TournamentType
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public TournamentTypeEdit TournamentTypeEdit { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to edit a a tournament type's data, reflects the changes onto the database
        /// </summary>
        /// <returns>Tournament type page</returns>
        public IActionResult OnPost()
        {
            string name = TournamentTypeEdit.name;

            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE TournamentType SET name = '{name}' WHERE Id = {urlID.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/TournamentType/TournamentType");
        }
    }
    /// <summary>
    /// Required attributes for editing a tournament type
    /// </summary>
    public class TournamentTypeEdit
    {
        [Required]
        public string name { get; set; }
    }
}
