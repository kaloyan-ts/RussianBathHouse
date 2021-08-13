namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Users;
    using System.Threading.Tasks;

    public class AccessoriesController : Controller
    {
        private readonly IAccessoriesService accessories;
        private readonly IUsersService users;

        public AccessoriesController(IAccessoriesService accessories, IUsersService users)
        {
            this.accessories = accessories;
            this.users = users;
        }

        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        public IActionResult All([FromQuery] AccessoriesQueryModel query)
        {
            query = accessories.All(query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AccessoriesQueryModel.AccessoriesPerPage);

            return View(query);
        }

        public IActionResult Details(string Id)
        {
            var accessory = accessories.Details(Id);

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

            await users.SetAddressAndPhoneNumber(userId, model.PhoneNumber, model.Address);

            return RedirectToAction("All");
        }
    }
}
