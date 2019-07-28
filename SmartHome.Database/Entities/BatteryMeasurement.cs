namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Measurement by done specified <see cref="Measurement.Unit"/> of battery voltage.
    /// </summary>
    public class BatteryMeasurement : Measurement
    {
        public double Voltage { get; set; }
        
        public virtual BatteryPowerSourceType BatteryPowerSourceType { get; set; }
        /// <summary>
        /// Represents battery power source given <see cref="Measurement.Unit"/> has at the moment
        /// of this measurement.
        /// </summary>
        public long BatteryPowerSourceTypeId { get; set; }
    }
}