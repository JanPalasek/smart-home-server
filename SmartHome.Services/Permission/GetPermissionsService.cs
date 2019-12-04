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

        public GetPermissionsService(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        public Task<IList<PermissionModel>> GetPermissionsAsync(long userId)
        {
            return repository.GetUserPermissionsAsync(userId);
        }

        public Task<IList<PermissionModel>> GetAllPermissionsAsync()
        {
            return repository.GetAllAsync();
        }

        public async Task<PermissionModel> GetByIdAsync(long id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}