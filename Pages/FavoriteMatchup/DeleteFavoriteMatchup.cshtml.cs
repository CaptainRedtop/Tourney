using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.FavoriteMatchup
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
                    string sql = $"DELETE FROM FavoritMatchup WHERE Id={urlID.AsQueryable().Last()}";
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
