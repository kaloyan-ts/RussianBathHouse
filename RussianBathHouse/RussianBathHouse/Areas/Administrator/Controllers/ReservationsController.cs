namespace RussianBathHouse.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RussianBathHouse.Services.Reservations;

    public class ReservationsController : AdministratorController
    {
        private readonly IReservationsService reservations;

        public ReservationsController(IReservationsService reservations)
        {
            this.reservations = reservations;
        }

        public IActionResult Schedule()
        {
            var allReservations = reservations.GetReservedDates();

            return View(allReservations);
        }
    }
}
