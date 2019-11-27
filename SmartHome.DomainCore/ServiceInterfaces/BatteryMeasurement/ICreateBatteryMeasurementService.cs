using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.BatteryMeasurement
{
    public interface ICreateBatteryMeasurementService
    {
        Task<SmartHomeValidationResult> CreateAsync(long sensorId, double voltage, DateTime measurementDateTime);
    }
}