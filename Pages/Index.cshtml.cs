using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TourneyPlaner.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }

        /// <summary>
        /// Upon submitting login form, checks credentials
        /// </summary>
        /// <returns>Admin page</returns>
        public IActionResult OnPost()
        {

            if (Credential.UserName == "TourneyAdmin" && Credential.Password == "Kode1234!")
            {
                return RedirectToPage("/Index2");
            }
            else
            {
                // If not correct, show an error message
                ModelState.AddModelError(string.Empty, "Wrong username or password :(");
                return Page();
            }
        }

    }
    /// <summary>
    /// Required attributes for login
    /// </summary>
    public class Credential
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}