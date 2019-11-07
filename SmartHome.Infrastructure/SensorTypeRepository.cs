using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class SensorTypeRepository : StandardRepository<SensorType, SensorTypeModel>, ISensorTypeRepository
    {
        public SensorTypeRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }
    }
}