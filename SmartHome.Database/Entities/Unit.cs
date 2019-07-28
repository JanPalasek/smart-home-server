namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents one unit at home, e.g. temperature sensor nr. 1, temperature sensor nr. 2,...
    /// </summary>
    public class Unit : Entity
    {
        /// <summary>
        /// Type of this particular unit instance (e.g. temperature sensor DS1820, humidity sensor,...).
        /// </summary>
        public long UnitTypeId { get; set; }
        public virtual UnitType UnitType { get; set; }
        
        /// <summary>
        /// Represents current power source type of the unit. If null, the power source is not from a battery.
        /// </summary>
        public long? BatteryPowerSourceTypeId { get; set; }
        public virtual BatteryPowerSourceType BatteryPowerSourceType { get; set; }
    }
}