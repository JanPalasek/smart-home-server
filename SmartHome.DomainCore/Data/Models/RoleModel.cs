using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class RoleModel : Model
    {
        [Required]
        public string? Name { get; set; }
    }
}