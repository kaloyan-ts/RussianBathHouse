namespace RussianBathHouse.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static Areas.Administrator.AdminConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {

            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedCabins(services);
            SeedServices(services);
            SeedAdministrator(services);

            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<BathHouseDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCabins(IServiceProvider services)
        {
            var data = services.GetService<BathHouseDbContext>();


            

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

        private static void SeedServices(IServiceProvider services)
        {
            var data = services.GetService<BathHouseDbContext>();

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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRole))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdminRole };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@crs.com";
                    const string adminPassword = "admin12";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "Adminov"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
