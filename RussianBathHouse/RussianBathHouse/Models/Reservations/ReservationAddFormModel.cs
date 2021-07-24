namespace RussianBathHouse.Models.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class ReservationAddFormModel
    {

        [Required]
        [Range(ReservationMinPeople, ReservationMaxPeople)]
        public int NumberOfPeople { get; set; }

        [Range(typeof(DateTime), ReservationMinYear, ReservationMaxYear)]
        public DateTime ReserveFrom { get; set; }

        [Required]
        [Range(ReservationMinHours, ReservationMaxHours)]
        public int ReserveForHours { get; set; }

    }
}
