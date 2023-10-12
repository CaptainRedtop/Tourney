using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Team
{
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Upon selecting a team to delete, deletes said team from the database
        /// </summary>
        /// <returns>Tournaments page</returns>
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
                    string sql = $"DELETE FROM Team WHERE Id={noget.AsQueryable().Last()}";
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
            return RedirectToPage("/Team/Team");
        }
    }
}
