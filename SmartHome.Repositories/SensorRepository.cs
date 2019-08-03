using System.Threading.Tasks;
using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
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