using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHome.Common;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
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

            var rolesStrings = (List<string>)await userManager.GetRolesAsync(user);

            return SmartHomeAppDbContext.Query<Role>()
                .Where(x => rolesStrings.Contains(x.Name))
                .ProjectTo<RoleModel>(Mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task AddPermissionsToRoleAsync(long roleId, List<string> permissions)
        {
            var permissionIds = await SmartHomeAppDbContext.Query<Permission>()
                .Where(x => permissions.Contains(x.Name))
                .Select(x => x.Id)
                .ToListAsync();

            var entities = permissionIds.Select(x => new RolePermission()
            {
                RoleId = roleId,
                PermissionId = x
            });

            // get already existing user permissions from the database
            var existingUserPermissions = await SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => x.RoleId == roleId)
                .Where(x => permissionIds.Contains(x.PermissionId))
                .ToListAsync();
            
            // insert only those that are not in the database
            entities = entities.Except(existingUserPermissions, EqualityComparerFactory.Create<RolePermission>(
                x => x.PermissionId.GetHashCode(),
                (x, y) => x.PermissionId == y.PermissionId && x.RoleId == y.RoleId));
            
            await SmartHomeAppDbContext.AddRangeAsync(entities);
        }

        public async Task RemovePermissionsFromRoleAsync(long roleId, List<long> permissions)
        {
            var permissionsToRemove = await SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => x.RoleId == roleId)
                .Where(x => permissions.Contains(x.Permission.Id))
                .ToListAsync();
            
            await SmartHomeAppDbContext.DeleteRangeAsync(permissionsToRemove);
        }
    }
}