namespace RussianBathHouse.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using Xunit;
    public class PurchasesControllerTest
    {
        [Fact]
        public void SuccessfullyAddedRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(r => r
                        .WithPath("/Purchases/SuccessfullyAdded")
                        .WithMethod(HttpMethod.Get)
                        .WithUser())
                   .To<PurchasesController>(c => c.SuccessfullyAdded(With.Any<int>()));

        [Fact]
        public void AllRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(r => r
                        .WithPath("/Purchases/All")
                        .WithMethod(HttpMethod.Get)
                        .WithUser())
                   .To<PurchasesController>(c => c.All());
    }
}
