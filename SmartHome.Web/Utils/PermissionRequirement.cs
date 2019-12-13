using Microsoft.AspNetCore.Authorization;

namespace SmartHome.Web.Utils
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; }
    }
}