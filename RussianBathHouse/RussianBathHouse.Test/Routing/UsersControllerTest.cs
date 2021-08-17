namespace RussianBathHouse.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Controllers;
    using RussianBathHouse.Models.Users;
    using Xunit;
    public class UsersControllerTest
    {
        [Fact]
        public void GetChangePhoneNumberRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(r => r
                        .WithPath("/Users/ChangePhoneNumber")
                        .WithUser()
                        .WithMethod(HttpMethod.Get))
                   .To<UsersController>(c => c.ChangePhoneNumber())
                    .Which()
                    .ShouldReturn()
                    .View();

        [Fact]
        public void PostChangePhoneNumberRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(r => r
                        .WithPath("/Users/ChangePhoneNumber")
                        .WithUser()
                        .WithMethod(HttpMethod.Post))
                 .To<UsersController>(c => c.ChangePhoneNumber(With.Any<PhoneNumber>()));


        [Fact]
        public void GetChangeAddressRouteShouldBeMapped()
               => MyRouting
                  .Configuration()
                  .ShouldMap(r => r
                        .WithPath("/Users/ChangeAddress")
                        .WithUser()
                        .WithMethod(HttpMethod.Get))
                  .To<UsersController>(c => c.ChangeAddress())
                   .Which()
                   .ShouldReturn()
                   .View();

        [Fact]
        public void PostChangeAddressRouteShouldBeMapped()
               => MyRouting
                  .Configuration()
                  .ShouldMap(r => r
                        .WithPath("/Users/ChangeAddress")
                        .WithUser()
                        .WithMethod(HttpMethod.Post))
                  .To<UsersController>(c => c.ChangeAddress(With.Any<Address>()));
    }
}
