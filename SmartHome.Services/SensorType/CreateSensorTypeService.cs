using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.SensorType;

namespace SmartHome.Services.SensorType
{
    public class CreateSensorTypeService : ICreateSensorTypeService
    {
        private readonly ISensorTypeRepository repository;

        public CreateSensorTypeService(ISensorTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<long> CreateAsync(SensorTypeModel model)
        {
            if (model.Id > 0)
            {
                throw new ArgumentException(nameof(model.Id), "Invalid model.");
            }
            return await repository.AddOrUpdateAsync(model);
        }
    }
}