using System;

namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Abstract predecessor for all measurements (e.g. temperature, humidity, voltage,...).
    /// </summary>
    public abstract class Measurement : Entity
    {
        /// <summary>
        /// Unit that did this particular measurement.
        /// </summary>
        public long UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        
        /// <summary>
        /// Date and time when this measurement was performed.
        /// </summary>
        public DateTime MeasurementDateTime { get; set; }
    }
}