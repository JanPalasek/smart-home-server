using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Database.Entities;
using SmartHome.Shared;

namespace SmartHome.Database.Repositories
{
    public interface ITemperatureMeasurementRepository
    {
        Task<long> AddAsync(long unitId, double temperature, DateTime measurementDateTime);
        Task<IList<TemperatureMeasurement>> GetTemperatureMeasurementsAsync(MeasurementFilter filter);
        
        /// <summary>
        /// Obtains last temperature measurement of specified unit.
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        Task<TemperatureMeasurement> GetUnitLastTemperatureMeasurementAsync(long unitId);
    }
}