using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class SensorProfile : Profile
    {
        public SensorProfile()
        {
            CreateMap<SensorModel, Sensor>()
                .ForMember(x => x.SensorType, x => x.Ignore())
                .ForMember(x => x.BatteryPowerSourceType, x => x.Ignore())
                .ForMember(x => x.Place, x => x.Ignore())
                .ReverseMap();
        }
    }
}