﻿namespace RussianBathHouse.Services.Accessories
{
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using System.Linq;

    public class AccessoriesService : IAccessoriesService
    {
        private readonly BathHouseDbContext data;

        public AccessoriesService(BathHouseDbContext data)
        {
            this.data = data;
        }

        public string Add(string imagePath, string name, decimal price,int quantityLeft, string description)
        {
            var accessory = new Accessory
            {
                ImagePath = imagePath,
                Name = name,
                Price = price,
                QuantityLeft = quantityLeft,
                Description = description
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
                TotalAccessories = totalAccessories
            };
        }

        public AccessoryDetailsViewModel Details(string id)
        {
            var accessory = this.data.Accessories.Find(id);

            var accessoryModel = new AccessoryDetailsViewModel
            {
                Id = accessory.Id,
                Description = accessory.Description,
                Image = accessory.ImagePath,
                Name = accessory.Name,
                Price = accessory.Price,
                QuantityLeft = accessory.QuantityLeft
            };

            return accessoryModel;
        }

        public void Edit(string id, string description, string imagePath, string name, decimal price, int quantity)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == id);

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
    }
}