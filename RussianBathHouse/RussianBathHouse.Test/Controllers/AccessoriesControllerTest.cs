namespace RussianBathHouse.Test.Controllers
{
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Test.Data;
    using System;
    using System.Linq;
    using Xunit;

    using static Data.Accessories;
    public class AccessoriesControllerTest : BaseTest
    {
        private IAccessoriesService accessories => this.ServiceProvider.GetRequiredService<IAccessoriesService>();

        [Fact]
        public void IndexShouldReturnRedirectToAll()
        {
            MyController<AccessoriesController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.Index())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            MyController<AccessoriesController>
                .Instance()
                .Calling(c => c.All(QueryFormModel()))
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .View(v => v.WithModelOfType<AccessoriesQueryModel>());
        }

       //[Fact]
       //public void GetDetailsShouldReturnView()
       //{
       //
       //    var accessory = this.DbContext.Accessories.Add(new Accessory
       //    {
       //        Id = Guid.NewGuid().ToString(),
       //        Description = "description",
       //        Name = "name",
       //        Price = 2,
       //        QuantityLeft = 10
       //    });
       //
       //    this.DbContext.SaveChanges();
       //
       //    //Act
       //    MyController<AccessoriesController>
       //        .Instance(i => i.WithData(accessory))
       //        .Calling(c => c.Details(accessory.Entity.Id))
       //        .ShouldHave()
       //        .NoActionAttributes()
       //        .AndAlso()
       //        .ShouldReturn()
       //        .View(c => c.WithModelOfType<AccessoryDetailsViewModel>());
       //}

        [Fact]
        public void GetBuyActionShouldReturnViewWithCorrectModel()
        {

        }
    }
}
