namespace RussianBathHouse.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {

            using var serviceScope = app.ApplicationServices.CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<BathHouseDbContext>();

            dbContext.Database.Migrate();

            SeedCabins(dbContext);
            SeedServices(dbContext);

            return app;
        }

        private static void SeedCabins(BathHouseDbContext data)
        {
            if (data.Cabins.Any())
            {
                return;
            }

            data.Cabins.AddRange(new[]
            {
                new Cabin
                {
                    Capacity  = 3,
                    PricePerHour = 20,
                },
                new Cabin
                {
                    Capacity  = 4,
                    PricePerHour = 25,
                },
                new Cabin
                {
                    Capacity  = 5,
                    PricePerHour = 30,
                },
                new Cabin
                {
                    Capacity  = 6,
                    PricePerHour = 40,
                }
            });

            data.SaveChanges();
        }

        private static void SeedServices(BathHouseDbContext data)
        {
            if (data.Services.Any())
            {
                return;
            }

            data.Services.AddRange(new[]
            {
                new Service
                {
                     Description ="Traditional parenie by a Bannik",
                     Price = 10,
                },
                new Service
                {
                   Description ="Luxury organic body scrub",
                     Price = 20,
                },
                new Service
                {
                    Description ="Classical Russian Massage",
                     Price = 50,
                },
                new Service
                {
                    Description ="Body Scrub",
                     Price = 40,
                }
            });

            data.SaveChanges();
        }
    }
}
