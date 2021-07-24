
using RussianBathHouse.Models.Services;

namespace RussianBathHouse.Models.Reservations
{
    public class ReservationsServicesListingModel
    {
        public string ReservationId { get; set; }

        public ServiceListViewModel[] Services { get; set; }
    }
}

