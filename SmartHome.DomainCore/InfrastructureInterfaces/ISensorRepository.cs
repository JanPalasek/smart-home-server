using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface ISensorRepository : IStandardRepository<SensorModel>
    {
        Task<bool> AnyWithBatteryPowerSourceAsync(long sensorId);
    }
}