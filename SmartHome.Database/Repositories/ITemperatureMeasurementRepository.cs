using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Database.Entities;
using SmartHome.Shared;

namespace SmartHome.Database.Repositories
{
    public interface ITemperatureMeasurementRepository : IGenericRepository<TemperatureMeasurement>
    {
        Task<long> AddAsync(long unitId, double temperature);
        Task<IList<TemperatureMeasurement>> GetTemperatureMeasurements(MeasurementFilter filter);
    }
}