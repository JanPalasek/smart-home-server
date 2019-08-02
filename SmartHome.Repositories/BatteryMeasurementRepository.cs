using System;
using System.Threading.Tasks;
using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Database.Repositories;
using SmartHome.Repositories.Interfaces;

namespace SmartHome.Repositories
{
    public class BatteryMeasurementRepository : GenericRepository<BatteryMeasurement>, IBatteryMeasurementRepository
    {
        public BatteryMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public async Task<long> AddAsync(long unitId, double voltage, DateTime measurementDateTime)
        {
            var unit = await SmartHomeAppDbContext.SingleAsync<Unit>(unitId);
            
            if (unit.BatteryPowerSourceTypeId == null)
            {
                throw new ArgumentNullException(nameof(unit.BatteryPowerSourceType), $"Unit with id {unit.Id} " +
                                                                                     $"doesn't have any battery power source type, " +
                                                                                     $"and thus can't have voltage measurements.");
            }
            
            var batteryMeasurement = new BatteryMeasurement()
            {
                BatteryPowerSourceTypeId = unit.BatteryPowerSourceTypeId.Value,
                MeasurementDateTime = measurementDateTime,
                UnitId = unit.Id,
                Voltage = voltage
            };

            return await AddOrUpdateAsync(batteryMeasurement);
        }
    }
}