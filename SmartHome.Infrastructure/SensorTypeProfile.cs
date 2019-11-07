using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class SensorTypeProfile : Profile
    {
        public SensorTypeProfile()
        {
            CreateMap<SensorType, SensorTypeModel>().ReverseMap();
            CreateMap<SensorType, EntityReference>();
        }
    }
}