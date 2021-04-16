using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektASP.Data;
using ProjektASP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektASP.Pages
{
    public class JoinEventModel : PageModel
    {
        private readonly EventsDbContext _context;
        private readonly UserManager<Attendee> _userManager;
        public JoinEventModel(EventsDbContext context,
            UserManager<Attendee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }
        public Attendee Attendee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);
            var userid = _userManager.GetUserId(User);
            Attendee = await _context.Attendees.Where(m => m.Id == userid).Include(m => m.Events).FirstOrDefaultAsync();

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            var userId =  _userManager.GetUserId(User);
            var user = await _context.Attendees
                .Where(u => u.Id == userId)
                .Include(u => u.Events)
                .FirstOrDefaultAsync();



            if (!user.Events.Contains(Event) && Event.SpotsAvailable >= 1)
            {
                user.Events.Add(Event);
                _context.Events.Where(e => e.Id == id).First().SpotsAvailable--;
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
