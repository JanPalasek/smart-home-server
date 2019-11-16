using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Role
{
    public class DetailRoleViewModel
    {
        public RoleModel Model { get; set; }

        public DetailRoleViewModel(RoleModel model)
        {
            this.Model = model;
        }
    }
}