using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class BatteryPowerSourceTypeProfile : Profile
    {
        public BatteryPowerSourceTypeProfile()
        {
            CreateMap<BatteryPowerSourceTypeModel, BatteryPowerSourceType>().ReverseMap();
        }
    }
}