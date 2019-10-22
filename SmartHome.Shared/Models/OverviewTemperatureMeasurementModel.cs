using System;

namespace SmartHome.Shared.Models
{
    public class OverviewTemperatureMeasurementModel
    {
        public string? SensorTypeName { get; set; }
        public string? PlaceName { get; set; }
        public bool IsInside { get; set; }
        public double? Temperature { get; set; }
    }
}