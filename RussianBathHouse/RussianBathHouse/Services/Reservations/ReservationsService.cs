namespace RussianBathHouse.Services.Reservations
{
    using RussianBathHouse.Data;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using System;
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
                .Where(r => r.ReservedFrom.CompareTo(DateTime.Now) > 0)
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
                    ServicesPrice = a.ReservationServices.Sum(rs => rs.Service.Price),
                    CabinPrice = a.Cabin.PricePerHour * 2
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
                        Description = rs.Service.Description,
                    }),
                    ServicesPrice = a.ReservationServices.Sum(rs => rs.Service.Price),
                    CabinPrice = a.Cabin.PricePerHour * 2
                })
                .ToList();

            return reservations;
        }


        public DateTime GetDateTimeOfReservation(string dateAndTime)
        {
            int timespan;
            int date;

            if (dateAndTime.StartsWith("0"))
            {
                date = 0;
                timespan = GetTimespan(dateAndTime);

            }
            else if (dateAndTime.StartsWith("1"))
            {
                date = 1;
                timespan = GetTimespan(dateAndTime);

            }
            else if (dateAndTime.StartsWith("2"))
            {
                date = 2;
                timespan = GetTimespan(dateAndTime);

            }
            else if (dateAndTime.StartsWith("3"))
            {
                date = 3;
                timespan = GetTimespan(dateAndTime);

            }
            else if (dateAndTime.StartsWith("4"))
            {
                date = 4;
                timespan = GetTimespan(dateAndTime);

            }
            else if (dateAndTime.StartsWith("5"))
            {
                date = 5;
                timespan = GetTimespan(dateAndTime);

            }
            else if (dateAndTime.StartsWith("6"))
            {
                date = 6;
                timespan = GetTimespan(dateAndTime);
            }
            else
            {
                date = 9;
                timespan = 9;
            }

            var reservationTime = new DateTime();

            for (int i = 0; i < 7; i++)
            {
                if (date == i)
                {
                    reservationTime = DateTime.Now.Date.AddDays(i);
                    break;
                }

            }
            for (int h = 8; h < 20; h += 2)
            {
                if (timespan == h)
                {
                    reservationTime = reservationTime.AddHours(h);
                    break;
                }
            }
            return reservationTime;
        }

        public int GetTimespan(string date)
        {
            var timespan = int.Parse(date.Substring(4, 1));

            return timespan switch
            {
                0 => 8,
                1 => 10,
                2 => 12,
                3 => 14,
                4 => 16,
                5 => 18,
                6 => 20,
                _ => 7,
            };
        }
    }
}
