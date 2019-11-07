namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents one sensor type at home, e.g. temperature sensor.
    /// </summary>
    public class SensorType : Entity
    {
        /// <summary>
        /// Name of this sensor, e.g. DS18B20. It has to be unique.
        /// </summary>
        public string Name { get; set; }
        
//        /// <summary>
//        /// Describes types that the sensor can measure.
//        /// </summary>
//        public MeasurementType MeasurementTypes { get; set; }
        
        /// <summary>
        /// Text describing what this sensor is supposed to do in detail.
        /// </summary>
        public virtual string? Description { get; set; }
    }
}