using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class SensorProfile : Profile
    {
        public SensorProfile()
        {
            CreateMap<SensorModel, Sensor>()
                .ForMember(x => x.SensorType, x => x.Ignore())
                .ForMember(x => x.BatteryPowerSourceType, x => x.Ignore())
                .ForMember(x => x.Place, x => x.Ignore())
                .ReverseMap()
                .ForMember(x => x.PlaceName, x => x.MapFrom(y => y.Place!.Name))
                .ForMember(x => x.SensorTypeName, x => x.MapFrom(y => y.SensorType!.Name))
                .ForMember(x => x.BatteryPowerSourceTypeName, x => x.MapFrom(y => y.BatteryPowerSourceType!.BatteryType));
        }
    }
}