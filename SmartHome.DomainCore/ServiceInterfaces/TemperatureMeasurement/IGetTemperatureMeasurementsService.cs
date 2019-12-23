using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement
{
    public interface IGetTemperatureMeasurementsService : IGetAllRepository<TemperatureMeasurementModel>
    {
        Task<CountedResult<TemperatureMeasurementModel>> GetFilteredMeasurementsAsync(MeasurementFilter filter, PagingArgs pagingArgs);
        Task<IList<TemperatureMeasurementModel>> GetFilteredMeasurementsAsync(MeasurementFilter filter);
        Task<IList<AggregatedStatisticsModel>> GetFilteredMeasurementAsync(StatisticsFilter filter);
        Task<TemperatureMeasurementModel?> GetLastMeasurementAsync(long sensorId);
        Task<TemperatureMeasurementModel> GetTemperatureMeasurementById(long id);
    }
}