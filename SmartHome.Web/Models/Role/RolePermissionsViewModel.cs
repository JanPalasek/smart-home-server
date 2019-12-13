using System.Collections.Generic;
using System.Linq;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Role
{
    public class RolePermissionsViewModel
    {
        public long RoleId { get; set; }
        public IEnumerable<PermissionModel> PermissionOptions { get; set; } = Enumerable.Empty<PermissionModel>();

        public RolePermissionsViewModel(long roleId)
        {
            RoleId = roleId;
        }
    }
}