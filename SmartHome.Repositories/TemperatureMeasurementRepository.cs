using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class TemperatureMeasurementRepository : GenericRepository<TemperatureMeasurement>, ITemperatureMeasurementRepository
    {
        public TemperatureMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public async Task<long> AddAsync(long unitId, double temperature, DateTime measurementDateTime)
        {
            var unit = await SmartHomeAppDbContext.SingleAsync<Unit>(unitId);
            
            var temperatureMeasurement = new TemperatureMeasurement()
            {
                MeasurementDateTime = measurementDateTime,
                Temperature = temperature,
                UnitId = unit.Id
            };

            return await AddOrUpdateAsync(temperatureMeasurement);
        }

        public async Task<TemperatureMeasurementModel> GetUnitLastTemperatureMeasurementAsync(long unitId)
        {
            var query = SmartHomeAppDbContext.Query<TemperatureMeasurement>()
                .Where(x => x.UnitId == unitId);

            // take temperature measurement of given unit with maximum date time
            var lastDateTime = query.DefaultIfEmpty().Max(y => y.MeasurementDateTime);
            var temperatureMeasurement = await query.FirstOrDefaultAsync(x => x.MeasurementDateTime == lastDateTime);
            return Mapper.Map<TemperatureMeasurementModel>(temperatureMeasurement);
        }
        
        public Task<IList<TemperatureMeasurement>> GetLastTemperatureMeasurementAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter)
        {
            return await GetTemperatureMeasurementsQuery(filter).ProjectTo<TemperatureMeasurementModel>(Mapper).ToListAsync();
        }

        private IQueryable<TemperatureMeasurement> GetTemperatureMeasurementsQuery(MeasurementFilter filter)
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