using System;

namespace SmartHome.DomainCore.Data.Models
{
    public class MeasurementStatisticsPlaceModel
    {
        public DateTime? MeasurementDateTime { get; }
        public double? Value { get; }

        public MeasurementStatisticsPlaceModel(DateTime? measurementDateTime, double? value)
        {
            MeasurementDateTime = measurementDateTime;
            Value = value;
        }
    }
}