using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Services.TemperatureMeasurement
{
    public class CreateTemperatureMeasurementService : ICreateTemperatureMeasurementService
    {
        private readonly ISensorRepository sensorRepository;
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;

        public CreateTemperatureMeasurementService(
            ITemperatureMeasurementRepository temperatureMeasurementRepository,
            ISensorRepository sensorRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
            this.sensorRepository = sensorRepository;
        }

        public async Task<ServiceResult<long>> CreateAsync(long sensorId, double temperature, DateTime measurementDateTime)
        {
            if (!(await sensorRepository.AnyAsync(sensorId)))
            {
                return new ServiceResult<long>(default, SmartHomeValidationResult.Failed(
                    new SmartHomeValidation(nameof(sensorId), "Invalid sensor Id")));
            }

            long? placeId = (await sensorRepository.GetByIdAsync(sensorId)).PlaceId;
            
            var temperatureMeasurement = new TemperatureMeasurementModel()
            {
                MeasurementDateTime = measurementDateTime,
                Temperature = temperature,
                SensorId = sensorId,
                PlaceId = placeId
            };

            long id = await temperatureMeasurementRepository.AddOrUpdateAsync(temperatureMeasurement);
            
            return new ServiceResult<long>(id, SmartHomeValidationResult.Success);
        }

        public async Task<ServiceResult<long>> CreateAsync(TemperatureMeasurementModel model)
        {
            if (model.Id > 0)
            {
                throw new ArgumentException("Invalid arguments.");
            }
            
            long id = await temperatureMeasurementRepository.AddOrUpdateAsync(model);
            
            return new ServiceResult<long>(id, SmartHomeValidationResult.Success);
        }
    }
}