using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Sensor;

namespace SmartHome.Services.Sensor
{
    public class GetSensorsService : IGetSensorsService
    {
        private readonly ISensorRepository repository;

        public GetSensorsService(ISensorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SensorModel> GetSensorByIdAsync(long sensorId)
        {
            return await repository.GetByIdAsync(sensorId);
        }
    }
}