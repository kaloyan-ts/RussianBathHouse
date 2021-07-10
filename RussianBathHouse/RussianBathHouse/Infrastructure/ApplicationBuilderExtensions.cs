namespace RussianBathHouse.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {

            using var serviceScope = app.ApplicationServices.CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<BathHouseDbContext>();

            dbContext.Database.Migrate();

            SeedCabins(dbContext);

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
    }
}
