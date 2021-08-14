namespace RussianBathHouse.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public string AccessoryId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal TotalPrice { get; set; }
    }
}
