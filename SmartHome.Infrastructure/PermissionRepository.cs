using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHome.Common;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class PermissionRepository : StandardRepository<Permission, PermissionModel>, IPermissionRepository
    {
        private readonly UserManager<User> userManager;
        
        public PermissionRepository(SmartHomeAppDbContext smartHomeAppDbContext,
            IMapper mapper, UserManager<User> userManager) : base(smartHomeAppDbContext, mapper)
        {
            this.userManager = userManager;
        }
        
        public async Task<IList<PermissionModel>> GetUserOnlyPermissionsAsync(long userId)
        {
            var userPermissions = await SmartHomeAppDbContext.Query<UserPermission>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Permission)
                .ProjectTo<PermissionModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return userPermissions;
        }

        public async Task<IList<PermissionModel>> GetAllUserPermissionsDistinctAsync(long userId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            var userPermissions = await SmartHomeAppDbContext.Query<UserPermission>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Permission)
                .ProjectTo<PermissionModel>(Mapper.ConfigurationProvider)
                .ToListAsync();
            
            var rolesStrings = (List<string>)await userManager.GetRolesAsync(user);

            var rolePermissions = await SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => rolesStrings.Contains(x.Role.Name))
                .Select(x => x.Permission)
                .ProjectTo<PermissionModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            var comparer = EqualityComparerFactory.Create<PermissionModel>(
                x => (int) x.Id,
                (x, y) => x.Id == y.Id);
            var resultPermissions = userPermissions.Union(rolePermissions, comparer);
            resultPermissions = resultPermissions.OrderBy(x => x.Name);

            return resultPermissions.ToList();
        }

        public async Task<IList<PermissionModel>> GetRolePermissionsAsync(long roleId)
        {
            var rolePermissions = SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => x.RoleId == roleId)
                .Select(x => x.Permission)
                .OrderBy(x => x.Name)
                .ProjectTo<PermissionModel>(Mapper.ConfigurationProvider);
            var result = await rolePermissions.ToListAsync();
            return result;
        }

        public async Task<PermissionModel?> GetByNameAsync(string name)
        {
            var permission = await SmartHomeAppDbContext.Query<Permission>().SingleOrDefaultAsync(x => x.Name == name);

            return Mapper.Map<PermissionModel>(permission);
        }

        public async Task<IList<PermissionRoleModel>> GetAllUserPermissionsWithRolesAsync(long userId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);

            var userPermissions = await SmartHomeAppDbContext.Query<UserPermission>()
                .Where(x => x.UserId == userId)
                .ProjectTo<PermissionRoleModel>(Mapper.ConfigurationProvider)
                .ToListAsync();
            
            var rolesStrings = (List<string>)await userManager.GetRolesAsync(user);

            var rolePermissions = await SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => rolesStrings.Contains(x.Role.Name))
                .ProjectTo<PermissionRoleModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            var comparer = EqualityComparerFactory.Create<PermissionRoleModel>(
                x => (int) x.PermissionId,
                (x, y) => x.PermissionId == y.PermissionId && string.Equals(x.RoleName, y.RoleName));
            var resultPermissions = userPermissions.Union(rolePermissions, comparer);
            resultPermissions = resultPermissions.OrderBy(x => x.PermissionName);

            return resultPermissions.ToList();
        }
    }
}