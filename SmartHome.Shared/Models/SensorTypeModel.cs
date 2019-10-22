using System.ComponentModel.DataAnnotations;

namespace SmartHome.Shared.Models
{
    public class SensorTypeModel : Model
    {
        [Required]
        public string? Name { get; set; }
        
//        [Required]
//        public MeasurementType? MeasurementTypes { get; set; }
        public string? Description { get; set; }
    }
}