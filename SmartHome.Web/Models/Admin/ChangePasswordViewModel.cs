using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Admin
{
    public class ChangePasswordViewModel
    {
        public ChangePasswordModel Model { get; set; }
        public ChangePasswordViewModel(ChangePasswordModel model)
        {
            Model = model;
        }
    }
}