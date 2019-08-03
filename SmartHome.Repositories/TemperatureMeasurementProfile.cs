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
                .ForMember(x => x.Sensor, x => x.Ignore())
                .ForMember(x => x.Place, x => x.Ignore())
                .ReverseMap();
        }
    }
}