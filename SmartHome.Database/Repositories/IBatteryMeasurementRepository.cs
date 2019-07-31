using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Database.Entities;

namespace SmartHome.Database.Repositories
{
    public interface IBatteryMeasurementRepository
    {
        Task<long> AddAsync(long unitId, double voltage);
    }
}