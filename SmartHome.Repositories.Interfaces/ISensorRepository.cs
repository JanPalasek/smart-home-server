using System.Threading.Tasks;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories.Interfaces
{
    public interface ISensorRepository : IStandardRepository<SensorModel>
    {
        Task<bool> AnyWithBatteryPowerSourceAsync(long sensorId);
    }
}