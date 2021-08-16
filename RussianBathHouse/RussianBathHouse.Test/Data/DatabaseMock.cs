using Microsoft.EntityFrameworkCore;
using RussianBathHouse.Data;
using System;

namespace RussianBathHouse.Test.Data
{
    public static class DatabaseMock
    {
        public static BathHouseDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<BathHouseDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                return new BathHouseDbContext(dbContextOptions);
            }
        }
    }
}
