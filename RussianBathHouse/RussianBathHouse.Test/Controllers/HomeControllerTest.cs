namespace RussianBathHouse.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using RussianBathHouse.Models;
    using Xunit;
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            MyController<HomeController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.Index())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AboutUsShouldReturnView()
        {
            MyController<HomeController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.AboutUs())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ErrorShouldReturnViewWithCorrectModel()
        {
            MyController<HomeController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.Error())
                .ShouldHave()
                .ActionAttributes(at => at.CachingResponse(c => c.WithDuration(0).WithLocation(Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None).WithNoStore(true)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithModelOfType<ErrorViewModel>());
        }
    }
}
