
using SmartHome.DomainCore.Data;

namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents battery power source. type
    /// </summary>
    public class BatteryPowerSourceType : Entity
    {
        public string Name { get; set; }
        /// <summary>
        /// Represents type of battery used by this power source.
        /// </summary>
        public BatteryType BatteryType { get; set; }
        
        /// <summary>
        /// Represents minimum voltage this source can have.
        /// </summary>
        public double MinimumVoltage { get; set; }
        
        /// <summary>
        /// Represents maximum voltage this source can have.
        /// </summary>
        public double MaximumVoltage { get; set; }
    }
}