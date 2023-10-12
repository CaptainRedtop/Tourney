using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.User
{
    public class CreateUser_Model : PageModel
    {
        [BindProperty]
        public CreateUser createUser { get; set; }
        public void OnGet()
        {

        }
        /// <summary>
        /// Upon submitting a form to create a user, inserts the user's attributes as a row for the database table
        /// </summary>
        /// <returns>User page</returns>
        public IActionResult OnPost()
        {
            string email = createUser.email;
            string passwordHash = createUser.passwordHash;
            string salt = createUser.salt;

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO User(Email, PasswordHash, Salt) VALUSE ({email}', '{passwordHash}', '{salt}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/User/User");
        }
    }
    /// <summary>
    /// Required attributes for creating a user
    /// </summary>
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
