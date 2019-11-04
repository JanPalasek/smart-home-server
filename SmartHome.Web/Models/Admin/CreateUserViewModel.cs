using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Admin
{
    public class CreateUserViewModel
    {
        public CreateUserViewModel(CreateUserModel model)
        {
            Model = model;
        }

        public CreateUserModel Model { get; set; }
    }
}