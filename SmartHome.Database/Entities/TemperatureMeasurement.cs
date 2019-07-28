namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents one measurement of temperature at given time done by given unit.
    /// </summary>
    public class TemperatureMeasurement : Measurement
    {
        public double Temperature { get; set; }
    }
}