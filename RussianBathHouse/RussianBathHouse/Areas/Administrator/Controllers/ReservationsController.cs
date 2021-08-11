namespace RussianBathHouse.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Services.Reservations;
    using System;
    using System.Linq;


    public class ReservationsController : AdministratorController
    {
        private readonly IReservationsService reservations;
        private readonly BathHouseDbContext data;

        public ReservationsController(IReservationsService reservations, BathHouseDbContext data)
        {
            this.reservations = reservations;
            this.data = data;
        }

        public IActionResult All()
        {
            var allReservations = reservations.All();

            return View(allReservations);
        }
        public IActionResult Schedule()
        {

            var allReservations = data.Reservations.Select(r => new ReservedDayAndHoursViewModel
            {
                Date = r.ReservedFrom
            })
                .ToList(); ;

            return View(allReservations);
        }
    }
}
