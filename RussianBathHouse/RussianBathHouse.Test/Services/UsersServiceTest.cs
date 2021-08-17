namespace RussianBathHouse.Test.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Services.Users;
    using Xunit;
    public class UsersServiceTest : BaseTest
    {
        private IUsersService users =>
            this.ServiceProvider.GetRequiredService<IUsersService>();

        private const string address = "address";
        private const string phoneNumber = "phoneNumber";
        private const string firstName = "Ivan";
        private const string lastName = "Ivanov";


        [Fact]
        public void GetUserFullNameShouldReturnCorrectFullName()
        {
            //Arrange
            AddUser();

            //Act
            var result = users.GetUserFullName(TestUser.Identifier).Result;

            //Assert
            Assert.Equal(firstName + " " + lastName, result);
        }

        [Fact]
        public void GetUserPhoneNumberShouldReturnCorrectPhoneNumber()
        {
            //Arrange
            AddUser();

            //Act
            var result = users.GetUserPhoneNumber(TestUser.Identifier).Result;

            //Assert
            Assert.Equal(phoneNumber, result);
        }

        [Fact]
        public void GetUserAddressShouldReturnCorrectAddress()
        {
            //Arrange
            AddUser();

            //Act
            var result = users.GetUserPhoneNumber(TestUser.Identifier).Result;

            //Assert
            Assert.Equal(phoneNumber, result);
        }

        [Fact]
        public void SetPhoneNUmberAndAddressShouldSetTheCorrectData()
        {
            //Arrange
            var id = AddUserWithoutPhoneNumberAndAddress();

            //Act
            users.SetAddressAndPhoneNumber(id,phoneNumber,address);
            var addressResult = users.GetUserAddress(id).Result;
            var phoneNumberResult = users.GetUserPhoneNumber(id).Result;

            //Assert
            Assert.Equal(phoneNumber, phoneNumberResult);
            Assert.Equal(address, addressResult);
        }

        [Fact]
        public void ChangePhoneNumberShouldChangeTheCorrectData()
        {
            //Arrange
            var id = AddUser();
            var newPhoneNumber = "new phone number";

            //Act
            users.ChangePhoneNumber(id, newPhoneNumber);
            var phoneNumberResult = users.GetUserPhoneNumber(id).Result;

            //Assert
            Assert.Equal(newPhoneNumber, phoneNumberResult);
        }

        [Fact]
        public void ChangeAddressShouldChangeTheCorrectData()
        {
            //Arrange
            var id = AddUser();
            var newAddress = "new address";

            //Act
            users.ChangeAddress(id, newAddress);
            var phoneNumberResult = users.GetUserAddress(id).Result;

            //Assert
            Assert.Equal(newAddress, phoneNumberResult);
        }


        public string AddUser()
        {
            this.DbContext.Users.Add(new ApplicationUser
            {
                Id = TestUser.Identifier,
                Address = address,
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName
            });

            this.DbContext.SaveChanges();

            return TestUser.Identifier;
        }

        private string AddUserWithoutPhoneNumberAndAddress()
        {
            this.DbContext.Users.Add(new ApplicationUser
            {
                Id = TestUser.Identifier,
                FirstName = firstName,
                LastName = lastName
            });

            this.DbContext.SaveChanges();

            return TestUser.Identifier;
        }
    }
}
