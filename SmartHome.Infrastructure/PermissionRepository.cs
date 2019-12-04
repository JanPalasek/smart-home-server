using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<IList<PermissionModel>> GetUserPermissionsAsync(long userId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);

            var userPermissions = SmartHomeAppDbContext.Query<UserPermission>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Permission)
                .AsEnumerable();
            
            var rolesStrings = await userManager.GetRolesAsync(user);

            // TODO: fix client-side evaluation
            var rolePermissions = SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => rolesStrings.Contains(x.Role.Name))
                .Select(x => x.Permission)
                .AsEnumerable();

            var intersect = userPermissions.Intersect(rolePermissions);

            return intersect.Select(Mapper.Map<PermissionModel>).ToList();
        }

        public async Task<IList<PermissionModel>> GetRolePermissionsAsync(long roleId)
        {
            var rolePermissions = SmartHomeAppDbContext.Query<RolePermission>()
                .Where(x => x.RoleId == roleId)
                .Select(x => x.Permission)
                .ProjectTo<PermissionModel>(Mapper.ConfigurationProvider);
            return await rolePermissions.ToListAsync();
        }

        public async Task<PermissionModel?> GetByName(string name)
        {
            var permission = await SmartHomeAppDbContext.Query<Permission>().SingleOrDefaultAsync(x => x.Name == name);

            return Mapper.Map<PermissionModel>(permission);
        }
    }
}