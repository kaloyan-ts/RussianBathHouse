namespace RussianBathHouse.Test.Routing.Areas.Administrator
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Areas.Administrator.Controllers;
    using RussianBathHouse.Models.Accessories;
    using Xunit;
    public class AccessoriesControllerTest
    {
        [Fact]
        public void GetAddRouteShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap(r => r
                        .WithPath("/Accessories/Add/Administrator")
                        .WithUser(u => u.InRole("Administrator")))
                  .To<AccessoriesController>(c => c.Add())
                  .Which()
                  .ShouldReturn()
                  .View();

        [Fact]
        public void PostAddRouteShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap(r => r
                        .WithPath("/Accessories/Add/Administrator")
                        .WithUser(u => u.InRole("Administrator"))
                        .WithMethod(HttpMethod.Post))
                  .To<AccessoriesController>(c => c.Add(With.Any<AccessoryAddFormModel>()));

        [Fact]
        public void GetEditRouteShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap(r => r
                        .WithPath("/Accessories/Edit/Administrator")
                        .WithUser(u => u.InRole("Administrator"))
                        .WithMethod(HttpMethod.Get))
                  .To<AccessoriesController>(c => c.Edit(With.Any<string>()));

        [Fact]
        public void PostEditRouteShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap(r => r
                        .WithPath("/Accessories/Edit/Administrator")
                        .WithUser(u => u.InRole("Administrator"))
                        .WithMethod(HttpMethod.Post))
                  .To<AccessoriesController>(c => c.Edit(With.Any<AccessoryEditFormModel>()));

        [Fact]
        public void DeleteRouteShouldBeMapped()
      => MyRouting
          .Configuration()
          .ShouldMap(r => r
                .WithPath("/Accessories/Delete/Administrator")
                .WithUser(u => u.InRole("Administrator")))
          .To<AccessoriesController>(c => c.Delete(With.Any<string>()));
    }
}
