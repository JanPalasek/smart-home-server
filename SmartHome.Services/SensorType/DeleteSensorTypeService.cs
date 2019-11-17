using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.SensorType;

namespace SmartHome.Services.SensorType
{
    public class DeleteSensorTypeService : IDeleteSensorTypeService
    {
        private readonly ISensorTypeRepository repository;

        public DeleteSensorTypeService(ISensorTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
        }
    }
}