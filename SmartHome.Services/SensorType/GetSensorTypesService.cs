using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Sensor;
using SmartHome.DomainCore.ServiceInterfaces.SensorType;

namespace SmartHome.Services.SensorType
{
    public class GetSensorTypesService : IGetSensorTypesService
    {
        private readonly ISensorTypeRepository repository;

        public GetSensorTypesService(ISensorTypeRepository repository)
        {
            this.repository = repository;
        }

        public Task<IList<SensorTypeModel>> GetAllSensorTypesAsync()
        {
            return repository.GetAllAsync();
        }

        public async Task<SensorTypeModel> GetByIdAsync(long id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}