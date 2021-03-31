using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProjektASP.Models
{
    public class Attendee : IdentityUser
    {
        public List<Event> Events { get; set; }

    }
}
