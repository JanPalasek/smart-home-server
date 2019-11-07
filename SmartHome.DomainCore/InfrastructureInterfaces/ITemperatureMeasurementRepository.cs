using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface ITemperatureMeasurementRepository
    {
        Task<long> AddAsync(long sensorId, double temperature, DateTime measurementDateTime);
        Task<IList<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter);
        
        /// <summary>
        /// Obtains last temperature measurement of specified sensor.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        Task<TemperatureMeasurementModel> GetSensorLastTemperatureMeasurementAsync(long sensorId);

        /// <summary>
        /// Obtains last temperature measurement of all sensors.
        /// </summary>
        /// <returns></returns>
        Task<IList<OverviewTemperatureMeasurementModel>> GetAllSensorsLastTemperatureMeasurementAsync();
    }
}