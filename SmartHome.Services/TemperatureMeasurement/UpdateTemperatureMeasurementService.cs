using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Services.TemperatureMeasurement
{
    public class UpdateTemperatureMeasurementService : IUpdateTemperatureMeasurementService
    {
        private readonly ITemperatureMeasurementRepository repository;

        public UpdateTemperatureMeasurementService(ITemperatureMeasurementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SmartHomeValidationResult> UpdateTemperatureMeasurementAsync(TemperatureMeasurementModel model)
        {
            if (model.Id == 0)
            {
                throw new ArgumentException(nameof(model.Id), "Invalid parameters.");
            }

            await repository.AddOrUpdateAsync(model);

            return SmartHomeValidationResult.Success;
        }
    }
}