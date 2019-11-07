using System;
using System.Threading.Tasks;
using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class BatteryMeasurementRepository : GenericRepository<BatteryMeasurement>, IBatteryMeasurementRepository
    {
        public BatteryMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public async Task<long> AddAsync(long sensorId, double voltage, DateTime measurementDateTime)
        {
            var sensor = await SmartHomeAppDbContext.SingleAsync<Sensor>(sensorId);
            
            if (sensor.BatteryPowerSourceTypeId == null)
            {
                throw new ArgumentNullException(nameof(sensor.BatteryPowerSourceType), $"Sensor with id {sensor.Id} " +
                                                                                     $"doesn't have any battery power source type, " +
                                                                                     $"and thus can't have voltage measurements.");
            }
            
            var batteryMeasurement = new BatteryMeasurement()
            {
                BatteryPowerSourceTypeId = sensor.BatteryPowerSourceTypeId.Value,
                MeasurementDateTime = measurementDateTime,
                SensorId = sensor.Id,
                Voltage = voltage
            };

            return await AddOrUpdateAsync(batteryMeasurement);
        }
    }
}