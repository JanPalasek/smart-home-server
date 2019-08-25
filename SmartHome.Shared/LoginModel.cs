using System.ComponentModel.DataAnnotations;

namespace SmartHome.Shared
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}