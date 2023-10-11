using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace TourneyPlaner.Pages.GameType
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public GameTypeEdit GameTypeEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string name = GameTypeEdit.name;
            int teamsPerMatch = GameTypeEdit.teamsPerMatch;
            int pointsForDraw = GameTypeEdit.pointsForDraw;
            int pointsForWin = GameTypeEdit.pointsForWin;

            string url = Request.GetDisplayUrl();
            string[] iD = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE GameType SET Name = '{name}', TeamsPerMatch = '{teamsPerMatch}', PointsForDraw = '{pointsForDraw}', PointsForWin = '{pointsForWin}' WHERE Id = {iD.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/GameType/GameType");
        }
    }
    public class GameTypeEdit
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int teamsPerMatch { get; set; }
        [Required]
        public int pointsForDraw { get; set;}
        [Required]
        public int pointsForWin { get; set;}
    }
}