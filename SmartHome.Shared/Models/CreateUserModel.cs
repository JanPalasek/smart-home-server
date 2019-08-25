using System.ComponentModel.DataAnnotations;

namespace SmartHome.Shared.Models
{
    public class CreateUserModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}