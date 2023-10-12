using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class DeletePlayerModel : PageModel
    {
        /// <summary>
        /// Upon selecting a player to delete, deletes said player from the database
        /// </summary>
        /// <returns>Players page</returns>
        public IActionResult OnGet()
        {
            string url = Request.GetDisplayUrl();
            string[] noget = url.Split('=');
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM Player WHERE Id={noget.AsQueryable().Last()}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }

            catch (Exception ex)
            {

            }
            return RedirectToPage("/Player/Player");
        }
    }
}
