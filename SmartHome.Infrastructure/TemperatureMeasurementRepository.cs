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
using SmartHome.Infrastructure.Extensions;

namespace SmartHome.Infrastructure
{
    public class TemperatureMeasurementRepository : GenericRepository<TemperatureMeasurement>, ITemperatureMeasurementRepository
    {
        public TemperatureMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public async Task<IList<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter)
        {
            var query = GetTemperatureMeasurementsQuery(filter);

            return await query.OrderByDescending(x => x.MeasurementDateTime)
                .ProjectTo<TemperatureMeasurementModel>(Mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TemperatureMeasurementModel?> GetSensorLastTemperatureMeasurementAsync(long sensorId)
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

        public async Task<long> AddOrUpdateAsync(TemperatureMeasurementModel temperatureMeasurementModel)
        {
            var temperatureMeasurement = Mapper.Map<TemperatureMeasurement>(temperatureMeasurementModel);

            return await AddOrUpdateAsync(temperatureMeasurement);
        }

        public async Task<CountedResult<TemperatureMeasurementModel>> GetTemperatureMeasurementsAsync(MeasurementFilter filter,
            PagingArgs? pagingArgs)
        {
            var query = GetTemperatureMeasurementsQuery(filter);

            int count = await query.CountAsync();
            
            query = query.OrderByDescending(x => x.MeasurementDateTime).ThenBy(x => x.Id);
            query = query.PageBy(pagingArgs);

            return new CountedResult<TemperatureMeasurementModel>(count,
                await query.ProjectTo<TemperatureMeasurementModel>(Mapper.ConfigurationProvider).ToListAsync());
        }

        public async Task<IList<TemperatureMeasurementModel>> GetAllAsync()
        {
            return await SmartHomeAppDbContext.Query<TemperatureMeasurement>()
                .ProjectTo<TemperatureMeasurementModel>(Mapper.ConfigurationProvider)
                .OrderByDescending(x => x.MeasurementDateTime)
                .ToListAsync();
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

        public async Task<TemperatureMeasurementModel> GetByIdAsync(long id)
        {
            var temperatureMeasurement = await SmartHomeAppDbContext.SingleAsync<TemperatureMeasurement>(id);
            return Mapper.Map<TemperatureMeasurementModel>(temperatureMeasurement);
        }
    }
}