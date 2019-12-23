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

        public async Task<IList<MeasurementStatisticsModel>> GetTemperatureMeasurementsAsync(StatisticsFilter filter)
        {
            var query = SmartHomeAppDbContext.Query<TemperatureMeasurement>();

            if (filter.DateFrom != null)
            {
                query = query.Where(x => x.MeasurementDateTime >= filter.DateFrom);
            }
            if (filter.DateTo != null)
            {
                query = query.Where(x => x.MeasurementDateTime <= filter.DateTo);
            }

            IQueryable<MeasurementStatisticsModel> modelQuery;
            if (filter.GroupBy != null)
            {
                switch (filter.GroupBy)
                {
                    case GroupByType.DayOfYear:
                    {
                        modelQuery = GroupByDate(query);
                        break;
                    }
                    case GroupByType.Month:
                    {
                        modelQuery = GroupByMonth(query);
                        break;
                    }
                    case GroupByType.Year:
                    {
                        modelQuery = GroupByYear(query);
                        break;
                    }
                    case GroupByType.Place:
                    {
                        modelQuery = GroupByPlace(query);
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                modelQuery = query.Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = x.MeasurementDateTime,
                    Value = x.Temperature,
                    PlaceId = x.PlaceId
                });
            }

            var list = await modelQuery
                .ToListAsync();
            return list;
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

        private IQueryable<MeasurementStatisticsModel> GroupByDate(
            IQueryable<TemperatureMeasurement> query)
        {
            var grouped = query
                .GroupBy(x => new
                {
                    x.PlaceId,
                    // group by day of a year (but dayofyear on datetime cannot be used, it won't translate to db call)
                    x.MeasurementDateTime.Day,
                    x.MeasurementDateTime.Month
                });
                        
            var result = grouped
                .Select(x => new
                {
                    PlaceId = x.Key.PlaceId,
                    Month = x.Key.Month,
                    Day = x.Key.Day,
                    Value = x.Average(y => y.Temperature)
                })
                .Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = new DateTime(DateTime.Now.Year, x.Month, x.Day),
                    Value = x.Value,
                    PlaceId = x.PlaceId
                });
            return result;
        }
        
        private IQueryable<MeasurementStatisticsModel> GroupByMonth(
            IQueryable<TemperatureMeasurement> query)
        {
            var grouped = query
                .GroupBy(x => new
                {
                    x.PlaceId,
                    x.MeasurementDateTime.Month
                });
                        
            var result = grouped
                .Select(x => new
                {
                    PlaceId = x.Key.PlaceId,
                    Month = x.Key.Month,
                    Value = x.Average(y => y.Temperature)
                })
                .Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = new DateTime(DateTime.Now.Year, x.Month, 1),
                    Value = x.Value,
                    PlaceId = x.PlaceId
                });

            return result;
        }

        private IQueryable<MeasurementStatisticsModel> GroupByYear(
            IQueryable<TemperatureMeasurement> query)
        {
            var grouped = query
                .GroupBy(x => new
                {
                    x.PlaceId,
                    x.MeasurementDateTime.Year
                });
                        
            var result = grouped
                .Select(x => new
                {
                    Place = x.Key.PlaceId,
                    Year = x.Key.Year,
                    Value = x.Average(y => y.Temperature)
                })
                .Select(x => new MeasurementStatisticsModel()
                {
                    PlaceId = x.Place,
                    MeasurementDateTime = new DateTime(x.Year, 1, 1),
                    Value = x.Value
                });
            return result;
        }

        private IQueryable<MeasurementStatisticsModel> GroupByPlace(
            IQueryable<TemperatureMeasurement> query)
        {
            var grouped = query
                .GroupBy(x => new
                {
                    x.PlaceId,
                    x.MeasurementDateTime
                });
                        
            var result = grouped
                .Select(x => new
                {
                    Date = x.Key.MeasurementDateTime,
                    Value = x.Average(y => y.Temperature)
                })
                .Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = x.Date,
                    Value = x.Value,
                    PlaceId = null
                });

            return result;
        }
    }
}