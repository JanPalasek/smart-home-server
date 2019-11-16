using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Sensor;

namespace SmartHome.Services.Sensor
{
    public class CreateSensorService : ICreateSensorService
    {
        private readonly ISensorRepository repository;

        public CreateSensorService(ISensorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ServiceResult<long>> CreateSensorAsync(SensorModel model)
        {
            if (model.Id > 0)
            {
                throw new ArgumentException(nameof(model.Id), "Invalid model Id.");
            }
            
            return new ServiceResult<long>(await repository.AddOrUpdateAsync(model), SmartHomeValidationResult.Success);
        }
    }
}