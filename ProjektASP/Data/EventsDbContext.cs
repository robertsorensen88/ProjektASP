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



        public async Task ResetDb(UserManager<Attendee> userManager, RoleManager<IdentityRole> roleManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            await roleManager.CreateAsync(new IdentityRole("Attendee"));
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Organizer"));

            Attendee attendee = new Attendee()
            {
                UserName = "Kalle",
                Email = "Kalle@gmail.com",
                PhoneNumber = "0734-431267"
            };

            await userManager.CreateAsync(attendee, "Kalle123!");
            await userManager.AddToRoleAsync(attendee, "Attendee");

            Attendee admin = new Attendee()
            {
                UserName = "Admin",
                Email = "admin@admin.com"
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");

            Attendee[] organizers = new Attendee[] {
                new Attendee(){
                    UserName = "Org",
                    Email = "Org@org.com",
                },
                new Attendee(){
                    UserName = "Org1",
                    Email = "org1@org1.com",
                },
            };
            foreach (var org in organizers)
            {
                await userManager.CreateAsync(org, "Org123!");
                await userManager.AddToRoleAsync(org, "Organizer");
            }


            Event[] events = new Event[] {
                new Event(){
                    Title="Summer camp",
                    Description="Have a fun time chilling in the sun",
                    Place="Colorado springs",
                    Date=DateTime.Now.AddDays(34),
                    SpotsAvailable=234,
                    
                },
                new Event(){
                    Title="Moonhaven",
                    Description="Best lazertag in the world",
                    Place="Blackpark",
                    Date=DateTime.Now.AddDays(12),
                    SpotsAvailable=23,
                    
                },
            };

            await AddRangeAsync(events);
            await SaveChangesAsync();

        }
    }
}
