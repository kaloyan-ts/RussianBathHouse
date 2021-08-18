namespace RussianBathHouse.Infrastructure
{
    using AutoMapper;
    using RussianBathHouse.Data.Models;
    using RussianBathHouse.Models.Accessories;
    using RussianBathHouse.Models.Reservations;

    public class MappingProfile : Profile
    {
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
