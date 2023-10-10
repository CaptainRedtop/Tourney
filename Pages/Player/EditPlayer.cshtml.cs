using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class EditModel : PageModel
    {   
        public void OnGet()
        {

        }

        public string EditPlayer()
        {
            string url = Request.GetDisplayUrl();
            string[] playerID = url.Split('=');

            var teamID = Request.Form["TeamID"];
            var firstName = Request.Form["FirstnName"];
            var lastName = Request.Form["LastName"];

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Player SET TeamID = {teamID}, FirstName = {firstName}, LastName = {lastName} WHERE PlayerID = {playerID.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return "";
        }

        public void submitChanges(Object sender, EventArgs e)
        {
            EditPlayer();
        }
    }
}
