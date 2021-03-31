using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektASP.Models;
using System;
using System.Threading.Tasks;

namespace ProjektASP.Data
{
    public class EventsDbContext : IdentityDbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
           : base(options)
        {

        }
        //public DbSet<Organiser> Organizers { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }



        public async Task ResetDb(UserManager<Attendee> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            Attendee attendee = new Attendee()
            {
                UserName = "Kalle",
                Email = "Kalle@gmail.com",
                PhoneNumber = "0734-431267"

            };

            Event[] events = new Event[] {
                new Event(){
                    Title="Summer camp",
                    Description="Have a fun time chilling in the sun",
                    Place="Colorado springs",
                    Date=DateTime.Now.AddDays(34),
                    SpotsAvailable=234,
                    //Organizer= organizers[0],
                },
                new Event(){
                    Title="Moonhaven",
                    Description="Best lazertag in the world",
                    Place="Blackpark",
                    Date=DateTime.Now.AddDays(12),
                    SpotsAvailable=23,
                    //Organizer= organizers[0],
                },
            };

            await AddRangeAsync(events);
            await AddRangeAsync(attendee);
            await SaveChangesAsync();

        }
    }
}
