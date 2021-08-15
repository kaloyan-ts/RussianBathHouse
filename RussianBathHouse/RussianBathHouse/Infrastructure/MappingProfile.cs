namespace RussianBathHouse.Infrastructure
{
    using AutoMapper;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Models.Purchases;
    using RussianBathHouse.Models.Reservations;
    using RussianBathHouse.Models.Services;
    using RussianBathHouse.Services.Accessories;
    using RussianBathHouse.Services.Users;
    using System.Linq;

    public class MappingProfile : Profile
    {
        private readonly IUsersService users;
        private readonly IAccessoriesService accessories;

        public MappingProfile(IUsersService users, IAccessoriesService accessories)
        {
            this.users = users;
            this.accessories = accessories;
        }

        public MappingProfile()
        {

            this.CreateMap<Accessory, AccessoriesAllViewModel>()
                .ForMember(a => a.Image, cfg => cfg.MapFrom(m => m.ImagePath));

            this.CreateMap<Accessory, AccessoryDetailsViewModel>()
                .ForMember(a => a.Image, cfg => cfg.MapFrom(m => m.ImagePath));

            this.CreateMap<Accessory, AccessoryEditFormModel>()
                .ForMember(a => a.Quantity, cfg => cfg.MapFrom(m => m.QuantityLeft));

            this.CreateMap<Reservation, ReservedDayAndHoursViewModel>()
                .ForMember(r => r.Date, cfg => cfg.MapFrom(m => m.ReservedFrom));

        }
    }
}
