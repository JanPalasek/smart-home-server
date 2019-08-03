using System.ComponentModel.DataAnnotations;

namespace SmartHome.Shared
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        [UIHint("pass")]
        public string Password { get; set; }
    }
}