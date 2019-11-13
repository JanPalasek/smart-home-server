using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class RoleRepository : StandardRepository<Role, RoleModel>, IRoleRepository
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;
        
        public RoleRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper,
            RoleManager<Role> roleManager, UserManager<User> userManager) : base(smartHomeAppDbContext, mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleModel model)
        {
            var role = Mapper.Map<Role>(model);
            
            var result = await roleManager.CreateAsync(role);

            return result;
        }

        public async Task<IdentityResult> UpdateRoleAsync(RoleModel model)
        {
            var role = await SingleAsync(model.Id);
            
            role.Name = model.Name;
            role.NormalizedName = model.Name;

            return await roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(long roleId)
        {
            var role = await SmartHomeAppDbContext.SingleAsync<Role>(roleId);

            return await roleManager.DeleteAsync(role);
        }

        public async Task<IList<EntityReference>> GetAllRoleReferences()
        {
            return await SmartHomeAppDbContext.Query<Role>().ProjectTo<EntityReference>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<RoleModel> GetByNameAsync(string name)
        {
            var role = await SmartHomeAppDbContext.Query<Role>().SingleAsync(x => x.Name == name);
            return Mapper.Map<RoleModel>(role);
        }
        
        public async Task<IList<RoleModel>> GetUserRolesAsync(long userId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);

            var rolesStrings = await userManager.GetRolesAsync(user);

            // TODO: fix client-side evaluation
            return SmartHomeAppDbContext.Query<Role>()
                .ProjectTo<RoleModel>(Mapper.ConfigurationProvider)
                .AsEnumerable().Where(x => rolesStrings.Contains(x.Name))
                .ToList();
        }
    }
}