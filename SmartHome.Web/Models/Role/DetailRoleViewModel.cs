using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Role
{
    public class DetailRoleViewModel
    {
        public IList<PermissionModel> Permissions { get; set; }
        public RoleModel Model { get; set; }

        public DetailRoleViewModel(RoleModel model, IList<PermissionModel> permissions)
        {
            this.Model = model;
            Permissions = permissions;
        }
    }
}