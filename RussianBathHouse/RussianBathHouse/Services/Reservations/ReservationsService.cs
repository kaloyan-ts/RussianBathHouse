namespace RussianBathHouse.Services.Reservations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RussianBathHouse.Data;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using RussianBathHouse.Services.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReservationsService : IReservationsService
    {
        private readonly BathHouseDbContext data;
        private readonly IMapper mapper;
        private readonly IUsersService users;

        public ReservationsService(BathHouseDbContext data, IMapper mapper, IUsersService users)
        {
            this.data = data;
            this.mapper = mapper;
            this.users = users;
        }

        public List<ReservationsUpcomingListModel> AllUpcoming()
        {
            var reservations = this.data.Reservations
                .Where(r => r.ReservedFrom.CompareTo(DateTime.Today) > 0)
                .Select(a => new ReservationsUpcomingListModel
                {
                    ReservedFrom = a.ReservedFrom,
                    UserFullName = users.GetUserFullName(a.UserId).Result,
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



        public List<ReservationsUpcomingListModel> UpcomingForUser(string id)
        {
            var reservations = this.data.Reservations
               .Where(r => r.ReservedFrom.CompareTo(DateTime.Today) > 0)
               .Where(r => r.UserId == id)
               .Select(a => new ReservationsUpcomingListModel
               {
                   ReservedFrom = a.ReservedFrom,
                   UserFullName = users.GetUserFullName(a.UserId).Result,
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
            for (int h = 8; h <= 20; h += 2)
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

        public List<ReservedDayAndHoursViewModel> GetReservedDates()
        {
            var reserved = this.data.Reservations
                .ProjectTo<ReservedDayAndHoursViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return reserved;
        }

        public string Add(int numberOfPeople,
            DateTime reservationTime,
            string userId)
        {
            var reservation = new Reservation
            {
                NumberOfPeople = numberOfPeople,
                ReservedFrom = reservationTime,
                UserId = userId,
                CabinId = 4,
            };

            this.data.Reservations.Add(reservation);
            this.data.SaveChanges();

            return reservation.Id;
        }

        public ServiceListViewModel[] GetReservationServices()
        {
            var services = this.data
                .Services
                .Select(s => new ServiceListViewModel
                {
                    Description = s.Description,
                    Id = s.Id
                })
                .ToArray();

            return services;
        }

        public Reservation FindById(string id)
        {
            var reservation = this.data.Reservations.FirstOrDefault(r => r.Id == id);

            return reservation;
        }

        public void AddServicesToReservation(string id, ServiceListViewModel[] services)
        {
            var reservation = FindById(id);

            var chosenServices = new List<Service>();

            foreach (var service in services)
            {
                if (service == null)
                {
                    continue;
                }

                reservation.ReservationServices.Add(new ReservationService
                {
                    ServiceId = service.Id
                });
            }

            this.data.SaveChanges();
        }

        public Service FindServiceById(string serviceId)
        {
            return this.data.Services.First(s => s.Id == serviceId);
        }
    }
}
