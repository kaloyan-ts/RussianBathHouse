namespace RussianBathHouse.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Purchases;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Purchases;
    using System.Collections.Generic;

    [Authorize]
    public class PurchasesController : Controller
    {
        private readonly IPurchasesService purchases;
        private readonly IAccessoriesService accessories;

        public PurchasesController(IPurchasesService purchases, IAccessoriesService accessories)
        {
            this.purchases = purchases;
            this.accessories = accessories;
        }

        public IActionResult SuccessfullyAdded(int purchaseId)
        {
            var purchase = purchases.FindById(purchaseId);

            var model = new SuccessfullyAddedPurchaseViewModel
            {
                AccessoryName = accessories.FindById(purchase.AccessoryId).Name,
                Quantity = purchase.Quantity,
                TotalPrice = purchase.TotalPrice
            };

            return View(model);
        }

        public IActionResult All()
        {
            List<AllPurchasesViewModel> purchases;

            if (this.User.IsAdmin())
            {
                purchases = this.purchases.All();
            }
            else
            {
                purchases = this.purchases.AllForUser(this.User.Id());
            }

            return View(purchases);
        }

    }
}
