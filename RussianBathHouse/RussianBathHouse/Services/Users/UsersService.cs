namespace RussianBathHouse.Services.Users
{
    using Microsoft.AspNetCore.Identity;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly BathHouseDbContext data;

        public UsersService(UserManager<ApplicationUser> userManager, BathHouseDbContext data)
        {
            this.userManager = userManager;
            this.data = data;
        }

        public async Task SetAddressAndPhoneNumber(string id, string phoneNumber, string address)
        {
            if (await this.GetUserPhoneNumber(id) != null)
            {
                return;
            }

            var user = await userManager.FindByIdAsync(id);

            await this.userManager.SetPhoneNumberAsync(user, phoneNumber);

            user.Address = address;

            this.data.SaveChanges();
        }

        public async Task ChangePhoneNumber(string id, string phoneNumber)
        {
            var user = await userManager.FindByIdAsync(id);

            user.PhoneNumber = phoneNumber;

        }

        public async Task ChangeAddress(string id, string address)
        {
            var user = await userManager.FindByIdAsync(id);

            user.Address = address;

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

        public async Task<string> GetUserFullName(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var FirstName = user.FirstName;
            var lastName = user.LastName;

            return FirstName + " " + lastName;
        }
    }
}
