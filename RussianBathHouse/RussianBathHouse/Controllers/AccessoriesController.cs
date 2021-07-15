﻿namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using System.Linq;


    public class AccessoriesController : Controller
    {
        private readonly BathHouseDbContext data;

        public AccessoriesController(BathHouseDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            return Redirect("Accessories/All");
        }

        public IActionResult All([FromQuery]AccessoriesQueryModel query)
        {
            var accessoriesQuery = this.data.Accessories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                accessoriesQuery = accessoriesQuery.Where(c => (c.Name + " " + c.Description).ToLower().Contains(query.SearchTerm.ToLower()));
            }

            accessoriesQuery = query.Sorting switch
            {
                AccessoriesSorting.AlphabeticalyDescending => accessoriesQuery.OrderByDescending(c => c.Name),
                AccessoriesSorting.Price => accessoriesQuery.OrderBy(c => c.Price),
                AccessoriesSorting.PriceDescending => accessoriesQuery.OrderByDescending(c => c.Price),
                AccessoriesSorting.Alphabeticaly or _ => accessoriesQuery.OrderBy(c => c.Name)
            };

            var totalAccessories = accessoriesQuery.Count();

            var accessories = accessoriesQuery
                 .Skip((query.CurrentPage - 1) * AccessoriesQueryModel.AccessoriesPerPage)
                .Take(AccessoriesQueryModel.AccessoriesPerPage)
                .Select(a => new AccessoriesAllViewModel
                {
                    Id = a.Id,
                    Image = a.ImagePath,
                    Name = a.Name,
                    Price = a.Price,
                })
                .ToList();


            query.TotalAccessories = totalAccessories;
            query.Accessories = accessories;

            return View(query);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AccessoryAddFormModel accessoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(accessoryModel);
            }

            var accessory = new Accessory
            {
                ImagePath = accessoryModel.ImagePath,
                Name = accessoryModel.Name,
                Price = accessoryModel.Price,
                QuantityLeft = accessoryModel.Quantity,
                Description = accessoryModel.Description
            };

            this.data.Accessories.Add(accessory);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

       
        public IActionResult Delete(string Id)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == Id);

            if (accessory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            this.data.Accessories.Remove(accessory);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Edit(string Id)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == Id);

            if (accessory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var accessoryModel = new AccessoryEditFormModel
            {
                Id = accessory.Id,
                Description = accessory.Description,
                ImagePath = accessory.ImagePath,
                Name = accessory.Name,
                Price = accessory.Price,
                Quantity = accessory.QuantityLeft
            };

            return View(accessoryModel);
        }

        [HttpPost]
        public IActionResult Edit(AccessoryEditFormModel changedAccessory)
        {
            var accessory = this.data.Accessories.FirstOrDefault(a => a.Id == changedAccessory.Id);

            if (accessory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            accessory.Description = changedAccessory.Description;
            accessory.ImagePath = changedAccessory.ImagePath;
            accessory.Name = changedAccessory.Name;
            accessory.Price = changedAccessory.Price;
            accessory.QuantityLeft = changedAccessory.Quantity;

            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Details(string Id)
        {
            var accessory = this.data.Accessories.Find(Id);

            if (accessory == null)
            {
                return BadRequest();
            }

            var accessoryModel = new AccessoryDetailsViewModel
            {
                Id = accessory.Id,
                Description = accessory.Description,
                Image = accessory.ImagePath,
                Name = accessory.Name,
                Price = accessory.Price,
                QuantityLeft = accessory.QuantityLeft
            };

            return View(accessoryModel);
        }
    }
}
