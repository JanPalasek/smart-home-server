using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Sensor
{
    public interface IGetSensorsService
    {
        Task<SensorModel> GetSensorByIdAsync(long sensorId);
    }
}