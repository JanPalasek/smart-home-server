using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;

namespace SmartHome.Services.Permission
{
    public class GetPermissionsService : IGetPermissionsService
    {
        private readonly IPermissionRepository repository;
        private readonly IUserRepository userRepository;

        public GetPermissionsService(IPermissionRepository repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public Task<IList<PermissionModel>> GetUserOnlyPermissionsAsync(long userId)
        {
            return repository.GetUserOnlyPermissionsAsync(userId);
        }

        public async Task<IList<PermissionModel>> GetRolePermissionsAsync(long roleId)
        {
            return await repository.GetRolePermissionsAsync(roleId);
        }

        public Task<IList<PermissionModel>> GetAllPermissionsAsync()
        {
            return repository.GetAllAsync();
        }

        public async Task<PermissionModel> GetByIdAsync(long id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<IList<PermissionRoleModel>> GetAllUserPermissionsWithRolesAsync(long userId)
        {
            return await repository.GetAllUserPermissionsWithRolesAsync(userId);
        }

        public async Task<IList<PermissionModel>> GetAllUserPermissionsAsync(string userName)
        {
            var user = await userRepository.GetUserByNameAsync(userName);

            if (user == null)
            {
                throw new ArgumentException($"{nameof(userName)} is not valid user name.");
            }
            
            return await repository.GetAllUserPermissionsDistinctAsync(user.Id);
        }
    }
}