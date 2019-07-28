namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents one unit type at home, e.g. temperature sensor.
    /// </summary>
    public class UnitType : Entity
    {
        /// <summary>
        /// Name of this unit, e.g. DS18B20. It has to be unique.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Text describing what this unit is supposed to do in detail.
        /// </summary>
        public virtual string Description { get; set; }
    }
}