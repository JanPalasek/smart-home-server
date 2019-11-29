using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class TemperatureMeasurementProfile : Profile
    {
        public TemperatureMeasurementProfile()
        {
            CreateMap<TemperatureMeasurementModel, TemperatureMeasurement>()
                .ForMember(x => x.Sensor, x => x.Ignore())
                .ForMember(x => x.Place, x => x.Ignore())
                .ReverseMap()
                .ForMember(x => x.PlaceName, x => x.MapFrom(y => y.Place.Name))
                .ForMember(x => x.SensorName, x => x.MapFrom(y => y.Sensor.Name));

            CreateMap<TemperatureMeasurement, OverviewTemperatureMeasurementModel>()
                .ForMember(x => x.PlaceName, x => x.MapFrom(y => y.Place.Name))
                .ForMember(x => x.IsInside, x => x.MapFrom(y => y.Place.IsInside))
                .ForMember(x => x.SensorTypeName, x => x.MapFrom(y => y.Sensor.SensorType.Name));
        }
    }
}