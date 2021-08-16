namespace RussianBathHouse.Test
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RussianBathHouse.Data;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Purchases;
    using RussianBathHouse.Services.Reservations;
    using RussianBathHouse.Services.Users;
    using System;
    using AutoMapper;


    public abstract class BaseTest
    {

        protected BaseTest()
        {

            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<BathHouseDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected BathHouseDbContext DbContext { get; set; }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BathHouseDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

           

            services.AddDbContext<BathHouseDbContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IAccessoriesService, AccessoriesService>();
            services.AddTransient<IReservationsService, ReservationsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPurchasesService, PurchasesService>();

            return services;
        }
    }
}
