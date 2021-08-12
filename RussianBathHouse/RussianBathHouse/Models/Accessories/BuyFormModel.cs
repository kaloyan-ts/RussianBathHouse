namespace RussianBathHouse.Models.Accessories
{
    using System.ComponentModel.DataAnnotations;

    using static  Data.DataConstants;
    public class BuyFormModel
    {
        public string AccessoryId { get; set; }

        public decimal AccessoryPrice { get; set; }
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        [MinLength(PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        [MinLength(AddressMinLength)]
        public string Address { get; set; }

    }
}
