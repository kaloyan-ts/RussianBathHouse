namespace RussianBathHouse.Services.Users
{
    using System.Threading.Tasks;
    public interface IUsersService
    {
        Task<string> GetUserPhoneNumber(string id);
        Task<string> GetUserAddress(string id);
    }
}
