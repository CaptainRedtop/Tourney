using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.TournamentType
{
    public class CreateTournamentTypeModel : PageModel
    {
        [BindProperty]
        public TournamentTypeCreate TournamentTypeCreate { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to create tournament type, inserts the tournament type's attributes as a row for the database table
        /// </summary>
        /// <returns>Tournament type page</returns>
        public IActionResult OnPost()
        {
            string name = TournamentTypeCreate.name;
            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO TournamentType(Name)  VALUES('{name}')";
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
    /// Required attributes for tournament type
    /// </summary>
    public class TournamentTypeCreate
    {
        [Required]
        public string name { get; set; }
    }
}
