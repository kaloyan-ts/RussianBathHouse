namespace RussianBathHouse.Services.Reservations
{
    using RussianBathHouse.Models.Reservations;
    using System;
    using System.Collections.Generic;

    public interface IReservationsService
    {
        List<ReservationsUpcomingListModel> Upcoming(string id);

        List<ReservationsUpcomingListModel> All();

        DateTime GetDateTimeOfReservation(string dateAndTime);

        int GetTimespan(string date);

    }
}
