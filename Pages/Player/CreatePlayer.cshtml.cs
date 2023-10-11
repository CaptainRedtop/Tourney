using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class CreatePlayerModel : PageModel
    {
        [BindProperty]
        public PlayerCreate PlayerCreate { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string firstName = PlayerCreate.firstName;
            string lastName = PlayerCreate.lastName;
            int teamID = PlayerCreate.teamID;

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO Player (FirstName, LastName, TeamId) VALUES ('{firstName}','{lastName}',{teamID})";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Player/Player");
        }
    }
    public class PlayerCreate
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public int teamID { get; set; }
    }
}
