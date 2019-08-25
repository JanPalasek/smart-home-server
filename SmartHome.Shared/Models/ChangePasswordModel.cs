namespace SmartHome.Shared.Models
{
    public class ChangePasswordModel : Model
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}