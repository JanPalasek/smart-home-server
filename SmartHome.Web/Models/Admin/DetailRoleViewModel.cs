using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Admin
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