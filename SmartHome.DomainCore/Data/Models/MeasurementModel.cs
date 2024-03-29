using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class MeasurementModel : Model
    {
        [Required]
        public DateTime? MeasurementDateTime { get; set; }
        public long? SensorId { get; set; }
        public string? SensorName { get; set; }
        public long? PlaceId { get; set; }
        public string? PlaceName { get; set; }
    }
}