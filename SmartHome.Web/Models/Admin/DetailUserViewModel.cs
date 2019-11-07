using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Admin
{
    public class DetailUserViewModel : SmartHomeViewModel<UserModel>
    {
        public DetailUserViewModel(UserModel model) : base(model)
        {
        }
    }
}