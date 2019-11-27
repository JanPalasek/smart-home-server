using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryMeasurement;

namespace SmartHome.Services.BatteryMeasurement
{
    public class CreateBatteryMeasurementService : ICreateBatteryMeasurementService
    {
        private readonly IBatteryMeasurementRepository batteryMeasurementRepository;
        private readonly ISensorRepository sensorRepository;

        public CreateBatteryMeasurementService(
            IBatteryMeasurementRepository batteryMeasurementRepository,
            ISensorRepository sensorRepository)
        {
            this.batteryMeasurementRepository = batteryMeasurementRepository;
            this.sensorRepository = sensorRepository;
        }

        public async Task<SmartHomeValidationResult> CreateAsync(long sensorId, double voltage, DateTime measurementDateTime)
        {
            if (!(await sensorRepository.AnyWithBatteryPowerSourceAsync(sensorId)))
            {
                return SmartHomeValidationResult.Failed(new SmartHomeValidation(
                    nameof(sensorId), $"There is no sensor with id {sensorId} with battery power source."));
            }
            
            await batteryMeasurementRepository.AddAsync(sensorId, voltage, measurementDateTime);
            
            return SmartHomeValidationResult.Success;
        }
    }
}