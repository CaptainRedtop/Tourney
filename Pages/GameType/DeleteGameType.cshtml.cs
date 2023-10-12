using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.Extensions;

namespace TourneyPlaner.Pages.GameType
{
    public class DeleteModel : PageModel
    {
        Connection con = new Connection();

        public void OnGet()
        {
            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');
            try
            {
                string connectionString = con.ConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM GameType WHERE Id={urlID.AsQueryable().Last()}";
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
        }
    }
}
