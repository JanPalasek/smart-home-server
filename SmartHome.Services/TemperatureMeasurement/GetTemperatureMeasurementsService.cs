using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Services.TemperatureMeasurement
{
    public class GetTemperatureMeasurementsService : IGetTemperatureMeasurementsService
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;

        public GetTemperatureMeasurementsService(ITemperatureMeasurementRepository temperatureMeasurementRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
        }

        public Task<IList<TemperatureMeasurementModel>> GetFilteredMeasurementsAsync(
            MeasurementFilter filter)
        {
            return temperatureMeasurementRepository.GetTemperatureMeasurementsAsync(filter);
        }

        public async Task<IList<TemperatureMeasurementModel>> GetAllAsync()
        {
            return await temperatureMeasurementRepository.GetAllAsync();
        }

        public Task<TemperatureMeasurementModel?> GetLastMeasurementAsync(long sensorId)
        {
            return temperatureMeasurementRepository.GetSensorLastTemperatureMeasurementAsync(sensorId);
        }

        public async Task<TemperatureMeasurementModel> GetTemperatureMeasurementById(long id)
        {
            return await temperatureMeasurementRepository.GetByIdAsync(id);
        }
    }
}