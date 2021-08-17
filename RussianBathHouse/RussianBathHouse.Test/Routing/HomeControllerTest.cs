namespace RussianBathHouse.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using Xunit;


    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void AboutUsRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/AboutUs")
                .To<HomeController>(c => c.AboutUs());

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
