using System;
using System.Threading.Tasks;
using SmartHome.Database.Entities;

namespace SmartHome.Database.Repositories
{
    public class BatteryMeasurementRepository : GenericRepository<BatteryMeasurement>, IBatteryMeasurementRepository
    {
        public BatteryMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext) : base(smartHomeAppDbContext)
        {
        }

        public async Task<long> AddAsync(long unitId, double voltage)
        {
            var unit = await SmartHomeAppDbContext.SingleAsync<Unit>(unitId);
            
            // TODO: it has to be check somewhere, MCU can send battery info all the time but not be on the battery at the same moment
            if (unit.BatteryPowerSourceTypeId == null)
            {
                throw new ArgumentNullException(nameof(unit.BatteryPowerSourceType), $"Unit with id {unit.Id} " +
                                                                                     $"doesn't have any battery power source type, " +
                                                                                     $"and thus can't have voltage measurements.");
            }
            
            var batteryMeasurement = new BatteryMeasurement()
            {
                BatteryPowerSourceTypeId = unit.BatteryPowerSourceTypeId.Value,
                MeasurementDateTime = DateTime.Now,
                UnitId = unit.Id,
                Voltage = voltage
            };

            return await AddOrUpdateAsync(batteryMeasurement);
        }
    }
}