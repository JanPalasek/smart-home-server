using System.ComponentModel.DataAnnotations;

namespace SmartHome.Shared.Models
{
    public class UnitTypeModel : Model
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}