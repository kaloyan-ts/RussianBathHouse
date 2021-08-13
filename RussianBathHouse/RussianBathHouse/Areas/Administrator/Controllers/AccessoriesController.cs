namespace RussianBathHouse.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Accessories;

    public class AccessoriesController : AdministratorController
    {
        private readonly IAccessoriesService accessories;

        public AccessoriesController(IAccessoriesService accessories)
        {
            this.accessories = accessories;
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

            accessories.Add(accessoryModel.ImagePath,
                accessoryModel.Name,
                accessoryModel.Price,
                accessoryModel.Quantity,
                accessoryModel.Description);

            return RedirectToAction(actionName:"All",controllerName:"Accessories");
        }

        public IActionResult Edit(string Id)
        {
            var accessory = accessories.FindById(Id);

            if (accessory == null)
            {
                return BadRequest();
            }

            var accessoryModel = new AccessoryEditFormModel
            {
                Id = accessory.Id,
                Name = accessory.Name,
                Description = accessory.Description,
                ImagePath = accessory.ImagePath,
                Price = accessory.Price,
                Quantity = accessory.QuantityLeft
            };

            return View(accessoryModel);
        }

        [HttpPost]
        public IActionResult Edit(AccessoryEditFormModel changedAccessory)
        {
            accessories.Edit(changedAccessory.Id,
                changedAccessory.Description,
                changedAccessory.ImagePath,
                changedAccessory.Name,
                changedAccessory.Price,
                changedAccessory.Quantity);

            return RedirectToAction(actionName: "All", controllerName: "Accessories");
        }
        public IActionResult Delete(string Id)
        {
            accessories.Remove(Id);

            return RedirectToAction(actionName: "All", controllerName: "Accessories");
        }

    }
}
