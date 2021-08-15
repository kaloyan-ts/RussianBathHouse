namespace RussianBathHouse.Services.Purchases
{
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Purchases;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PurchasesService : IPurchasesService
    {
        private readonly BathHouseDbContext data;
        private readonly IAccessoriesService accessories;
        private readonly IUsersService users;

        public PurchasesService(BathHouseDbContext data, IAccessoriesService accessories, IUsersService users)
        {
            this.data = data;
            this.accessories = accessories;
            this.users = users;
        }

        public Purchase FindById(int id)
        {
            var purchase = data.Purchases.FirstOrDefault(p => p.Id == id);

            return purchase;
        }

        public int Add(string userId, string accessoryId, int quantity, DateTime dateOfPurchase)
        {
            var totalPrice = accessories.GetTotalPrice(accessoryId, quantity);

            var purchase = new Purchase
            {
                AccessoryId = accessoryId,
                UserId = userId,
                DateOfPurchase = dateOfPurchase,
                Quantity = quantity,
                TotalPrice = totalPrice,
            };

            this.data.Purchases.Add(purchase);
            this.data.SaveChanges();

            return purchase.Id;
        }

        public List<AllPurchasesViewModel> All()
        {
            var purchases = this.data.Purchases
                   .Select(p => new AllPurchasesViewModel
                   {
                       AccessoryName = accessories.FindById(p.AccessoryId).Name,
                       UserFullName = users.GetUserFullName(p.UserId).Result,
                       DateOfPurchase = p.DateOfPurchase,
                       Quantity =p.Quantity,
                       TotalPrice = p.TotalPrice
                   })
                   .OrderByDescending(p => p.DateOfPurchase)
                   .ToList();

            return purchases;
        }

        public List<AllPurchasesViewModel> AllForUser(string userId)
        {
            var purchases = this.data.Purchases
                .Where(p => p.UserId == userId).
                Select(p => new AllPurchasesViewModel
                {
                    AccessoryName = accessories.FindById(p.AccessoryId).Name,
                    UserFullName = users.GetUserFullName(p.UserId).Result,
                    DateOfPurchase = p.DateOfPurchase,
                    Quantity = p.Quantity,
                    TotalPrice = p.TotalPrice
                })
                .OrderByDescending(p => p.DateOfPurchase)
                .ToList();

            return purchases;
        }


    }
}
