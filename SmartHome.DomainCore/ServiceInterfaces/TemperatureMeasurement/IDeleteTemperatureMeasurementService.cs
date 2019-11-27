using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement
{
    public interface IDeleteTemperatureMeasurementService
    {
        Task DeleteAsync(long id);
    }
}