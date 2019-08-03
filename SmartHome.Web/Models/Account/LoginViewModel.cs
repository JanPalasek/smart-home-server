using System.ComponentModel.DataAnnotations;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Account
{
    public class LoginViewModel
    {
        public LoginModel Model { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}