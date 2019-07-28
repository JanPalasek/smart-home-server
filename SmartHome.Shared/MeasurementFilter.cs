using System;

namespace SmartHome.Shared
{
    /// <summary>
    /// Filter for all kinds of measurement.
    /// </summary>
    public class MeasurementFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        
        /// <summary>
        /// Id of unit that we want to take.
        /// </summary>
        public long? UnitId { get; set; }
    }
}