using System;
using System.Threading.Tasks;

namespace SmartHome.Repositories.Interfaces
{
    public interface IBatteryMeasurementRepository
    {
        Task<long> AddAsync(long unitId, double voltage, DateTime measurementDateTime);
    }
}