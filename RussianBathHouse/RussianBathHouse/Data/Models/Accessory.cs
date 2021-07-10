namespace RussianBathHouse.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants;
    public class Accessory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(AccessoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int QuantityLeft { get; set; }
    }
}
