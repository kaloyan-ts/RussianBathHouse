namespace RussianBathHouse.Test.Routing.Areas.Administrator
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Areas.Administrator.Controllers;
    using Xunit;
    public class ReservationsControllerTest
    {
        [Fact]
        public void ScheduleRouteShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap(r => r
                          .WithPath("/Reservations/Schedule/Administrator")
                          .WithUser(u => u.InRole("Administrator")))
                    .To<ReservationsController>(c => c.Schedule())
                    .Which()
                    .ShouldReturn()
                    .View();
    }
}
