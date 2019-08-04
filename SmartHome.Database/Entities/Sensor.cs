using SmartHome.Shared;

namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents one sensor at home, e.g. temperature sensor nr. 1, temperature sensor nr. 2,...
    /// </summary>
    public class Sensor : Entity
    {
        /// <summary>
        /// Type of this particular sensor instance (e.g. temperature sensor DS1820, humidity sensor,...).
        /// </summary>
        public long SensorTypeId { get; set; }
        public virtual SensorType SensorType { get; set; }
        
        /// <summary>
        /// Represents current power source type of the sensor. If null, the power source is not from a battery.
        /// </summary>
        public long? BatteryPowerSourceTypeId { get; set; }
        public virtual BatteryPowerSourceType BatteryPowerSourceType { get; set; }
        
        /// <summary>
        /// Describes place where this sensor is located.
        /// </summary>
        public long? PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}