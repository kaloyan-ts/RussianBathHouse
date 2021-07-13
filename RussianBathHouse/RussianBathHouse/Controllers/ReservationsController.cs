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
            return View(new ReservationAddFormModel
            {
                Services = this.GetReservationServices()
            });
        }

        [HttpPost]
        public IActionResult Add(ReservationAddFormModel reservationModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            var cabinForReservation = this.data.Cabins
                .Where(c => c.Capacity == reservationModel.NumberOfPeople
                || c.Capacity > reservationModel.NumberOfPeople)
                //&& c.Reservations.Where(r => DateTime.Compare())
                .Select(c => c.Id).First();

            //isCabinAvailable()

            var services = new List<Service>();

            foreach (var service in reservationModel.Services)
            {
                if (service == null)
                {
                    continue;
                }

                services.Add(this.data.Services.First(s => s.Id == service.Id));
            }


            var reservation = new Reservation
            {
                NumberOfPeople = reservationModel.NumberOfPeople,
                CabinId = cabinForReservation,
                ReservedFrom = reservationModel.ReserveFrom,
                ReservedUntill = reservationModel.ReserveFrom.AddHours(reservationModel.ReserveForHours),

            };


            foreach (var service in services)
            {
                reservation.ReservationServices.Add(new ServiceReservationListViewModel
                {
                    ServiceId = service.Id
                });
            }


            this.data.Reservations.Add(reservation);
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
                    .Select(rs => new ServiceReservaionListViewModel
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

        private ReservationsServicesListingModel[] GetReservationServices()
        {
            return this.data
                .Services
                .Select(c => new ReservationsServicesListingModel
                {
                    Id = c.Id,
                    Description = c.Description
                })
                .ToArray();
        }
    }
}

