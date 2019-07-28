using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHome.Database.Entities;
using SmartHome.Shared;

namespace SmartHome.Database.Repositories
{
    public class TemperatureMeasurementRepository : GenericRepository<TemperatureMeasurement>, ITemperatureMeasurementRepository
    {
        public TemperatureMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext) : base(smartHomeAppDbContext)
        {
        }

        public async Task<long> AddAsync(long unitId, double temperature)
        {
            var unit = await SmartHomeAppDbContext.SingleAsync<Unit>(unitId);
            
            var temperatureMeasurement = new TemperatureMeasurement()
            {
                MeasurementDateTime = DateTime.Now,
                Temperature = temperature,
                UnitId = unit.Id
            };

            return await AddOrUpdateAsync(temperatureMeasurement);
        }

        public async Task<IList<TemperatureMeasurement>> GetTemperatureMeasurements(MeasurementFilter filter)
        {
            return await GetTemperatureMeasurementsQuery(filter).ToListAsync();
        }

        public IQueryable<TemperatureMeasurement> GetTemperatureMeasurementsQuery(MeasurementFilter filter)
        {
            var query = SmartHomeAppDbContext.Query<TemperatureMeasurement>();

            if (filter.From != null)
            {
                query = query.Where(x => x.MeasurementDateTime >= filter.From);
            }

            if (filter.To != null)
            {
                query = query.Where(x => x.MeasurementDateTime <= filter.To.Value);
            }

            if (filter.UnitId != null)
            {
                query = query.Where(x => x.UnitId == filter.UnitId.Value);
            }

            return query;
        }
    }
}