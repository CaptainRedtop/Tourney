using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class CreatePlayerModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public PlayerCreate PlayerCreate { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to create a player, inserts the player's attributes as a row for the database table
        /// </summary>
        /// <returns>Tournaments page</returns>
        public IActionResult OnPost()
        {
            string firstName = PlayerCreate.firstName;
            string lastName = PlayerCreate.lastName;
            int teamID = PlayerCreate.teamID;

            string connectionString = con.ConnectionString();
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
    /// <summary>
    /// Required attributes for creating a player
    /// </summary>
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
