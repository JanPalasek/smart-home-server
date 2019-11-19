using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class SensorModel : Model
    {
        [Required]
        public long? BatteryPowerSourceTypeId { get; set; }
        [Required]
        public long? SensorTypeId { get; set; }
        [Required]
        public long? PlaceId { get; set; }
        [Required]
        public double? MinimumRequiredVoltage { get; set; }
    }
}