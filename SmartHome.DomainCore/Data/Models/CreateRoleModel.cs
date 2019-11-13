using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string? Name { get; set; }
    }
}