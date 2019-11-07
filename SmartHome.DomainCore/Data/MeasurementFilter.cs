using System;

namespace SmartHome.DomainCore.Data
{
    /// <summary>
    /// Filter for all kinds of measurement.
    /// </summary>
    public class MeasurementFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        
        /// <summary>
        /// Id of sensor that we want to take.
        /// </summary>
        public long? SensorId { get; set; }
    }
}