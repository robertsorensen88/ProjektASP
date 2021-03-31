using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektASP.Models;
using System.Threading.Tasks;

namespace ProjektASP.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Attendee> _signInManager;
        private readonly UserManager<Attendee> _userManager;

        public LoginModel(SignInManager<Attendee> signInManager,
            UserManager<Attendee> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public LoginUserForm LoginUser { get; set; }
        public class LoginUserForm
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager.PasswordSignInAsync(LoginUser.UserName, LoginUser.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
