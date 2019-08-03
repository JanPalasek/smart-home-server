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

        public async Task<long> AddAsync(long sensorId, double temperature, DateTime measurementDateTime)
        {
            var sensor = await SmartHomeAppDbContext.SingleAsync<Sensor>(sensorId);
            
            var temperatureMeasurement = new TemperatureMeasurement()
            {
                MeasurementDateTime = measurementDateTime,
                Temperature = temperature,
                SensorId = sensor.Id
            };

            return await AddOrUpdateAsync(temperatureMeasurement);
        }

        public async Task<TemperatureMeasurementModel> GetSensorLastTemperatureMeasurementAsync(long sensorId)
        {
            var query = SmartHomeAppDbContext.Query<TemperatureMeasurement>()
                .Where(x => x.SensorId == sensorId);

            // take temperature measurement of given sensor with maximum date time
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

            if (filter.SensorId != null)
            {
                query = query.Where(x => x.SensorId == filter.SensorId.Value);
            }

            return query;
        }
    }
}