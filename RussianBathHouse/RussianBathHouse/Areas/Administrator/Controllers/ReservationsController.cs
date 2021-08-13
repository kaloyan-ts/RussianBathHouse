namespace RussianBathHouse.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Data;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Services.Reservations;
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

        public IActionResult Schedule()
        {
            var allReservations = reservations.GetReservedDates();

            return View(allReservations);
        }
    }
}
