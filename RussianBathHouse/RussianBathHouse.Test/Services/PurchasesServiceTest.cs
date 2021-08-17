namespace RussianBathHouse.Test.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Purchases;
    using System;
    using System.Linq;
    using Xunit;
    public class PurchasesServiceTest : BaseTest
    {
        private IPurchasesService purchases => this.ServiceProvider.GetRequiredService<IPurchasesService>();
        private IAccessoriesService accessories => this.ServiceProvider.GetRequiredService<IAccessoriesService>();

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

      // [Fact]
      // public void AllMethodReturnsCorrectModel()
      // {
      //     //Arrange
      //     var accID = this.accessories.Add(null, "test", 1, 0, null);
      //     AddPurchase();
      //
      //     //Act
      //     var purchases = this.purchases.All();
      //     var purchase = purchases.First();
      //     var purchaseCount = this.DbContext.Purchases.Count();
      //
      //     //Assert
      //     Assert.IsAssignableFrom<List<AllPurchasesViewModel>>(purchases);
      //     Assert.Equal(1,purchaseCount);
      //     Assert.Equal("test", purchase.AccessoryName);
      //     Assert.Equal(TestUser.Username, purchase.UserFullName);
      //     Assert.Equal(dateOfPurchase, purchase.DateOfPurchase);
      //     Assert.Equal(quantity, purchase.Quantity);
      //     Assert.Equal(totalPrice, purchase.TotalPrice);
      // }
        
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

    }
}
