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
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

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
            var upcomingReservations = new List<ReservationsUpcomingListModel>();

            if (this.User.IsAdmin())
            {
                upcomingReservations = reservations.All();
            }
            else
            {
                upcomingReservations = reservations.Upcoming(this.User.Id());
            }

            return View(upcomingReservations);
        }

        public IActionResult Add()
        {
            var reservations = this.data.Reservations.Select(r => new ReservedDayAndHoursViewModel
            {
                Date = r.ReservedFrom,
            });

            var model = new ReservationAddFormModel
            {
                Reserved = reservations
            };

            return View(model);
        }
        public void SelectDate(string id)
        {
            TempData["SelectedDateId"] = id;
        }

        [HttpPost]
        public IActionResult Add(ReservationAddFormModel model)
        {

            var dateAndTimeId = TempData.Peek("SelectedDateId") as string;
            var reservationTime = reservations.GetDateTimeOfReservation(dateAndTimeId);

            var reservation = new Reservation
            {
                NumberOfPeople = model.NumberOfPeople,
                ReservedFrom = reservationTime,
                UserId = this.User.Id(),
                CabinId = 1,
            };

            this.data.Reservations.Add(reservation);
            this.data.SaveChanges();

            return RedirectToAction(actionName: "ChooseServices", controllerName: "Reservations", routeValues: new { id = reservation.Id }, fragment: reservation.Id);
        }

        public IActionResult ChooseServices(string id)
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

