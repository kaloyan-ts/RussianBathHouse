namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Users;
    using RussianBathHouse.Services.Users;
    using System.Threading.Tasks;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        public IActionResult ChangePhoneNumber()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber(PhoneNumber model)
        {
            await users.ChangePhoneNumber(this.User.Id(), model.Data);

            var previousUrl = TempData["PreviousUrl"].ToString();
            return Redirect(previousUrl);
        }

        public IActionResult ChangeAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAddress(Address model)
        {
            await users.ChangeAddress(this.User.Id(), model.Data);

            var previousUrl = TempData["PreviousUrl"].ToString();
            return Redirect(previousUrl);
        }
    }
}
