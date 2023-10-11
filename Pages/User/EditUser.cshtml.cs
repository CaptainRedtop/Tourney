using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TourneyPlaner.Pages.User
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UserEdit userEdit { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string email = userEdit.email;
            string passwordHash = userEdit.passwordHash;
            string salt = userEdit.salt;

            string url = Request.GetDisplayUrl();
            string[] urlID = url.Split('=');

            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE User SET Email = '{email}', PasswordHash = '{passwordHash}', Salt = '{salt}' WHERE Id = {urlID.AsQueryable().Last()}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToPage("/User/User");
        }
    }
    public class UserEdit
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string passwordHash { get; set; }
        [Required]
        public string salt { get; set; }
    }
}
