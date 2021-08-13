namespace RussianBathHouse.Services.Users
{
    using System.Threading.Tasks;
    public interface IUsersService
    {
        Task SetAddressAndPhoneNumber(string id, string phoneNumber, string address);

        Task<string> GetUserPhoneNumber(string id);
        Task<string> GetUserAddress(string id);

        Task<string> GetUserFullName(string id);
    }
}
