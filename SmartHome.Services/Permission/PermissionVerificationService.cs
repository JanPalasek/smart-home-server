using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;

namespace SmartHome.Services.Permission
{
    public class PermissionVerificationService : IPermissionVerificationService
    {
        private readonly IUserRepository userRepository;

        public PermissionVerificationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> HasPermissionAsync(string userName, string permissionName)
        {
            var user = await userRepository.GetUserByNameAsync(userName);
            if (user == null)
            {
                return false;
            }
            
            return await userRepository.HasPermissionAsync(user.Id, permissionName);
        }

        public bool HasPermission(string userName, string permissionName)
        {
            var user = userRepository.GetUserByNameAsync(userName).Result;
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Invalid user name.");
            }
            return userRepository.HasPermissionAsync(user.Id, permissionName).Result;
        }
    }
}