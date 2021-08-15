namespace RussianBathHouse.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Address
    {
        [MaxLength(AddressMaxLength)]
        [MinLength(AddressMinLength, ErrorMessage = "The address should be between 10 and 40 symbols")]
        public string Data { get; set; }
    }
}
