using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SmartHome.Common;
using SmartHome.Common.DateTimeProviders;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.Shared.Tests;
using Syncfusion.EJ2.Linq;

namespace SmartHome.Infrastructure.Tests
{
    [TestFixture]
    public class TemperatureMeasurementRepositoryTests : DatabaseTests
    {
        private TemperatureMeasurementRepository temperatureMeasurementRepository;
        private Place place;
        private IEqualityComparer<MeasurementStatisticsModel> equalityComparer;
        
        private static IEnumerable BindModelTestCases
		{
			get
			{
                yield return new TestCaseData(
                    new Func<IDateTimeProvider, StatisticsFilter>(dateTimeProvider => new StatisticsFilter()
                    {
                        AggregateOver = AggregateOver.DayOfYear,
                        DateFrom = dateTimeProvider.Today.AddMonths(-6)
                    }),
                    new Func<IDateTimeProvider, List<MeasurementStatisticsModel>>(
                        dateTimeProvider => new List<MeasurementStatisticsModel>()
                    {
                        new MeasurementStatisticsModel()
                        {
                            MeasurementDateTime = dateTimeProvider.Today.AddDays(-3),
                            Value = 21
                        },
                        new MeasurementStatisticsModel()
                        {
                            MeasurementDateTime = dateTimeProvider.Today.AddDays(-2),
                            Value = 25
                        },
                        new MeasurementStatisticsModel()
                        {
                            MeasurementDateTime = dateTimeProvider.Today.AddDays(-1),
                            Value = 27
                        }
                    })
                );
                
                yield return new TestCaseData(
                    new Func<IDateTimeProvider, StatisticsFilter>(dateTimeProvider => new StatisticsFilter()
                    {
                        AggregateOver = AggregateOver.Month,
                        DateFrom = dateTimeProvider.Today.AddMonths(-6)
                    }),
                    new Func<IDateTimeProvider, List<MeasurementStatisticsModel>>(
                        dateTimeProvider => new List<MeasurementStatisticsModel>()
                    {
                        new MeasurementStatisticsModel()
                        {
                            MeasurementDateTime = new DateTime(dateTimeProvider.Today.Year, dateTimeProvider.Today.Month, 1),
                            Value = (20 + 22 + 24 + 25 + 30) / 5f
                        }
                    })
                );
                
                yield return new TestCaseData(
                    new Func<IDateTimeProvider, StatisticsFilter>(dateTimeProvider => new StatisticsFilter()
                    {
                        AggregateOver = AggregateOver.Year
                    }),
                    new Func<IDateTimeProvider, List<MeasurementStatisticsModel>>(
                        dateTimeProvider => new List<MeasurementStatisticsModel>()
                        {
                            new MeasurementStatisticsModel()
                            {
                                MeasurementDateTime = new DateTime(dateTimeProvider.Today.AddYears(-3).Year, 1, 1),
                                Value = 30
                            },
                            new MeasurementStatisticsModel()
                            {
                                MeasurementDateTime = new DateTime(dateTimeProvider.Today.AddYears(-2).Year, 1, 1),
                                Value = 24
                            },
                            new MeasurementStatisticsModel()
                            {
                                MeasurementDateTime = new DateTime(dateTimeProvider.Today.Year, 1, 1),
                                Value = (20 + 22 + 24 + 25 + 30) / 5f
                            }
                        })
                );
			}
		}
        
        [SetUp]
        public async Task SetUp()
        {
            equalityComparer = EqualityComparerFactory.Create<MeasurementStatisticsModel>(x => x.MeasurementDateTime.Value!.GetHashCode(),
                (x, y) => Math.Abs(x.Value.Value - y.Value.Value) < 10e-5 &&
                          x.MeasurementDateTime == y.MeasurementDateTime);
            
            var sensorType = await GetAnyAsync<SensorType>();
            var batteryPowerSourceType = await GetAnyAsync<BatteryPowerSourceType>();
            place = await GetAnyAsync<Place>();
            var sensor = new Sensor()
            {
                BatteryPowerSourceTypeId = batteryPowerSourceType.Id,
                PlaceId = place.Id,
                SensorTypeId = sensorType.Id,
                MinimumRequiredVoltage = 3
            };
            DbContext.Add(sensor);
            
            var measurement1 = new TemperatureMeasurement()
            {
                MeasurementDateTime = DateTimeProvider.Today.AddDays(-3),
                PlaceId = place.Id,
                SensorId = sensor.Id,
                Temperature = 20
            };
            DbContext.Add(measurement1);
            var measurement2 = new TemperatureMeasurement()
            {
                MeasurementDateTime = DateTimeProvider.Today.AddDays(-3),
                PlaceId = place.Id,
                SensorId = sensor.Id,
                Temperature = 22
            };
            DbContext.Add(measurement2);
            var measurement3 = new TemperatureMeasurement()
            {
                MeasurementDateTime = DateTimeProvider.Today.AddDays(-2),
                PlaceId = place.Id,
                SensorId = sensor.Id,
                Temperature = 25
            };
            DbContext.Add(measurement3);
            var measurement4 = new TemperatureMeasurement()
            {
                MeasurementDateTime = DateTimeProvider.Today.AddDays(-1),
                PlaceId = place.Id,
                SensorId = sensor.Id,
                Temperature = 30
            };
            DbContext.Add(measurement4);
            var measurement5 = new TemperatureMeasurement()
            {
                MeasurementDateTime = DateTimeProvider.Today.AddDays(-1),
                PlaceId = place.Id,
                SensorId = sensor.Id,
                Temperature = 24
            };
            DbContext.Add(measurement5);
            
            await DbContext.SaveChangesAsync();
            
            var appDbContext = new SmartHomeAppDbContext(DbContext);
            
            temperatureMeasurementRepository = new TemperatureMeasurementRepository(appDbContext, Mapper, DateTimeProvider);
        }

        [Test]
        [TestCaseSource(nameof(BindModelTestCases))]
        public async Task GetTemperatureMeasurementsAsyncTest(
            Func<IDateTimeProvider, StatisticsFilter> statisticsFilterBuilder,
            Func<IDateTimeProvider, List<MeasurementStatisticsModel>> expectedTemperaturesBuilder)
        {
            var filter = statisticsFilterBuilder(DateTimeProvider);
            var expectedTemperatures = expectedTemperaturesBuilder(DateTimeProvider);
            var result = await temperatureMeasurementRepository.GetTemperatureMeasurementsAsync(filter);
            result = result.OrderBy(x => x.MeasurementDateTime).ToList();
            
            Assert.That(result.Count, Is.EqualTo(expectedTemperatures.Count));
            Assert.That(result, Is.EqualTo(expectedTemperatures).AsCollection.Using(equalityComparer));
        }
    }
}