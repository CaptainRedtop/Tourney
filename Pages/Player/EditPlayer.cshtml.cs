using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace TourneyPlaner.Pages.Player
{
    public class EditModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public CreatePlayer PlayerEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string firstName = PlayerEdit.firstName;
            string lastName = PlayerEdit.lastName;
            int teamID = PlayerEdit.teamID;

            string url = Request.GetDisplayUrl();
            string[] playerID = url.Split('=');

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Player SET FirstName = '{firstName}', LastName = '{lastName}', TeamId = {teamID} WHERE Id = {playerID.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Player/Player");
        }
    }
    public class CreatePlayer
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public int teamID { get; set; }
    }
}
