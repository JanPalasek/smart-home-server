using System;
using System.Threading.Tasks;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IBatteryMeasurementRepository
    {
        Task<long> AddAsync(long sensorId, double voltage, DateTime measurementDateTime);
    }
}