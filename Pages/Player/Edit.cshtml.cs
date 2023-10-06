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

        public void EditPlayer()
        {
            var playerID = Request.Form["PlayerID"];
            var teamID = Request.Form["TeamID"];
            var firstName = Request.Form["FirstnName"];
            var lastName = Request.Form["LastName"];

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlaner;User ID=Admin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE Player SET TeamID = {teamID} WHERE PlayerID = 1";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        
    }
}
