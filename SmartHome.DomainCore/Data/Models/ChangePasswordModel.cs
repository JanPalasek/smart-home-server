using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class ChangePasswordModel : Model
    {
        [Required]
        public string? OldPassword { get; set; }
        
        [Required]
        public string? NewPassword { get; set; }
    }
}