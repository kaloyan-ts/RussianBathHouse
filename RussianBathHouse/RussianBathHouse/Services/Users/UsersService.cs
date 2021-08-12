namespace RussianBathHouse.Services.Users
{
    using Microsoft.AspNetCore.Identity;
    using RussianBathHouse.Data.Models;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetUserAddress(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var address = user.Address;

            return address;

        }

        public async Task<string> GetUserPhoneNumber(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var phoneNumber = user.PhoneNumber;
            
            if (phoneNumber == null)
            {
                return null;
            }

            return phoneNumber;
        }
    }
}
