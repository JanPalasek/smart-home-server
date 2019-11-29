using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface ITemperatureMeasurementRepository : IGetAllRepository<TemperatureMeasurementModel>,
        IGetByIdRepository<TemperatureMeasurementModel>
    {
        Task<long> AddOrUpdateAsync(TemperatureMeasurementModel temperatureMeasurementModel);
        
        Task<CountedResult<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter, PagingArgs pagingArgs);

        Task<IList<Data.Models.TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter);
        /// <summary>
        /// Obtains last temperature measurement of specified sensor.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        Task<TemperatureMeasurementModel?> GetSensorLastTemperatureMeasurementAsync(long sensorId);

        /// <summary>
        /// Obtains last temperature measurement of all sensors.
        /// </summary>
        /// <returns></returns>
        Task<IList<OverviewTemperatureMeasurementModel>> GetAllSensorsLastTemperatureMeasurementAsync();

        Task DeleteAsync(long id);
    }
}