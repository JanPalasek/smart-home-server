using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Sensor;

namespace SmartHome.Services.Sensor
{
    public class DeleteSensorService : IDeleteSensorService
    {
        private readonly ISensorRepository repository;

        public DeleteSensorService(ISensorRepository repository)
        {
            this.repository = repository;
        }

        public async Task DeleteSensorAsync(long sensorId)
        {
            await repository.DeleteAsync(sensorId);
        }
    }
}