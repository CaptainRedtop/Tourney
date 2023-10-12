using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.User
{
    public class DeleteModel : PageModel
    {
        Connection con = new Connection();

        /// <summary>
        /// Upon selecting a user to delete, deletes said user from the database
        /// </summary>
        /// <returns>User page</returns>
        public IActionResult OnGet()
        {
            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');
            try
            {
                string connectionString = con.ConnectionString();
                //string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM [User] WHERE Id={urlID.AsQueryable().Last()}";
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
            return RedirectToPage("/User/User");
        }
    }
}
