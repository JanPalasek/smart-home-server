using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<UnitModel, Unit>()
                .ForMember(x => x.UnitType, x => x.Ignore())
                .ForMember(x => x.BatteryPowerSourceType, x => x.Ignore())
                .ReverseMap();
        }
    }
}