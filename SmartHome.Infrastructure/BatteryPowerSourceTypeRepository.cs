using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class BatteryPowerSourceTypeRepository : StandardRepository<BatteryPowerSourceType, BatteryPowerSourceTypeModel>, IBatteryPowerSourceTypeRepository
    {
        public BatteryPowerSourceTypeRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }
    }
}