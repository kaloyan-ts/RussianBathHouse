namespace RussianBathHouse.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Reservation
    {
        public Reservation()
        {
            this.ReservationServices = new HashSet<ReservationService>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int NumberOfPeople { get; set; }

        public int CabinId { get; set; }

        public Cabin Cabin { get; set; }

        public DateTime ReservedFrom { get; set; }

        [Required]
        public string UserId { get; set; }

        public ICollection<ReservationService> ReservationServices { get; set; }
    }
}
