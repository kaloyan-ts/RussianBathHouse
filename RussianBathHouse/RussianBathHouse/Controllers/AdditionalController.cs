namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Users;
    using RussianBathHouse.Services.Users;

    public class AdditionalController : Controller
    {
        private readonly IUsersService users;

        public AdditionalController(IUsersService users)
        {
            this.users = users;
        }

        public IActionResult ChangePhoneNumber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePhoneNumber(PhoneNumber text)
        {
            users.ChangePhoneNumber(this.User.Id(), text.PhoneNumberText);

            return View();
        }

        public IActionResult ChangeAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeAddress(string Address)
        {
            return View();
        }
    }
}
