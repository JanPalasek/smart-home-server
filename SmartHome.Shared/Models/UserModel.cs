namespace SmartHome.Shared.Models
{
    public class UserModel : Model
    {
        /// <summary>Gets or sets the user name for this user.</summary>
        public string UserName { get; set; }

        /// <summary>Gets or sets the email address for this user.</summary>
        public string Email { get; set; }
    }
}