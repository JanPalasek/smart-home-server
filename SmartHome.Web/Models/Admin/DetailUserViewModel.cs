using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Admin
{
    public class DetailUserViewModel
    {
        public UserModel Model { get; set; }
        public DetailUserViewModel(UserModel model)
        {
            Model = model;
        }
    }
}