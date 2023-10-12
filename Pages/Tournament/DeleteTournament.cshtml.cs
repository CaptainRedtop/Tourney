using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Tournament
{
    public class DeleteModel : PageModel
    {
        Connection con = new Connection();

        /// <summary>
        /// Upon selecting a tournament to delete, deletes said tournament from the database
        /// </summary>
        /// <returns>Tournaments page</returns>
        public IActionResult OnGet()
        {
            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM Tournament WHERE Id={urlID.AsQueryable().Last()}";
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
            return RedirectToPage("/Tournament/Tournament");
        }
    }
}
