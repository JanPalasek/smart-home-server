using System.ComponentModel.DataAnnotations;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Account
{
    public class LoginViewModel
    {
        public LoginViewModel(LoginModel model)
        {
            Model = model;
        }

        public LoginModel Model { get; set; }
        
        public string? ReturnUrl { get; set; }
    }
}