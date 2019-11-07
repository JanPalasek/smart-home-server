using System;

namespace SmartHome.DomainCore.Data
{
    [Flags]
    public enum MeasurementType
    {
        Temperature = 1,
        Humidity = Temperature << 1,
        Voltage = Humidity << 1
    }
}