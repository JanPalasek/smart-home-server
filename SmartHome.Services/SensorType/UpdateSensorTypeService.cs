using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.SensorType;

namespace SmartHome.Services.SensorType
{
    public class UpdateSensorTypeService : IUpdateSensorTypeService
    {
        private readonly ISensorTypeRepository repository;

        public UpdateSensorTypeService(ISensorTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task UpdateAsync(SensorTypeModel model)
        {
            if (model.Id == 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }
            await repository.AddOrUpdateAsync(model);
        }
    }
}