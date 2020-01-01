using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartHome.Common;
using SmartHome.Common.DateTimeProviders;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.Infrastructure.Extensions;

namespace SmartHome.Infrastructure
{
    public class TemperatureMeasurementRepository : GenericRepository<TemperatureMeasurement>, ITemperatureMeasurementRepository
    {
        private readonly IDateTimeProvider dateTimeProvider;
        public TemperatureMeasurementRepository(SmartHomeAppDbContext smartHomeAppDbContext,
            IMapper mapper, IDateTimeProvider dateTimeProvider) : base(smartHomeAppDbContext, mapper)
        {
            this.dateTimeProvider = dateTimeProvider;
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

            if (filter.IsInside != null)
            {
                query = query.Where(x => x.Place.IsInside == filter.IsInside.Value);
            }

            if (filter.DateFrom != null)
            {
                query = query.Where(x => x.MeasurementDateTime >= filter.DateFrom);
            }
            if (filter.DateTo != null)
            {
                query = query.Where(x => x.MeasurementDateTime <= filter.DateTo);
            }

            IEnumerable<MeasurementStatisticsModel> modelQuery;
            switch (filter.AggregateOver)
            {
                case AggregateOver.DayOfYear:
                {
                    modelQuery = AggregateOverDayOfYear(query, filter.AggregateOverPlace);
                    break;
                }
                case AggregateOver.Month:
                {
                    modelQuery = AggregateOverMonth(query, filter.AggregateOverPlace);
                    break;
                }
                case AggregateOver.Year:
                {
                    modelQuery = AggregateOverYear(query, filter.AggregateOverPlace);
                    break;
                }
                case null:
                {
                    IQueryable<Tuple<long?, DateTime, int, double>> groupedQuery;
                    if (!filter.AggregateOverPlace)
                    {
                        groupedQuery = query
                            // make groups that have same date + hour and place and make temperature average over them
                            .GroupBy(x => new {x.MeasurementDateTime.Date, x.MeasurementDateTime.Hour, x.PlaceId})
                            .Select(x => Tuple.Create((long?) x.Key.PlaceId, x.Key.Date, x.Key.Hour,
                                x.Average(y => y.Temperature)));
                    }
                    else
                    {
                        groupedQuery = query
                            // make groups that have same date + hour and place and make temperature average over them
                            .GroupBy(x => new {x.MeasurementDateTime.Date, x.MeasurementDateTime.Hour})
                            .Select(x =>
                                Tuple.Create((long?) null, x.Key.Date, x.Key.Hour, x.Average(y => y.Temperature)));
                    }

                    modelQuery = groupedQuery.AsEnumerable()
                        .Select(x => new MeasurementStatisticsModel()
                        {
                            MeasurementDateTime = new DateTime(x.Item2.Year, x.Item2.Month, x.Item2.Day, x.Item3, 0, 0),
                            Value = x.Item4,
                            PlaceId = x.Item1
                        });
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var list = modelQuery
                .ToList();
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

        private IEnumerable<MeasurementStatisticsModel> AggregateOverDayOfYear(
            IQueryable<TemperatureMeasurement> query, bool aggregateOverPlace)
        {
            IQueryable<Tuple<long?, int, int, double>> groupedQuery;
            if (!aggregateOverPlace)
            {
                groupedQuery = query.GroupBy(x => new
                    {
                        x.PlaceId,
                        // group by day of a year (but dayofyear on datetime cannot be used, it won't translate to db call)
                        x.MeasurementDateTime.Day,
                        x.MeasurementDateTime.Month
                    })
                    .Select(x => Tuple.Create(
                        (long?)x.Key.PlaceId, x.Key.Month, x.Key.Day, x.Average(y => y.Temperature)));
            }
            else
            {
                groupedQuery = query
                    .GroupBy(x => new
                    {
                        // group by day of a year (but dayofyear on datetime cannot be used, it won't translate to db call)
                        x.MeasurementDateTime.Day,
                        x.MeasurementDateTime.Month
                    })
                    .Select(x => Tuple.Create(
                        (long?)null, x.Key.Month, x.Key.Day, x.Average(y => y.Temperature)));
            }
                        
            var result = groupedQuery
                .AsEnumerable()
                .Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = new DateTime(dateTimeProvider.Now.Year, x.Item2, x.Item3),
                    Value = x.Item4,
                    PlaceId = x.Item1
                });
            return result;
        }
        
        private IEnumerable<MeasurementStatisticsModel> AggregateOverMonth(
            IQueryable<TemperatureMeasurement> query, bool aggregateOverPlace)
        {
            IQueryable<Tuple<long?, int, double>> groupedQuery;
            if (!aggregateOverPlace)
            {
                groupedQuery = query.GroupBy(x => new
                    {
                        x.PlaceId,
                        x.MeasurementDateTime.Month
                    })
                    .Select(x => Tuple.Create(
                        (long?)x.Key.PlaceId, x.Key.Month, x.Average(y => y.Temperature)));
            }
            else
            {
                groupedQuery = query
                    .GroupBy(x => new
                    {
                        x.MeasurementDateTime.Month
                    })
                    .Select(x => Tuple.Create(
                        (long?)null, x.Key.Month, x.Average(y => y.Temperature)));
            }
                        
            var result = groupedQuery
                .AsEnumerable()
                .Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = new DateTime(dateTimeProvider.Now.Year, x.Item2, 1),
                    Value = x.Item3,
                    PlaceId = x.Item1
                });
            return result;
        }

        private IEnumerable<MeasurementStatisticsModel> AggregateOverYear(
            IQueryable<TemperatureMeasurement> query, bool aggregateOverPlace)
        {
            IQueryable<Tuple<long?, int, double>> groupedQuery;
            if (!aggregateOverPlace)
            {
                groupedQuery = query.GroupBy(x => new
                    {
                        x.PlaceId,
                        x.MeasurementDateTime.Year
                    })
                    .Select(x => Tuple.Create(
                        (long?)x.Key.PlaceId, x.Key.Year, x.Average(y => y.Temperature)));
            }
            else
            {
                groupedQuery = query
                    .GroupBy(x => new
                    {
                        x.MeasurementDateTime.Year
                    })
                    .Select(x => Tuple.Create(
                        (long?)null, x.Key.Year, x.Average(y => y.Temperature)));
            }
                        
            var result = groupedQuery
                .AsEnumerable()
                .Select(x => new MeasurementStatisticsModel()
                {
                    MeasurementDateTime = new DateTime(x.Item2, 1, 1),
                    Value = x.Item3,
                    PlaceId = x.Item1
                });
            return result;
        }
    }
}