using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.TournamentType
{
    public class CreateTournamentTypeModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public TournamentTypeCreate TournamentTypeCreate { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string name = TournamentTypeCreate.name;
            string connectionString = con.ConnectionString();
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
    public class TournamentTypeCreate
    {
        [Required]
        public string name { get; set; }
    }
}
