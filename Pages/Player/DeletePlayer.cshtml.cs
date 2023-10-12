using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class DeletePlayerModel : PageModel
    {
        Connection con = new Connection();
        public IActionResult OnGet()
        {
            string url = Request.GetDisplayUrl();
            string[] noget = url.Split('=');
            try
            {

                string connectionString = con.ConnectionString();
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
