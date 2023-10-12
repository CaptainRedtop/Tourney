using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Tournament
{
    public class CreateTournamentModel : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public CreateTournament TournamentCreate { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to create atournament, inserts the tournament's attributes as a row for the database table
        /// </summary>
        /// <returns>Tournaments page</returns>
        public IActionResult OnPost()
        {
            string name = TournamentCreate.name;
            DateOnly startDate = TournamentCreate.startDate;
            DateOnly endDate = TournamentCreate.endDate;
            int gameTypeID = TournamentCreate.gameTypeID;
            int tournamentTypeID = TournamentCreate.tournamentTypeID;
            int userID = TournamentCreate.userID;

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO Tournament(Name, StartDate, EndDate, GameTypeId, TournamentTypeId, UserId) VALUES('{name}',{startDate}, {endDate}, {gameTypeID}, {tournamentTypeID}, {userID})";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Tournament/Tournament");
        }
    }
    /// <summary>
    /// Required attributes for creating a tournament
    /// </summary>
    public class CreateTournament
    {
        [Required]
        public string name { get; set; }
        [Required]
        public DateOnly startDate { get; set; }
        [Required]
        public DateOnly endDate { get; set; }
        [Required]
        public int gameTypeID { get; set; }
        [Required]
        public int tournamentTypeID { get; set; }
        [Required]
        public int userID { get; set; }
    }
}
