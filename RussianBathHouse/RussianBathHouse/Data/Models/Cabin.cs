namespace RussianBathHouse.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cabin
    {
        [Key]
        public int Id { get; set; }

        public int Capacity { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal PricePerHour { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

    }
}
