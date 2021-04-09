using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektASP.Models;

namespace ProjektASP.Pages
{
    [Authorize(Roles ="Admin")]
    public class AdminAddingRolesModel : PageModel
    {
        SignInManager<Attendee> _signInManager;
        public void OnGet()
        {
        }
    }
}
