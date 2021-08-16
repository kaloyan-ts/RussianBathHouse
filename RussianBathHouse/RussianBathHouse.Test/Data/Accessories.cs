namespace RussianBathHouse.Test.Data
{
    using Microsoft.EntityFrameworkCore;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Accessories
    {

        public static BathHouseDbContext Context()
        {
            var cfg = new DbContextOptionsBuilder<BathHouseDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;

            var context = new BathHouseDbContext(cfg);

            return context;
        }


        public static Accessory GetAccessory()
        {
            return Context().Accessories.First();
        }

        public static IEnumerable<AccessoriesAllViewModel> TenAllViewModelAccessories
           => Enumerable.Range(0, 10).Select(i => new AccessoriesAllViewModel
           {

           });

        public static AccessoriesQueryModel QueryFormModel()
        {
            var query = new AccessoriesQueryModel
            {
                CurrentPage = 1,
                TotalAccessories = 10,
                Accessories = TenAllViewModelAccessories
            };

            return query;
        }

    }
}
