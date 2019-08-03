using System;
using System.Threading.Tasks;

namespace SmartHome.Repositories.Interfaces
{
    public interface IBatteryMeasurementRepository
    {
        Task<long> AddAsync(long sensorId, double voltage, DateTime measurementDateTime);
    }
}