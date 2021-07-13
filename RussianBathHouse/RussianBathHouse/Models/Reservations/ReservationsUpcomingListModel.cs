namespace RussianBathHouse.Models.Reservations
{
    using RussianBathHouse.Models.Services;
    using System;
    using System.Collections.Generic;

    public class ReservationsUpcomingListModel
    {

        public int NumberOfPeople { get; set; }

        public int CabinNumber { get; set; }

        public DateTime ReservedFrom { get; set; }

        public IEnumerable<ServiceReservaionListViewModel> ReservationServices { get; set; }

    }
}
