using System;

namespace SmartHome.DomainCore.Data.Models
{
    public class MeasurementModel : Model
    {
        public DateTime? MeasurementDateTime { get; set; }
        public long? SensorId { get; set; }
        public long? PlaceId { get; set; }
    }
}