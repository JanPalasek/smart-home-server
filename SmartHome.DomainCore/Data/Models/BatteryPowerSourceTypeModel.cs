using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class BatteryPowerSourceTypeModel : Model
    {
        [Required]
        public string? Name { get; set; }
        /// <summary>
        /// Represents type of battery used by this power source.
        /// </summary>
        [Required]
        public BatteryType? BatteryType { get; set; }
        
        /// <summary>
        /// Represents minimum voltage this source can have.
        /// </summary>
        [Required]
        public double? MinimumVoltage { get; set; }
        
        /// <summary>
        /// Represents maximum voltage this source can have.
        /// </summary>
        [Required]
        public double? MaximumVoltage { get; set; }
    }
}