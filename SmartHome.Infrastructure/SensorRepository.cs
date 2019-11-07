using System.Threading.Tasks;
using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class SensorRepository : StandardRepository<Sensor, SensorModel>, ISensorRepository
    {
        public SensorRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public Task<bool> AnyWithBatteryPowerSourceAsync(long sensorId)
        {
            return AnyAsync(x => x.Id == sensorId && x.BatteryPowerSourceTypeId != null);
        }
    }
}