using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.Sensor
{
    public interface ICreateSensorService
    {
        Task<ServiceResult<long>> CreateSensorAsync(SensorModel model);
    }
}