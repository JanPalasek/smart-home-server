using System;

namespace SmartHome.Shared
{
    [Flags]
    public enum MeasurementType
    {
        Temperature = 1,
        Humidity = Temperature << 1,
        Voltage = Humidity << 1
    }
}