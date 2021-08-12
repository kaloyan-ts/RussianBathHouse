namespace RussianBathHouse.Services.Reservations
{
    using RussianBathHouse.Models.Reservations;
    using System.Collections.Generic;

    public interface IReservationsService
    {
        List<ReservationsUpcomingListModel> Upcoming(string id);

        List<ReservationsUpcomingListModel> All();
    }
}
