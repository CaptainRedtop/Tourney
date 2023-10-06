using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.Player
{
    public class DeletePlayerModel : PageModel
    {
        public void OnGet()
        {
            
            try
            {
                string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlaner;User ID=Admin;Password=Kode1234!";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM Player WHERE PlayerID={playerID}";
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
