using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public MyEventsModel(EventsDbContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get; set; }

        public async Task OnGetAsync()
        {
            var attendee = await _context.Attendees.Include(a => a.Events).FirstOrDefaultAsync();
            Events = attendee.Events;
        }
    }
}
