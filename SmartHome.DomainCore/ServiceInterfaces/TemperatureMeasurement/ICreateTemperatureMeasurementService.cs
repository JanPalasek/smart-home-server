using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement
{
    public interface ICreateTemperatureMeasurementService
    {
        Task<ServiceResult<long>> CreateAsync(long sensorId, double temperature, DateTime measurementDateTime);
        Task<ServiceResult<long>> CreateAsync(TemperatureMeasurementModel model);
    }
}