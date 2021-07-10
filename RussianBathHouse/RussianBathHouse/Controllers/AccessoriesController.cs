namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using System.Linq;


    public class AccessoriesController : Controller
    {
        private readonly BathHouseDbContext data;

        public AccessoriesController(BathHouseDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            return Redirect("Accessories/All");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AccessoryAddFormModel accessoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(accessoryModel);
            }

            var accessory = new Accessory
            {
                ImagePath = accessoryModel.ImagePath,
                Name = accessoryModel.Name,
                Price = accessoryModel.Price,
                QuantityLeft = accessoryModel.Quantity
            };

            this.data.Accessories.Add(accessory);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var accessories = this.data.Accessories
                .Select(a => new AccessoriesAllViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Image = a.ImagePath,
                    Name = a.Name,
                    Price = a.Price,
                    QuantityLeft = a.QuantityLeft
                })
                .ToList();

            return View(accessories);
        }

        public IActionResult Delete(string Id)
        {
            var accessory = this.data.Accessories.Find(Id);

            this.data.Accessories.Remove(accessory);
            this.data.SaveChanges();

            return Redirect("All");
        }
    }
}
