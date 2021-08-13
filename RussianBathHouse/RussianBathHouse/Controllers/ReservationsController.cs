namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Infrastructure;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Services.Reservations;
    using System.Collections.Generic;

    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservations;

        public ReservationsController(IReservationsService reservations)
        {
            this.reservations = reservations;
        }

        public IActionResult Index()
        {
            List<ReservationsUpcomingListModel> upcomingReservations;

            if (this.User.IsAdmin())
            {
                upcomingReservations = reservations.AllUpcoming();
            }
            else
            {
                upcomingReservations = reservations.UpcomingForUser(this.User.Id());
            }

            return View(upcomingReservations);
        }

        public IActionResult Add()
        {
            var reserved = new ReservationAddFormModel
            {
                Reserved = reservations.GetReservedDates()
            };

            return View(reserved);
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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var reservationId = reservations.Add(model.NumberOfPeople, reservationTime, this.User.Id());

            return RedirectToAction(actionName: "ChooseServices", controllerName: "Reservations", routeValues: new { id = reservationId }, fragment: reservationId);
        }

        public IActionResult ChooseServices(string id)
        {
            var model = new ReservationsServicesListingModel
            {
                Services = reservations.GetReservationServices(),
                ReservationId = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ChooseServices(ReservationsServicesListingModel servicesModel)
        {

            var reservation = reservations.FindById(servicesModel.ReservationId);

            if (reservation == null)
            {
                return BadRequest();
            }

            reservations.AddServicesToReservation(reservation.Id, servicesModel.Services);

            return RedirectToAction("Index");
        }
    }
}

