using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Services.TemperatureMeasurement
{
    public class GetTemperatureMeasurementsService : IGetTemperatureMeasurementsService
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;
        private readonly IPlaceRepository placeRepository;

        public GetTemperatureMeasurementsService(
            ITemperatureMeasurementRepository temperatureMeasurementRepository,
            IPlaceRepository placeRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
            this.placeRepository = placeRepository;
        }

        public Task<CountedResult<TemperatureMeasurementModel>> GetFilteredMeasurementsAsync(
            MeasurementFilter filter, PagingArgs pagingArgs)
        {
            return temperatureMeasurementRepository.GetTemperatureMeasurementsAsync(filter, pagingArgs);
        }

        public async Task<IList<TemperatureMeasurementModel>> GetFilteredMeasurementsAsync(MeasurementFilter filter)
        {
            return await temperatureMeasurementRepository.GetTemperatureMeasurementsAsync(filter);
        }

        public async Task<IList<AggregatedStatisticsModel>> GetFilteredMeasurementAsync(StatisticsFilter filter)
        {
            // if filter group by is not set => don't filter by date
            filter = filter.Clone();
            if (filter.AggregateOver != null)
            {
                filter.DateFrom = null;
                filter.DateTo = null;
            }
            
            var places = (await placeRepository.GetAllAsync())
                .ToDictionary(x => x.Id, x => x.Name);
            
            // TODO: Issue #5 - solve many entries (reduce their number by default grouping into (day, month, year, hour)
            // TODO: for all (grouping by hour cleans data a lot)
            var result = await GetAggregatedStatisticModelAsync(places, filter);
            return result;
        }

        private async Task<IList<AggregatedStatisticsModel>> GetAggregatedStatisticModelAsync(Dictionary<long, string> places, StatisticsFilter filter)
        {
            var results = await temperatureMeasurementRepository
                .GetTemperatureMeasurementsAsync(filter);
            var grouped = results
                .GroupBy(x => x.PlaceId)
                .Select(x => new
                {
                    Place = x.Key,
                    Values = x.OrderByDescending(y => y.MeasurementDateTime)
                        .Select(y => new MeasurementStatisticsPlaceModel(y.MeasurementDateTime,
                            y.Value)).ToList()
                })
                .ToList();
            var result = new List<AggregatedStatisticsModel>();
            foreach (var item in grouped)
            {
                var placeName = item.Place == null ? "All" : places[item.Place.Value];
                result.Add(new AggregatedStatisticsModel(placeName, item.Values));
            }

            result = result.OrderBy(x => x.PlaceName).ToList();
            return result;
        }

        public async Task<IList<TemperatureMeasurementModel>> GetAllAsync()
        {
            return await temperatureMeasurementRepository.GetAllAsync();
        }

        public Task<TemperatureMeasurementModel?> GetLastMeasurementAsync(long sensorId)
        {
            return temperatureMeasurementRepository.GetSensorLastTemperatureMeasurementAsync(sensorId);
        }

        public async Task<TemperatureMeasurementModel> GetTemperatureMeasurementById(long id)
        {
            return await temperatureMeasurementRepository.GetByIdAsync(id);
        }
    }
}