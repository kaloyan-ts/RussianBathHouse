namespace RussianBathHouse.Models.Purchases
{
    using System;
    public class AllPurchasesViewModel
    {
        public string UserFullName { get; set; }

        public string AccessoryName { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
