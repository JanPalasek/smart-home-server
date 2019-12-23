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

            var places = (await placeRepository.GetAllAsync())
                .ToDictionary(x => x.Id, x => x.Name);
            
            var result = new List<AggregatedStatisticsModel>();
            foreach (var item in grouped)
            {
                string? placeName;
                if (item.Place == null)
                {
                    placeName = "All";
                }
                else
                {
                    placeName = places[item.Place.Value];
                }
                result.Add(new AggregatedStatisticsModel(placeName, item.Values));
            }

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