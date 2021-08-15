namespace RussianBathHouse.Services.Accessories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using System.Linq;

    public class AccessoriesService : IAccessoriesService
    {
        private readonly BathHouseDbContext data;
        private readonly IMapper mapper;

        public AccessoriesService(BathHouseDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
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
                .ProjectTo<AccessoriesAllViewModel>(this.mapper.ConfigurationProvider)
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

            var accessoryModel = this.mapper.Map<AccessoryDetailsViewModel>(accessory);

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

        public decimal GetTotalPrice(string accessoryId, int Quantity)
        {
            var accessory = FindById(accessoryId);

            var totalPrice = accessory.Price * Quantity;

            return totalPrice;
        }
    }
}
