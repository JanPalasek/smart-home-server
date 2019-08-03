using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class BatteryPowerSourceTypeRepository : StandardRepository<BatteryPowerSourceType, BatteryPowerSourceTypeModel>, IBatteryPowerSourceTypeRepository
    {
        public BatteryPowerSourceTypeRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }
    }
}