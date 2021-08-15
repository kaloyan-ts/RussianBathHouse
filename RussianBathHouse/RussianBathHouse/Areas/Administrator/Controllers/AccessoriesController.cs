namespace RussianBathHouse.Areas.Administrator.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Accessories;

    public class AccessoriesController : AdministratorController
    {
        private readonly IAccessoriesService accessories;
        private readonly IMapper mapper;

        public AccessoriesController(IAccessoriesService accessories, IMapper mapper)
        {
            this.accessories = accessories;
            this.mapper = mapper;
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

            var accessoryModel = this.mapper.Map<AccessoryEditFormModel>(accessory);

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
