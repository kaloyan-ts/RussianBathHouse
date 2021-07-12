namespace RussianBathHouse.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Reservation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int NumberOfPeople { get; set; }

        public int CabinId { get; set; }

        public Cabin Cabin { get; set; }

        public DateTime ReservedFrom { get; set; }

        public DateTime ReservedUntill { get; set; }

        public ICollection<Service> Services{ get; set; }
    }
}
