namespace RussianBathHouse.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class PhoneNumber
    {

        [MaxLength(PhoneNumberMaxLength)]
        [MinLength(PhoneNumberMinLength)]
        public string Data { get; set; }
    }
}
