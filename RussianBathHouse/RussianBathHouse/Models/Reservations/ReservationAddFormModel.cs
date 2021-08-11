namespace RussianBathHouse.Models.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class ReservationAddFormModel
    {
        [Required]
        [Range(ReservationMinPeople, ReservationMaxPeople)]
        public int NumberOfPeople { get; set; }

        public string DateAndTimeId { get; set; }

        public IEnumerable<ReservedDayAndHoursViewModel> Reserved { get; set; } = new List<ReservedDayAndHoursViewModel>();

    }
}
