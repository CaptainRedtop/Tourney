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
        public IActionResult OnPost()
        {

            if (Credential.UserName == "TourneyAdmin" && Credential.Password == "Kode1234!")
            {
                return RedirectToPage("/Index2");
            }
            else
            {
                // Hvis ikke korrekt, vis en fejlmeddelelse
                ModelState.AddModelError(string.Empty, "Wrong username or password :(");
                return Page();
            }
        }

    }
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