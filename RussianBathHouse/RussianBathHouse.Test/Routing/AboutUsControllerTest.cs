namespace RussianBathHouse.Test.Routing
{
    using RussianBathHouse.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class AboutUsControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/AboutUs")
                   .To<AboutUsController>(c => c.Index())
                    .Which()
                    .ShouldReturn()
                    .View();

        [Fact]
        public void BannikRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/AboutUs/Bannik")
                   .To<AboutUsController>(c => c.Bannik())
                    .Which()
                    .ShouldReturn()
                    .View();

        [Fact]
        public void PhotosRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/AboutUs/Photos")
                   .To<AboutUsController>(c => c.Photos())
                    .Which()
                    .ShouldReturn()
                    .View();
    }
}
