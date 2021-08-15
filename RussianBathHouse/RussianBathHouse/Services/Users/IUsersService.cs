namespace RussianBathHouse.Services.Users
{
    using System.Threading.Tasks;
    public interface IUsersService
    {
        Task SetAddressAndPhoneNumber(string id, string phoneNumber, string address);

        Task<string> GetUserPhoneNumber(string id);

        Task<string> GetUserAddress(string id);

        Task ChangePhoneNumber(string id, string phoneNumber);

        Task ChangeAddress(string id, string address);

        Task<string> GetUserFullName(string id);
    }
}
