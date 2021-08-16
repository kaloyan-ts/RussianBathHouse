namespace RussianBathHouse.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using Xunit;
    public class AboutUsControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            MyController<AboutUsController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.Index())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void BannikShouldReturnView()
        {
            MyController<AboutUsController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.Index())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PhotosShouldReturnView()
        {
            MyController<AboutUsController>
                .Instance(c => c.WithoutData())
                .Calling(c => c.Index())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .View();
        }
    }
}
