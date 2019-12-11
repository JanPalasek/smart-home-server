using System.Collections.Generic;
using System.Linq;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.User
{
    public class UserPermissionsViewModel
    {
        public long UserId { get; set; }
        public IEnumerable<PermissionModel> PermissionOptions { get; set; } = Enumerable.Empty<PermissionModel>();

        public UserPermissionsViewModel(long userId)
        {
            UserId = userId;
        }
    }
}