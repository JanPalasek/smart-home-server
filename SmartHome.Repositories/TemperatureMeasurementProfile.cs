using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class TemperatureMeasurementProfile : Profile
    {
        public TemperatureMeasurementProfile()
        {
            CreateMap<TemperatureMeasurementModel, TemperatureMeasurement>()
                .ForMember(x => x.Unit, x => x.Ignore())
                .ReverseMap();
        }
    }
}