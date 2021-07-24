namespace RussianBathHouse.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Service
    {
        public Service()
        {
            this.ReservationServices = new HashSet<ReservationService>();
        }

        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public ICollection<ReservationService> ReservationServices { get; set; }
    }
}
