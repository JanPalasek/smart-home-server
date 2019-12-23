using System.Collections.Generic;

namespace SmartHome.DomainCore.Data.Models
{
    public class AggregatedStatisticsModel
    {
        public string PlaceName { get; }
        public List<MeasurementStatisticsPlaceModel>? Values { get; }

        public AggregatedStatisticsModel(string placeName, List<MeasurementStatisticsPlaceModel>? values)
        {
            this.PlaceName = placeName;
            Values = values;
        }
    }
}