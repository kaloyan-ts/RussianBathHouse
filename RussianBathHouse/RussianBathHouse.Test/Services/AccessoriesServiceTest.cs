namespace RussianBathHouse.Test.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Services.Accessories;
    using System.Linq;
    using Xunit;
    public class AccessoriesServiceTest : BaseTest
    {
        private IAccessoriesService accessories => this.ServiceProvider.GetRequiredService<IAccessoriesService>();

        //Arrange
        private const string id = "test1";
        private const string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png";
        private const string name = "test";
        private const decimal price = 2;
        private const int quantityLeft = 10;
        private const string description = "description";

        [Fact]
        public void AddMethodCreatesAccessory()
        {
            //Act
            this.accessories.Add(imageUrl, name, price, quantityLeft, description);

            var accessory = this.DbContext.Accessories.FirstOrDefault();
            var accessoryCount = this.DbContext.Accessories.Count();

            //Assert
            Assert.Equal(1, accessoryCount);
            Assert.Equal(description, accessory.Description);
            Assert.Equal(name, accessory.Name);
            Assert.Equal(imageUrl, accessory.ImagePath);
            Assert.Equal(quantityLeft, accessory.QuantityLeft);
            Assert.Equal(price, accessory.Price);
        }

        [Fact]
        public void EditMethodEditsAccessory()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            accessories.Edit(id, "new description", "new", "new name", 3, 20);

            var accessory = this.DbContext.Accessories.FirstOrDefault(a => a.Id == id);

            //Assert
            Assert.Equal("new description", accessory.Description);
            Assert.Equal("new name", accessory.Name);
            Assert.Equal("new", accessory.ImagePath);
            Assert.Equal(20, accessory.QuantityLeft);
            Assert.Equal(3, accessory.Price);

        }

        [Fact]
        public void EnoughQuantityMethodReturnsTrueWhenThereIsEnoughQuantity()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            var result = accessories.EnoughQuantity(id, 5);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void EnoughQuantityMethodReturnsFalseWhenThereIsNoEnoughQuantity()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            var result = accessories.EnoughQuantity(id, 15);

            //Assert
            Assert.False(result);

        }

        [Fact]
        public void BuyMethodDecreasesQuantityCorrectly()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            accessories.Buy(id, 5);

            //Assert
            Assert.Equal(5, accessories.FindById(id).QuantityLeft);
        }

        [Fact]
        public void FindByIdMethodReturnsTheCorrectAccessory()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            var result = accessories.FindById(id);
            var expected = this.DbContext.Accessories.First(a => a.Id == id);

            //Assert
            Assert.Same(expected, result);
        }

        [Fact]
        public void RemoveMethodRemovesAccessoryFromDatabase()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            this.accessories.Remove(id);
            var accessory = this.DbContext.Accessories.FirstOrDefault(a => a.Id == id);

            //Assert
            Assert.Null(accessory);
        }

        [Fact]
        public void GetTotalPriceReturnsTheCorrectAmount()
        {
            //Arrange
            var id = this.AddAccessory();

            //Act
            var result = this.accessories.GetTotalPrice(id, 5);

            //Assert
            Assert.Equal(10, result);
        }

        private string AddAccessory()
        {
            this.DbContext.Accessories.Add(new Accessory
            {
                Id = id,
                QuantityLeft = quantityLeft,
                Description = description,
                ImagePath = imageUrl,
                Name = name,
                Price = price
            });
            this.DbContext.SaveChanges();

            return id;
        }
    }
}
