namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Accessories;


    public class AccessoriesController : Controller
    {
        private readonly IAccessoriesService accessories;

        public AccessoriesController(IAccessoriesService accessories)
        {

            this.accessories = accessories;
        }



        public IActionResult Index()
        {
            return Redirect("Accessories/All");
        }

        public IActionResult All([FromQuery] AccessoriesQueryModel query)
        {
            query = accessories.All(query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AccessoriesQueryModel.AccessoriesPerPage);

            return View(query);
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

            return RedirectToAction("All");
        }


        public IActionResult Delete(string Id)
        {
            this.accessories.Remove(Id);

            return RedirectToAction("All");
        }

        public IActionResult Edit(string Id)
        {
            var accessory = this.accessories.FindById(Id);

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
            this.accessories.Edit(changedAccessory.Id,
                changedAccessory.Description,
                changedAccessory.ImagePath,
                changedAccessory.Name,
                changedAccessory.Price,
                changedAccessory.Quantity);

            return RedirectToAction("All");
        }

        public IActionResult Details(string Id)
        {
            var accessory = this.accessories.Details(Id);

            return View(accessory);
        }
    }
}
