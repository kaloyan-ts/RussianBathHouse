namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using RussianBathHouse.Services.Reservations;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly BathHouseDbContext data;
        private readonly IReservationsService reservations;

        public ReservationsController(BathHouseDbContext data, IReservationsService reservations)
        {
            this.data = data;
            this.reservations = reservations;
        }

        public IActionResult Index()
        {
            var upcomingReservationsForUser = reservations.Upcoming(this.User.Id());

            return View(upcomingReservationsForUser);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReservationAddFormModel reservationModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reservationModel);
            }

            var cabinForReservation = this.data.Cabins
                .Where(c => c.Capacity == reservationModel.NumberOfPeople
                || c.Capacity > reservationModel.NumberOfPeople)
                //&& c.Reservations.Where(r => DateTime.Compare())
                .Select(c => c.Id).First();

            //isCabinAvailable()

            var reservation = new Reservation
            {
                UserId = this.User.Id(),
                NumberOfPeople = reservationModel.NumberOfPeople,
                CabinId = cabinForReservation,
                ReservedFrom = reservationModel.ReserveFrom,
                ReservedUntill = reservationModel.ReserveFrom.AddHours(reservationModel.ReserveForHours),

            };

            this.data.Reservations.Add(reservation);
            this.data.SaveChanges();

            return RedirectToAction("ChooseServices", new { id = reservation.Id });
        }

        public IActionResult ChooseServices([FromRoute] string id)
        {
            var model = new ReservationsServicesListingModel
            {
                Services = GetReservationServices(),
                ReservationId = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ChooseServices(ReservationsServicesListingModel servicesModel)
        {

            var reservation = this.data.Reservations.FirstOrDefault(r => r.Id == servicesModel.ReservationId);

            if (reservation == null)
            {
                return BadRequest();
            }

            var chosenServices = new List<Service>();

            foreach (var service in servicesModel.Services)
            {
                if (service == null)
                {
                    continue;
                }

                chosenServices.Add(this.data.Services.First(s => s.Id == service.Id));
            }

            foreach (var service in chosenServices)
            {
                reservation.ReservationServices.Add(new ReservationService
                {
                    ServiceId = service.Id
                });
            }

            this.data.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool isCabinAvailable(int cabinId, DateTime From, DateTime Until)
        {
            var cabin = this.data.Cabins.First(c => c.Id == cabinId);

            return true;
        }

        private ServiceListViewModel[] GetReservationServices()
        {
            var services = this.data
                .Services
                .Select(c => new ServiceListViewModel
                {
                    Id = c.Id,
                    Description = c.Description
                })
                .ToArray();
            return services;
        }
    }
}

