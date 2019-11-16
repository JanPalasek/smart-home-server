using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Role
{
    public class CreateRoleViewModel
    {
        public CreateRoleViewModel(CreateRoleModel model)
        {
            Model = model;
        }

        public CreateRoleModel Model { get; set; }
    }
}