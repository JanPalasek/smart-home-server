using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Services.TemperatureMeasurement
{
    public class DeleteTemperatureMeasurementService : IDeleteTemperatureMeasurementService
    {
        private readonly ITemperatureMeasurementRepository repository;

        public DeleteTemperatureMeasurementService(ITemperatureMeasurementRepository repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
        }
    }
}