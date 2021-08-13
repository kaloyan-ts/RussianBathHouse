namespace RussianBathHouse.Models.Reservations
{
    using RussianBathHouse.Models.Services;
    using System;
    using System.Collections.Generic;

    public class ReservationsUpcomingListModel
    {
        public string UserFullName { get; set; }

        public int NumberOfPeople { get; set; }

        public int CabinNumber { get; set; }

        public decimal ServicesPrice { get; set; }

        public decimal CabinPrice { get; set; }

        public DateTime ReservedFrom { get; set; }

        public IEnumerable<ServiceListViewModel> ReservationServices { get; set; }

    }
}
