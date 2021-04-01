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
    [Authorize]
    public class MyEventsModel : PageModel
    {

        private readonly EventsDbContext _context;
        private readonly UserManager<Attendee> _userManager;

        public MyEventsModel(
            EventsDbContext context,
            UserManager<Attendee> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }
        public IList<Event> Events { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _context.Attendees
                .Where(u => u.Id == userId)
                .Include(u => u.Events)
                .FirstOrDefaultAsync();

            Events = user.Events;
            
        }
    }
}
