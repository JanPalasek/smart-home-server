using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class SensorModel : Model
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public long? BatteryPowerSourceTypeId { get; set; }
        public string? BatteryPowerSourceTypeName { get; set; }
        [Required]
        public long? SensorTypeId { get; set; }
        public string? SensorTypeName { get; set; }
        [Required]
        public long? PlaceId { get; set; }
        public string? PlaceName { get; set; }
        [Required]
        public double? MinimumRequiredVoltage { get; set; }
    }
}