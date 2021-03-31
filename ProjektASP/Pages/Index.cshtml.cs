using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjektASP.Data;
using ProjektASP.Models;
using System.Threading.Tasks;

namespace ProjektASP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EventsDbContext _context;
        private readonly UserManager<Attendee> _userManager;

        public IndexModel(
            ILogger<IndexModel> logger,
            EventsDbContext context,
            UserManager<Attendee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync(bool? resetDb)
        {
            if (resetDb ?? false)
            {
                await _context.ResetDb(_userManager);
            }
        }
    }
}
