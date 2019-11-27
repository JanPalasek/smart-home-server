using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class TemperatureMeasurementModel : MeasurementModel
    {
        [Required]
        public double? Temperature { get; set; }
    }
}