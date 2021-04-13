using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektASP.Models;
using ProjektASP.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjektASP.Pages
{
    [Authorize(Roles ="Admin")]
    public class AdminAddingRolesModel : PageModel
    {
        
        private readonly EventsDbContext _context;
        private readonly UserManager<Attendee> _userManager;
        private readonly RoleManager<Attendee> _roleManager;
        public AdminAddingRolesModel(EventsDbContext context,
            UserManager<Attendee> userManager,
            RoleManager<Attendee> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IList<Attendee> Users { get; set; }
      
        public async Task OnGetAsync()
        {
            Users = await _context.Attendees.ToListAsync();   
            
        }
    }
}
