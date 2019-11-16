using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.Sensor
{
    public interface IDeleteSensorService
    {
        Task DeleteSensorAsync(long sensorId);
    }
}