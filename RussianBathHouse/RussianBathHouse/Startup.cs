namespace RussianBathHouse
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Reservations;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<BathHouseDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();


            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BathHouseDbContext>();

            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            services.AddTransient<IAccessoriesService, AccessoriesService>();
            services.AddTransient<IReservationsService, ReservationsService>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "Administrator Area",
                       pattern: "{area}/{controller}/{action}",
                       defaults: new
                       {
                           area = "",
                           controller = "Reservations",
                           action = "All"
                       });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
