namespace RussianBathHouse.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    using static DataConstants;
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
