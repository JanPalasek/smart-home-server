using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.SensorType
{
    public interface IDeleteSensorTypeService
    {
        Task DeleteAsync(long id);
    }
}