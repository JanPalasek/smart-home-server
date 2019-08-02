using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories.Interfaces
{
    public interface ITemperatureMeasurementRepository
    {
        Task<long> AddAsync(long unitId, double temperature, DateTime measurementDateTime);
        Task<IList<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter);
        
        /// <summary>
        /// Obtains last temperature measurement of specified unit.
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        Task<TemperatureMeasurementModel> GetUnitLastTemperatureMeasurementAsync(long unitId);
    }
}