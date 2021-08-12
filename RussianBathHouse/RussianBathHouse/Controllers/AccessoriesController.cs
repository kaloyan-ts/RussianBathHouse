namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Accessories;
    using System.Threading.Tasks;
    using static WebConstants;

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

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = AdministratorRoleName)]
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


        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(string Id)
        {
            var accessory = this.accessories.FindById(Id);

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

        [Authorize(Roles = AdministratorRoleName)]
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

        [Authorize]
        public IActionResult Buy(string Id)
        {
            var accessory = accessories.FindById(Id);

            var accessoryModel = new BuyFormModel
            {
                AccessoryId = accessory.Id,
                AccessoryPrice = accessory.Price,
            };

            return View(accessoryModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buy(BuyFormModel model)
        {
            var enoughQuantity = accessories.EnoughQuantity(model.AccessoryId, model.Quantity);

            if (!enoughQuantity)
            {
                ModelState.AddModelError("Quantity", "Not enough quantity! Please check in details page the maximum amount.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            accessories.Buy(model.AccessoryId, model.Quantity);

            string userId = this.User.Id();

            await accessories.SetAddressAndPhoneNumber(userId, model.PhoneNumber, model.Address);

            return RedirectToAction("All");
        }
    }
}
