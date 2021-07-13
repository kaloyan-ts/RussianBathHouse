namespace RussianBathHouse.Data.Models
{
    public class ServiceReservationListViewModel
    {
        public Service Service { get; set; }

        public string ServiceId { get; set; }

        public Reservation Reservation { get; set; }

        public string ReservationId { get; set; }
    }
}
