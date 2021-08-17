namespace RussianBathHouse.Test.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Purchases;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Purchases;
    using RussianBathHouse.Services.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    public class PurchasesServiceTest : BaseTest
    {
        private IPurchasesService purchases => this.ServiceProvider.GetRequiredService<IPurchasesService>();
        private IAccessoriesService accessories => this.ServiceProvider.GetRequiredService<IAccessoriesService>();
        private IUsersService users
            => this.ServiceProvider.GetRequiredService<IUsersService>();

        private const int id = 1;
        private const string accessoryId = "test1";
        private DateTime dateOfPurchase = DateTime.UtcNow;
        private const int quantity = 5;
        private const decimal totalPrice = 5;

        [Fact]
        public void FindByIdMethodReturnsTheCorrectPurchase()
        {
            //Arrange
            var id = this.AddPurchase();

            //Act
            var result = purchases.FindById(id);
            var expected = this.DbContext.Purchases.FirstOrDefault(a => a.Id == id);

            //Assert
            Assert.Same(expected, result);
        }

        [Fact]
        public void AddMethodCreatesPurchase()
        {
            //Arrange
            var accID = this.accessories.Add(null, null, 1, 0, null);

            //Act
            this.purchases.Add(TestUser.Identifier, accID, quantity, dateOfPurchase);

            var purchase = this.DbContext.Purchases.FirstOrDefault();
            var purchaseCount = this.DbContext.Purchases.Count();

            //Assert
            Assert.Equal(1, purchaseCount);
            Assert.Equal(TestUser.Identifier, purchase.UserId);
            Assert.Equal(accID, purchase.AccessoryId);
            Assert.Equal(dateOfPurchase, purchase.DateOfPurchase);
            Assert.Equal(quantity, purchase.Quantity);
            Assert.Equal(totalPrice, purchase.TotalPrice);
        }

        [Fact]
        public void AllMethodReturnsCorrectModel()
        {
            //Arrange
            var firstPurchaseId = AddPurchase();
            var secondPurchaseId = AddAnotherPurchase();
            var accessoryId = AddAccessory();
            AddUsers();

            var firstPurchase = this.purchases.FindById(firstPurchaseId);
            firstPurchase.AccessoryId = accessoryId;

            var secondPurchase = this.purchases.FindById(secondPurchaseId);
            secondPurchase.AccessoryId = accessoryId;

            this.DbContext.SaveChanges();
            //Act
            var result = this.purchases.All();
            var purchase = result.First();
            var purchaseCount = result.Count();

            //Assert
            Assert.IsAssignableFrom<List<AllPurchasesViewModel>>(result);
            Assert.Equal(2, purchaseCount);
            Assert.Equal("test", purchase.AccessoryName);
            Assert.Equal("Ivan Ivanov2", purchase.UserFullName);
            Assert.Equal(dateOfPurchase.AddDays(1), purchase.DateOfPurchase);
            Assert.Equal(quantity, purchase.Quantity);
            Assert.Equal(totalPrice, purchase.TotalPrice);
        }

        [Fact]
        public void AllForUserShouldReturnCorrectDataAndModel()
        {
            //Arrange
            var firstPurchaseId = AddPurchase();
            var secondPurchaseId = AddAnotherPurchase();
            var accessoryId = AddAccessory();
            AddUsers();

            var firstPurchase = this.purchases.FindById(firstPurchaseId);
            firstPurchase.AccessoryId = accessoryId;

            var secondPurchase = this.purchases.FindById(secondPurchaseId);
            secondPurchase.AccessoryId = accessoryId;

            this.DbContext.SaveChanges();
            //Act
            var result = this.purchases.AllForUser(TestUser.Identifier);
            var purchase = result.First();
            var purchaseCount = result.Count();

            //Assert
            Assert.IsAssignableFrom<List<AllPurchasesViewModel>>(result);
            Assert.Equal(1, purchaseCount);
            Assert.Equal("test", purchase.AccessoryName);
            Assert.Equal("Ivan Ivanov", purchase.UserFullName);
            Assert.Equal(dateOfPurchase, purchase.DateOfPurchase);
            Assert.Equal(quantity, purchase.Quantity);
            Assert.Equal(totalPrice, purchase.TotalPrice);
        }

        private int AddPurchase()
        {
            this.DbContext.Purchases.Add(new Purchase
            {
                Id = id,
                AccessoryId = accessoryId,
                DateOfPurchase = dateOfPurchase,
                Quantity = quantity,
                TotalPrice = totalPrice,
                UserId = TestUser.Identifier
            });

            this.DbContext.SaveChanges();

            return id;
        }

        private int AddAnotherPurchase()
        {
            this.DbContext.Purchases.Add(new Purchase
            {
                Id = 2,
                AccessoryId = accessoryId,
                DateOfPurchase = dateOfPurchase.AddDays(1),
                Quantity = quantity,
                TotalPrice = totalPrice,
                UserId = "userId"
            });

            this.DbContext.SaveChanges();

            return id;
        }

        public string AddUsers()
        {
            const string firstName = "Ivan";
            const string lastName = "Ivanov";

            this.DbContext.Users.Add(new ApplicationUser
            {
                Id = TestUser.Identifier,
                FirstName = firstName,
                LastName = lastName
            });

            this.DbContext.Users.Add(new ApplicationUser
            {
                Id = "userId",
                FirstName = firstName,
                LastName = lastName + "2"
            });

            this.DbContext.SaveChanges();

            return TestUser.Identifier;
        }
        private string AddAccessory()
        {
            const string id = "test1";
            const string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png";
            const string name = "test";
            const decimal price = 2;
            const int quantityLeft = 10;
            const string description = "description";

            this.DbContext.Accessories.Add(new Accessory
            {
                Id = id,
                QuantityLeft = quantityLeft,
                Description = description,
                ImagePath = imageUrl,
                Name = name,
                Price = price,
            });
            this.DbContext.SaveChanges();

            return id;
        }

    }
}
