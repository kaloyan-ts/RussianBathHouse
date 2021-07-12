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

       
        public IActionResult Delete(string Id)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == Id);

            if (accessory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            this.data.Accessories.Remove(accessory);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Edit(string Id)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == Id);

            if (accessory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var accessoryModel = new AccessoryEditFormModel
            {
                Id = accessory.Id,
                Description = accessory.Description,
                ImagePath = accessory.ImagePath,
                Name = accessory.Name,
                Price = accessory.Price,
                Quantity = accessory.QuantityLeft
            };

            return View(accessoryModel);
        }

        [HttpPost]
        public IActionResult Edit(AccessoryEditFormModel changedAccessory)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == changedAccessory.Id);

            if (accessory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            accessory.Description = changedAccessory.Description;
            accessory.ImagePath = changedAccessory.ImagePath;
            accessory.Name = changedAccessory.Name;
            accessory.Price = changedAccessory.Price;
            accessory.QuantityLeft = changedAccessory.Quantity;

            this.data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}
