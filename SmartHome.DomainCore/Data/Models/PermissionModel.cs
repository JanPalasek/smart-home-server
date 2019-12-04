using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class PermissionModel : Model
    {
        [Required]
        public string? Name { get; set; }
    }
}