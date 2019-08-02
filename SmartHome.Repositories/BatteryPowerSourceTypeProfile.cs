using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class BatteryPowerSourceTypeProfile : Profile
    {
        public BatteryPowerSourceTypeProfile()
        {
            CreateMap<BatteryPowerSourceTypeModel, BatteryPowerSourceType>().ReverseMap();
        }
    }
}