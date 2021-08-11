namespace RussianBathHouse.Services.Reservations
{
    using RussianBathHouse.Data;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using System.Collections.Generic;
    using System.Linq;

    public class ReservationsService : IReservationsService
    {
        private readonly BathHouseDbContext data;

        public ReservationsService(BathHouseDbContext data)
        {
            this.data = data;
        }

        public List<ReservationsUpcomingListModel> All()
        {
            var reservations = this.data.Reservations
                .Select(a => new ReservationsUpcomingListModel
                {
                    ReservedFrom =a.ReservedFrom,
                    CabinNumber = a.CabinId,
                    NumberOfPeople = a.NumberOfPeople,
                    ReservationServices = a.ReservationServices
                    .Select(rs => new ServiceListViewModel
                    {
                        Description = rs.Service.Description
                    }),
                })
                .ToList();

            return reservations;
        }

        public List<ReservationsUpcomingListModel> Upcoming(string id)
        {
            var reservations = this.data.Reservations
                .Where(r => id == r.UserId)
                .Select(a => new ReservationsUpcomingListModel
                {
                    ReservedFrom = a.ReservedFrom,
                    CabinNumber = a.CabinId,
                    NumberOfPeople = a.NumberOfPeople,
                    ReservationServices = a.ReservationServices
                    .Select(rs => new ServiceListViewModel
                    {
                        Description = rs.Service.Description
                    }),
                })
                .ToList();

            return reservations;
        }
    }
}
