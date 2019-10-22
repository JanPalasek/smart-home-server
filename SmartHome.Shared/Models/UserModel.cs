namespace SmartHome.Shared.Models
{
    public class UserModel : Model
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}