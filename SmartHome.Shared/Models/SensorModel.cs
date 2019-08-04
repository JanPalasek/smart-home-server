namespace SmartHome.Shared.Models
{
    public class SensorModel : Model
    {
        public long? BatteryPowerSourceTypeId { get; set; }
        public long? SensorTypeId { get; set; }
        public long? PlaceId { get; set; }
        public double? MinimumRequiredVoltage { get; set; }
    }
}