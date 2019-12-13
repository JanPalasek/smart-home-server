using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Web.Utils
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionVerificationService permissionVerificationService;

        public PermissionHandler(IPermissionVerificationService permissionVerificationService)
        {
            this.permissionVerificationService = permissionVerificationService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // no user => fail
            if(context.User?.Identity?.Name == null)
            {
                context.Fail();
                return;
            }

            // check whether user has the permission
            bool hasPermission = await permissionVerificationService.HasPermissionAsync(context.User.Identity.Name, requirement.PermissionName);
            if (!hasPermission)
            {
                context.Fail();
                return;
            }
            
            context.Succeed(requirement);
        }
    }
}