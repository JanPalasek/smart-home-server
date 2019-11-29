namespace SmartHome.Web.Models.BatteryPowerSourceType
{
    public class BatteryPowerSourceTypeGridItemModel
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? BatteryTypeName { get; set; }
        public double? MinimumVoltage { get; set; }
        public double? MaximumVoltage { get; set; }
    }
}