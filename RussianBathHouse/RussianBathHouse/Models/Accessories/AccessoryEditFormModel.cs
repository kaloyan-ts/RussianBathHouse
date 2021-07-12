namespace RussianBathHouse.Models.Accessories
{
    using System.ComponentModel.DataAnnotations;
    using System;

    using static Data.DataConstants;
    public class AccessoryEditFormModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(AccessoryNameMaxLength, MinimumLength = AccessoryNameMinLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0.01, 100.00,
           ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
