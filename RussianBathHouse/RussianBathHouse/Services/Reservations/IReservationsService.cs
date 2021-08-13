namespace RussianBathHouse.Services.Reservations
{
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        List<ReservationsUpcomingListModel> UpcomingForUser(string id);

        List<ReservationsUpcomingListModel> AllUpcoming();

        DateTime GetDateTimeOfReservation(string dateAndTime);

        int GetTimespan(string date);

        List<ReservedDayAndHoursViewModel> GetReservedDates();

        string Add(int numberOfPeople,
            DateTime reservationTime,
            string userId);

        ServiceListViewModel[] GetReservationServices();

        Reservation FindById(string id);

        void AddServicesToReservation(string id, ServiceListViewModel[] services);

        Service FindServiceById(string serviceId);

    }
}
