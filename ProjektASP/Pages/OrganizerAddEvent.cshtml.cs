using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektASP.Data;
using ProjektASP.Models;

namespace ProjektASP.Pages
{
    [Authorize(Roles = "Organizer")]
    public class OrganizerAddEventModel : PageModel
    {
        private readonly EventsDbContext _context;
        private readonly UserManager<Attendee> _userManager;

        public OrganizerAddEventModel(EventsDbContext context, UserManager<Attendee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }
        public Attendee Attendee { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var GetId = _userManager.GetUserId(User);

           

            var user = await _context.Attendees
                .Where(u => u.Id == GetId)
                .Include(u => u.Events)
                .FirstOrDefaultAsync();

            user.Events.Add(Event);
          

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
