using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Tournament
{
    public class CreateTournamentModel : PageModel
    {
        [BindProperty]
        public CreateTournament TournamentCreate { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string name = TournamentCreate.name;
            DateOnly startDate = TournamentCreate.startDate;
            DateOnly endDate = TournamentCreate.endDate;
            int gameTypeID = TournamentCreate.gameTypeID;
            int tournamentTypeID = TournamentCreate.tournamentTypeID;
            int userID = TournamentCreate.userID;

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
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
