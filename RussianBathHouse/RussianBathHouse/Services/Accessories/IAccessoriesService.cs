﻿using RussianBathHouse.Data.Models;
using RussianBathHouse.Models.Accessories;

namespace RussianBathHouse.Services.Accessories
{
    public interface IAccessoriesService
    {
        string Add(string imagePath, string name, decimal price, int quantity, string description);

        AccessoriesQueryModel All(
                string searchTerm,
                AccessoriesSorting sorting,
                int currentPage,
                int accessoriesPerPage);

        Accessory FindById(string id);

        void Remove(string id);

        AccessoryDetailsViewModel Details(string id);

        void Edit(string id,
                string description,
                string imagePath,
                string name,
                decimal price,
                int quantityLeft);    
    }
}