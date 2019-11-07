using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class CreateUserModel
    {
        [Required]
        public string? UserName { get; set; }
        
        [Required]
        public string? Email { get; set; }
        
        [Required]
        public string? Password { get; set; }
    }
}