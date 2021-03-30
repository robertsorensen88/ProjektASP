using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjektASP.Data;
using ProjektASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDb(host);
            host.Run();

        }
        static void SeedDb(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EventsDbContext>();
                /*
                 if (context.Events.Any() && context.Attendees.Any() && context.Organizers.Any())
                 {
                    return;   
                 }
                 
                context.Attendees.AddRange(new List<Attendee>()
                {
                    new Attendee(){Name="Roberta", Email="roberta@gmail.com", PhoneNumber="843749937"},
                });

                context.Events.AddRange(new List<Event>()
                {
                    new Event(){Title="Iron Maiden", Description="Consert", Place="Ullevi", Date= new DateTime(2021,8,21), SpotsAvailable= 80 },
                    new Event(){Title="Metallica", Description="Consert", Place="Ullevi", Date= new DateTime(2021,6,21), SpotsAvailable= 80 },
                    new Event(){Title="Fotboll", Description="Fotbollsmatch", Place="Ullevi", Date= new DateTime(2021,3,21), SpotsAvailable= 80 },
                    new Event(){Title="SpeedWay", Description="VM", Place="Ullevi", Date= new DateTime(2021,5,21), SpotsAvailable= 80 },
                });

                context.Organizers.AddRange(new List<Organiser>()
                {
                    new Organiser(){Name="GotiT", Email="gotit@email.com", PhoneNumber="836738847"},
                });*/

                context.SaveChanges();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
