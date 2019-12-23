using System;

namespace SmartHome.DomainCore.Data.Models
{
    public class MeasurementStatisticsModel
    {
        public long? PlaceId { get; set; }
        public DateTime? MeasurementDateTime { get; set; }
        public double? Value { get; set; }
    }
}