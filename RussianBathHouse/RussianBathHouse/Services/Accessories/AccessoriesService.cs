namespace RussianBathHouse.Services.Accessories
{
    using Microsoft.AspNetCore.Identity;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Services.Users;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccessoriesService : IAccessoriesService
    {
        private readonly BathHouseDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService users;

        public AccessoriesService(BathHouseDbContext data, UserManager<ApplicationUser> userManager, IUsersService users)
        {
            this.data = data;
            this.userManager = userManager;
            this.users = users;
        }

        public string Add(string imagePath, string name, decimal price, int quantityLeft, string description)
        {
            var accessory = new Accessory
            {
                ImagePath = imagePath,
                Name = name,
                Price = price,
                QuantityLeft = quantityLeft,
                Description = description,
            };

            this.data.Accessories.Add(accessory);
            this.data.SaveChanges();

            return accessory.Id;
        }

        public AccessoriesQueryModel All(string searchTerm, AccessoriesSorting sorting, int currentPage, int accessoriesPerPage)
        {
            var accessoriesQuery = this.data.Accessories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                accessoriesQuery = accessoriesQuery.Where(c => (c.Name + " " + c.Description).ToLower().Contains(searchTerm.ToLower()));
            }

            accessoriesQuery = sorting switch
            {
                AccessoriesSorting.AlphabeticalyDescending => accessoriesQuery.OrderByDescending(c => c.Name),
                AccessoriesSorting.Price => accessoriesQuery.OrderBy(c => c.Price),
                AccessoriesSorting.PriceDescending => accessoriesQuery.OrderByDescending(c => c.Price),
                AccessoriesSorting.Alphabeticaly or _ => accessoriesQuery.OrderBy(c => c.Name)
            };

            var totalAccessories = accessoriesQuery.Count();

            var accessories = accessoriesQuery
                .Skip((currentPage - 1) * accessoriesPerPage)
                .Take(accessoriesPerPage)
                .Select(a => new AccessoriesAllViewModel
                {
                    Id = a.Id,
                    Image = a.ImagePath,
                    Name = a.Name,
                    Price = a.Price,
                })
                .ToList();

            return new AccessoriesQueryModel
            {
                Accessories = accessories,
                CurrentPage = currentPage,
                SearchTerm = searchTerm,
                Sorting = sorting,
                TotalAccessories = totalAccessories,
            };
        }

        public bool EnoughQuantity(string id, int desiredQuantity)
        {
            var accessory = FindById(id);

            if (accessory.QuantityLeft < desiredQuantity)
            {
                return false;
            }

            return true;
        }
        public void Buy(string id, int desiredQuantity)
        {
            var accessory = FindById(id);

            accessory.QuantityLeft -= desiredQuantity;

            this.data.SaveChanges();
        }

        public AccessoryDetailsViewModel Details(string id)
        {
            var accessory = FindById(id);

            var accessoryModel = new AccessoryDetailsViewModel
            {
                Id = accessory.Id,
                Name = accessory.Name,
                Description = accessory.Description,
                Image = accessory.ImagePath,
                Price = accessory.Price,
                QuantityLeft = accessory.QuantityLeft
            };

            return accessoryModel;
        }

        public void Edit(string id, string description, string imagePath, string name, decimal price, int quantity)
        {
            var accessory = FindById(id);

            accessory.Description = description;
            accessory.ImagePath = imagePath;
            accessory.Name = name;
            accessory.Price = price;
            accessory.QuantityLeft = quantity;

            this.data.SaveChanges();
        }

        public Accessory FindById(string id)
        {
            return this.data.Accessories.Find(id);
        }

        public void Remove(string id)
        {
            var accessory = this.FindById(id);

            this.data.Accessories.Remove(accessory);
            this.data.SaveChanges();
        }

        public async Task SetAddressAndPhoneNumber(string id, string phoneNumber, string address)
        {
            if (await users.GetUserPhoneNumber(id) != null)
            {
                return;
            }

            var user = await userManager.FindByIdAsync(id);

            await this.userManager.SetPhoneNumberAsync(user, phoneNumber);

            user.Address = address;

            this.data.SaveChanges();
        }
    }
}
