using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement
{
    public interface IUpdateTemperatureMeasurementService
    {
        Task<SmartHomeValidationResult> UpdateTemperatureMeasurementAsync(TemperatureMeasurementModel model);
    }
}