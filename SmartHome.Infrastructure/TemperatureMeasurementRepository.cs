using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
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

        public async Task<IList<OverviewTemperatureMeasurementModel>> GetAllSensorsLastTemperatureMeasurementAsync()
        {
            var orderedMeasurements = from item in SmartHomeAppDbContext.Query<TemperatureMeasurement>()
                orderby item.MeasurementDateTime descending
                select item;

            // TODO: ToListAsync() because EF Core 2.2 cannot translate GroupBy
            var query = await orderedMeasurements
                .Include(x => x.Place)
                .Include(x => x.Sensor)
                .ThenInclude(x => x.SensorType)
                .ToListAsync();

            var groupedMeasurements = query.GroupBy(x => x.SensorId);
            
            // last temperature measurements for each sensor
            var lastTemperatureMeasurements = groupedMeasurements.Select(x => x.First());

            return lastTemperatureMeasurements.Select(x => Mapper.Map<OverviewTemperatureMeasurementModel>(x)).ToList();
        }

        public async Task<IList<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter)
        {
            return await GetTemperatureMeasurementsQuery(filter).ProjectTo<TemperatureMeasurementModel>(Mapper.ConfigurationProvider).ToListAsync();
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