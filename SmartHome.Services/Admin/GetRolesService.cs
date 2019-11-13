using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Admin;

namespace SmartHome.Services.Admin
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IRoleRepository repository;

        public GetRolesService(
            IRoleRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IList<RoleModel>> GetAllRolesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<RoleModel> GetRoleByIdAsync(long roleId)
        {
            return await repository.GetByIdAsync(roleId);
        }

        public Task<RoleModel> GetRoleByNameAsync(string name)
        {
            return repository.GetByNameAsync(name);
        }

        public async Task<IList<RoleModel>> GetUserRolesAsync(long userId)
        {
            return await repository.GetUserRolesAsync(userId);
        }
    }
}