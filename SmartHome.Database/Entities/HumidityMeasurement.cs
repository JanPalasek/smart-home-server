namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents one measurement of humidity by given unit at given time.
    /// </summary>
    public class HumidityMeasurement : Measurement
    {
        public double Humidity { get; set; }
    }
}