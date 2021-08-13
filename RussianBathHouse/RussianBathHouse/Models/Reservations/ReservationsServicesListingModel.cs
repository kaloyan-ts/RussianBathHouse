namespace RussianBathHouse.Models.Reservations
{
    using RussianBathHouse.Models.Services;
    public class ReservationsServicesListingModel
    {
        public string ReservationId { get; set; }

        public ServiceListViewModel[] Services { get; set; }
    }
}

