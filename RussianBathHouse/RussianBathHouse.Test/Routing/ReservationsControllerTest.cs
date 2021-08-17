namespace RussianBathHouse.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using RussianBathHouse.Models.Reservations;
    using Xunit;
    public class ReservationsControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap(r => r
                .WithPath("/Reservations")
                .WithMethod(HttpMethod.Get)
                .WithUser())
           .To<ReservationsController>(c => c.Index());

        [Fact]
        public void GetAddRouteShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap(r => r
                .WithPath("/Reservations/Add")
                .WithMethod(HttpMethod.Get)
                .WithUser())
           .To<ReservationsController>(c => c.Add());

        [Fact]
        public void PostAddRouteShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap(r => r
                .WithPath("/Reservations/Add")
                .WithMethod(HttpMethod.Post)
                .WithUser())
           .To<ReservationsController>(c => c.Add(With.Any<ReservationAddFormModel>()));

        [Fact]
        public void GetChooseServicesRouteShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap(r => r
                .WithPath("/Reservations/ChooseServices")
                .WithMethod(HttpMethod.Get)
                .WithUser())
           .To<ReservationsController>(c => c.ChooseServices(With.Any<string>()));

        [Fact]
        public void PostChooseServicesRouteShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap(r => r
                .WithPath("/Reservations/ChooseServices")
                .WithMethod(HttpMethod.Post)
                .WithUser())
           .To<ReservationsController>(c => c.ChooseServices(With.Any< ReservationsServicesListingModel>()));
    }

}
