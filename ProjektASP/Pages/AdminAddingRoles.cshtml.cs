using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektASP.Data;
using ProjektASP.Models;

namespace ProjektASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminAddingRolesModel : PageModel
    {

        private readonly EventsDbContext _context;
        private readonly UserManager<Attendee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminAddingRolesModel(EventsDbContext context,
            UserManager<Attendee> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public List<Attendee> Users { get; set; }
        
        public async Task OnGetAsync()
        {
            Users = await _context.Attendees.ToListAsync();
        }

        [BindProperty]
        public bool CheckBox { get; set; }
        public async Task OnPost(string? id)
        {
            var newRole = _context.Attendees
                    .Where(m => m.Id == id)
                    .FirstOrDefault();
            var test = _userManager.IsInRoleAsync(newRole, "Organizer").Result;
            if (!_userManager.IsInRoleAsync(newRole, "Organizer").Result)
            { 
                await _userManager.AddToRoleAsync(newRole, "Organizer");
                await _userManager.RemoveFromRoleAsync(newRole, "Attendee");
                
                await _context.SaveChangesAsync();
            }
            else
            {
              
                await _userManager.RemoveFromRoleAsync(newRole, "Organizer");
                await _userManager.AddToRoleAsync(newRole, "Attendee");

                await _context.SaveChangesAsync();
                
            }

            await OnGetAsync();
        }
    }
}

