using System;

namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Abstract predecessor for all measurements that need to be logged (e.g. temperature, humidity, voltage,...).
    /// </summary>
    public abstract class Measurement : Entity
    {
        /// <summary>
        /// Sensor that did this particular measurement.
        /// </summary>
        public long SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
        
        /// <summary>
        /// Date and time when this measurement was performed.
        /// </summary>
        public DateTime MeasurementDateTime { get; set; }
        
        /// <summary>
        /// Describes place where the sensor was located at time of the measurement.
        /// </summary>
        public long PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}