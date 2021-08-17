namespace RussianBathHouse.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using RussianBathHouse.Models.Accessories;
    using Xunit;
    public class AccessoriesControlerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/Accessories")
                   .To<AccessoriesController>(c => c.Index())
                   .Which()
                   .ShouldReturn()
                   .Redirect(r => r.ToAction("All"));


        [Fact]
        public void AllRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/Accessories/All")
                   .To<AccessoriesController>(c =>
                   c.All(With.Any<AccessoriesQueryModel>()));

        [Fact]
        public void DetailsRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/Accessories/Details")
                   .To<AccessoriesController>(c => c.Details(With.Any<string>()));

        [Fact]
        public void GetBuyRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(r => r
                   .WithPath("/Accessories/Buy")
                   .WithMethod(HttpMethod.Get)
                   .WithUser())
                   .To<AccessoriesController>(c => c.Buy(With.Any<string>()))
                   .Which()
                   .ShouldHave()
                   .ActionAttributes(a => a.RestrictingForAuthorizedRequests());

        [Fact]
        public void PostBuyRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(r => r
                        .WithPath("/Accessories/Buy")
                        .WithMethod(HttpMethod.Post)
                        .WithUser())
                   .To<AccessoriesController>(c => c.Buy(With.Any<BuyFormModel>()));
                  //.Which(a => a
                  //    .ShouldHave()
                  //    .ActionAttributes(r => r.RestrictingForAuthorizedRequests()));
                   //.AndAlso()
                   //.ShouldReturn()
                   //.Redirect(r => r
                   //.ToController(nameof(PurchasesController))
                   //.ToAction("SuccessfullyAdded")));
    }
}
