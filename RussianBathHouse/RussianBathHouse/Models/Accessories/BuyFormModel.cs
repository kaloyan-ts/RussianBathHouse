namespace RussianBathHouse.Models.Accessories
{
    using System.ComponentModel.DataAnnotations;

    using static  Data.DataConstants;
    public class BuyFormModel
    {
        public string AccessoryId { get; set; }

        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        [MinLength(PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        [MinLength(AddressMinLength, ErrorMessage = "The address should be between 10 and 40 symbols")]
        public string Address { get; set; }

    }
}
