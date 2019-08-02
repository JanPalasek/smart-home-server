using System;

namespace SmartHome.Shared.Models
{
    public class MeasurementModel : Model
    {
        public DateTime? MeasurementDateTime { get; set; }
        public long? UnitId { get; set; }
    }
}