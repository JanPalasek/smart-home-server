using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class SensorTypeRepository : StandardRepository<SensorType, SensorTypeModel>, ISensorTypeRepository
    {
        public SensorTypeRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }
    }
}