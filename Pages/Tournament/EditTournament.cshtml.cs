using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Tournament
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public TournamentEdit TournamentEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string name = TournamentEdit.name;
            DateOnly startDate = TournamentEdit.startDate;
            DateOnly endDate = TournamentEdit.endDate;
            int gameTypeID = TournamentEdit.gameTypeID;
            int tournamentTypeID = TournamentEdit.tournamentTypeID;
            int userID = TournamentEdit.userID;

            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Tournament SET name = '{name}', StartDate = {startDate}, EndDate = {endDate}, GameTypeId = {gameTypeID}, TournamentTypeId = {tournamentTypeID}, UserId = {userID} WHERE Id = {urlID.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/Tournament/Tournament");
        }
    }
    public class TournamentEdit
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
