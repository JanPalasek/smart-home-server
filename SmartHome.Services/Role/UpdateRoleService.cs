using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Role;

namespace SmartHome.Services.Role
{
    public class UpdateRoleService : IUpdateRoleService
    {
        private readonly IRoleRepository repository;

        public UpdateRoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IdentityResult> UpdateRoleAsync(RoleModel model)
        {
            return await repository.UpdateRoleAsync(model);
        }
    }
}