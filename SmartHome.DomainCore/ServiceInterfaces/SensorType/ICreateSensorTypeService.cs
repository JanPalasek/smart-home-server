using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.SensorType
{
    public interface ICreateSensorTypeService
    {
        Task<long> CreateAsync(SensorTypeModel model);
    }
}