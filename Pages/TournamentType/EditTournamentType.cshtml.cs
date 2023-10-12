using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.TournamentType
{
    public class EditModel : PageModel
    {
        Connection con = new Connection();

        [BindProperty]
        public TournamentTypeEdit TournamentTypeEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string name = TournamentTypeEdit.name;

            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');

            string connectionString = con.ConnectionString();
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
    public class TournamentTypeEdit
    {
        [Required]
        public string name { get; set; }
    }
}
