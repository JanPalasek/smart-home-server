using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Services.User
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUserRepository repository;
        private readonly IRoleRepository roleRepository;

        public UpdateUserService(IUserRepository repository, IRoleRepository roleRepository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserModel model)
        {
            return await repository.UpdateUserAsync(model);
        }

        public async Task<IdentityResult> AddToOrRemoveFromRolesAsync(long userId, IEnumerable<long> roleIds)
        {
            var allUserRoles = await roleRepository.GetUserRolesAsync(userId);

            var rolesToAdd = roleIds.Except(allUserRoles.Select(x => x.Id)).ToList();
            // roles that user currently has without those sent to the server
            var rolesToRemove = allUserRoles.Select(x => x.Id).Except(roleIds).ToList();
            
            if (rolesToAdd.Count > 0 && rolesToRemove.Count > 0)
            {
                // TODO: merge
                await repository.AddToRolesAsync(userId, rolesToAdd);
                return await repository.RemoveFromRolesAsync(userId, rolesToRemove);
            }
            
            return IdentityResult.Failed();
        }
    }
}