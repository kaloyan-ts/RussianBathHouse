namespace RussianBathHouse.Test.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Services;
    using RussianBathHouse.Services.Reservations;
    using System;
    using System.Linq;
    using Xunit;
    public class ReservationServiceTest : BaseTest
    {
        private IReservationsService reservations => this.ServiceProvider.GetRequiredService<IReservationsService>();

        private const string id = "1";
        private const int cabinId = 4;
        private const int numberOfPeople = 2;
        private DateTime reservedFrom = DateTime.UtcNow;

        private const string serviceId = "1";
        private const string serviceDescription = "description";
        private const decimal servicePrice = 20;

        [Fact]
        public void FindByIdMethodReturnsTheCorrectReservation()
        {
            //Arrange
            var id = this.AddReservation();

            //Act
            var result = reservations.FindById(id);
            var expected = this.DbContext.Reservations.FirstOrDefault(a => a.Id == id);

            //Assert
            Assert.Same(expected, result);
        }

        [Fact]
        public void AddMethodCreatesReservation()
        {
            //Act
            var reservationId = this.reservations.Add(numberOfPeople, reservedFrom, TestUser.Identifier);

            var reservation = this.DbContext.Reservations.FirstOrDefault();
            var reservationsCount = this.DbContext.Reservations.Count();

            //Assert
            Assert.Equal(1, reservationsCount);
            Assert.Equal(TestUser.Identifier, reservation.UserId);
            Assert.Equal(numberOfPeople, reservation.NumberOfPeople);
            Assert.Equal(reservedFrom, reservation.ReservedFrom);
            Assert.Equal(reservationId, reservation.Id);
        }

        [Fact]
        public void AddReservationToServiceAddsCorrectly()
        {
            //Arrange
            var reservatiodId = AddReservation();
            var serviceId = AddService();
            var service = new ServiceListViewModel
            {
                Id = serviceId,
            };
            var services = new ServiceListViewModel[] { service };

            //Act
            this.reservations.AddServicesToReservation(id, services);
            var reservation = this.DbContext.Reservations.FirstOrDefault(r => r.Id == id);
            var serviceResult = reservation.ReservationServices.First();

            //Assert
            Assert.Single(reservation.ReservationServices);
            Assert.NotNull(serviceResult);
            Assert.Equal(serviceId, serviceResult.Service.Id);
            Assert.Equal(serviceDescription, serviceResult.Service.Description);
            Assert.Equal(TestUser.Identifier, reservation.UserId);
            Assert.Equal(numberOfPeople, reservation.NumberOfPeople);
            Assert.Equal(reservedFrom, reservation.ReservedFrom);
            Assert.Equal(reservatiodId, reservation.Id);
        }

        [Fact]
        public void GetReservationsServicesShouldReturnCorrectServices()
        {
            //Arrange
            var serviceId = AddService();

            //Act

            var servicesResult = this.reservations.GetReservationServices();
            var service = servicesResult[0];

            //Assert
            Assert.NotNull(servicesResult);
            Assert.IsAssignableFrom<ServiceListViewModel[]>(servicesResult);
            Assert.Equal(serviceDescription, service.Description);
            Assert.Equal(serviceId, service.Id);
            Assert.Single(servicesResult);
        }

        [Theory]
        [InlineData("0 + 0")]
        [InlineData("0 + 1")]
        [InlineData("0 + 2")]
        [InlineData("0 + 3")]
        [InlineData("0 + 4")]
        [InlineData("0 + 5")]
        [InlineData("0 + 6")]
        public void GetDateOfReservationCorrectForToday0(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);

            //Assert
            Assert.Equal(DateTime.Today, servicesResult.Date);
        }

        [Theory]
        [InlineData("1 + 0")]
        [InlineData("1 + 1")]
        [InlineData("1 + 2")]
        [InlineData("1 + 3")]
        [InlineData("1 + 4")]
        [InlineData("1 + 5")]
        [InlineData("1 + 6")]
        public void GetDateOfReservationCorrectForToday1(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);

            //Assert
            Assert.Equal(DateTime.Today.AddDays(1), servicesResult.Date);
        }

        [Theory]
        [InlineData("2 + 0")]
        [InlineData("2 + 1")]
        [InlineData("2 + 2")]
        [InlineData("2 + 3")]
        [InlineData("2 + 4")]
        [InlineData("2 + 5")]
        [InlineData("2 + 6")]
        public void GetDateOfReservationCorrectForToday2(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);

            //Assert
            Assert.Equal(DateTime.Today.AddDays(2), servicesResult.Date);
        }

        [Theory]
        [InlineData("3 + 0")]
        [InlineData("3 + 1")]
        [InlineData("3 + 2")]
        [InlineData("3 + 3")]
        [InlineData("3 + 4")]
        [InlineData("3 + 5")]
        [InlineData("3 + 6")]
        public void GetDateOfReservationCorrectForToday3(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);

            //Assert
            Assert.Equal(DateTime.Today.AddDays(3), servicesResult.Date);
        }

        [Theory]
        [InlineData("4 + 0")]
        [InlineData("4 + 1")]
        [InlineData("4 + 2")]
        [InlineData("4 + 3")]
        [InlineData("4 + 4")]
        [InlineData("4 + 5")]
        [InlineData("4 + 6")]
        public void GetDateOfReservationCorrectForToday4(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);

            //Assert
            Assert.Equal(DateTime.Today.AddDays(4), servicesResult.Date);
        }

        [Theory]
        [InlineData("5 + 0")]
        [InlineData("5 + 1")]
        [InlineData("5 + 2")]
        [InlineData("5 + 3")]
        [InlineData("5 + 4")]
        [InlineData("5 + 5")]
        [InlineData("5 + 6")]
        public void GetDateReservationCorrectForToday5(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);

            //Assert
            Assert.Equal(DateTime.Today.AddDays(5), servicesResult.Date);
        }

        [Theory]
        [InlineData("0 + 0")]
        [InlineData("1 + 0")]
        [InlineData("2 + 0")]
        [InlineData("3 + 0")]
        [InlineData("4 + 0")]
        [InlineData("5 + 0")]
        [InlineData("6 + 0")]
        public void GetTimeReservationCorrectForTime0(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 8;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        [Theory]
        [InlineData("0 + 1")]
        [InlineData("1 + 1")]
        [InlineData("2 + 1")]
        [InlineData("3 + 1")]
        [InlineData("4 + 1")]
        [InlineData("5 + 1")]
        [InlineData("6 + 1")]
        public void GetTimeReservationCorrectForTime1(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 10;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        [Theory]
        [InlineData("0 + 2")]
        [InlineData("1 + 2")]
        [InlineData("2 + 2")]
        [InlineData("3 + 2")]
        [InlineData("4 + 2")]
        [InlineData("5 + 2")]
        [InlineData("6 + 2")]
        public void GetTimeReservationCorrectForTime2(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 12;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        [Theory]
        [InlineData("0 + 3")]
        [InlineData("1 + 3")]
        [InlineData("2 + 3")]
        [InlineData("3 + 3")]
        [InlineData("4 + 3")]
        [InlineData("5 + 3")]
        [InlineData("6 + 3")]
        public void GetTimeReservationCorrectForTime3(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 14;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        [Theory]
        [InlineData("0 + 4")]
        [InlineData("1 + 4")]
        [InlineData("2 + 4")]
        [InlineData("3 + 4")]
        [InlineData("4 + 4")]
        [InlineData("5 + 4")]
        [InlineData("6 + 4")]
        public void GetTimeReservationCorrectForTime4(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 16;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        [Theory]
        [InlineData("0 + 5")]
        [InlineData("1 + 5")]
        [InlineData("2 + 5")]
        [InlineData("3 + 5")]
        [InlineData("4 + 5")]
        [InlineData("5 + 5")]
        [InlineData("6 + 5")]
        public void GetTimeReservationCorrectForTime5(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 18;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        [Theory]
        [InlineData("0 + 6")]
        [InlineData("1 + 6")]
        [InlineData("2 + 6")]
        [InlineData("3 + 6")]
        [InlineData("4 + 6")]
        [InlineData("5 + 6")]
        [InlineData("6 + 6")]
        public void GetTimeReservationCorrectForTime6(string dateAndTime)
        {
            //Act

            var servicesResult = this.reservations.GetDateTimeOfReservation(dateAndTime);
            var expectedHour = 20;
            //Assert
            Assert.Equal(expectedHour, servicesResult.Hour);
        }

        private string AddReservation()
        {
            this.DbContext.Reservations.Add(new Reservation
            {
                Id = id,
                UserId = TestUser.Identifier,
                CabinId = cabinId,
                NumberOfPeople = numberOfPeople,
                ReservedFrom = reservedFrom,
            });

            this.DbContext.SaveChanges();

            return id;
        }

        private string AddService()
        {
            this.DbContext.Services.Add(new Service
            {
                Id = serviceId,
                Description = serviceDescription,
                Price = servicePrice,
            });

            this.DbContext.SaveChanges();

            return id;
        }

    }
}
