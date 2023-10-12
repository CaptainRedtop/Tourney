using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.User
{
    public class CreateUser_Model : PageModel
    {
        Connection con = new Connection();
        [BindProperty]
        public CreateUser createUser { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string email = createUser.email;
            string passwordHash = createUser.passwordHash;
            string salt = createUser.salt;

            string connectionString = con.ConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO [User] (Email, PasswordHash, Salt) VALUES ('{email}', '{passwordHash}', '{salt}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/User/User");
        }
    }
    public class CreateUser
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string passwordHash { get; set; }
        [Required]
        public string salt { get; set; }
    }
}
