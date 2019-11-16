using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class SensorModel : Model
    {
        public long? BatteryPowerSourceTypeId { get; set; }
        public long? SensorTypeId { get; set; }
        public long? PlaceId { get; set; }
        [Required]
        public double? MinimumRequiredVoltage { get; set; }
    }
}