namespace RussianBathHouse.Services.Purchases
{
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Purchases;
    using System;
    using System.Collections.Generic;

    public interface IPurchasesService
    {
        public int Add(string userId, 
            string accessoryId, 
            int quantity, 
            DateTime dateOfPurchase);

        public List<AllPurchasesViewModel> All();

        public List<AllPurchasesViewModel> AllForUser(string userId);

        public Purchase FindById(int id);

    }
}
