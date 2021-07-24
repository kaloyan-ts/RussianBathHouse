namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReservationsController : Controller
    {
        private readonly BathHouseDbContext data;

        public ReservationsController(BathHouseDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            return View();
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

            var choseServices = new List<Service>();

            foreach (var service in servicesModel.Services)
            {
                if (service == null)
                {
                    continue;
                }

                choseServices.Add(this.data.Services.First(s => s.Id == service.Id));
            }

            foreach (var service in choseServices)
            {
                reservation.ReservationServices.Add(new ReservationService
                {
                    ServiceId = service.Id
                });
            }

            this.data.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Upcoming()
        {
            var reservations = this.data.Reservations
                .Select(a => new ReservationsUpcomingListModel
                {
                    CabinNumber = a.CabinId,
                    NumberOfPeople = a.NumberOfPeople,
                    ReservedFrom = a.ReservedFrom,
                    ReservationServices = a.ReservationServices
                    .Select(rs => new ServiceListViewModel
                    {
                        Description = rs.Service.Description
                    }),
                })
                .ToList();

            return View(reservations);
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

