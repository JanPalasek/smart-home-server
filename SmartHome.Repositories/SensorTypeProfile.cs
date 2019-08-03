using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
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