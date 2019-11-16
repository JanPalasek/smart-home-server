using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Role;

namespace SmartHome.Services.Role
{
    public class CreateRoleService : ICreateRoleService
    {
        private readonly IRoleRepository roleRepository;

        public CreateRoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleModel model)
        {
            return await roleRepository.CreateRoleAsync(model);
        }
    }
}