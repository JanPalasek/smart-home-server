using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Admin
{
    public class ChangePasswordViewModel : SmartHomeViewModel<ChangePasswordModel>
    {
        public ChangePasswordViewModel(ChangePasswordModel model) : base(model)
        {
        }
    }
}