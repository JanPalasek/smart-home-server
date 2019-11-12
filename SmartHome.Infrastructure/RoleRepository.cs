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
    public class RoleRepository : StandardRepository<Role, RoleModel>, IRoleRepository
    {
        private readonly RoleManager<Role> roleManager;
        
        
        public RoleRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper,
            RoleManager<Role> roleManager) : base(smartHomeAppDbContext, mapper)
        {
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(RoleModel model)
        {
            var role = Mapper.Map<Role>(model);
            
            var result = await roleManager.CreateAsync(role);

            return result;
        }

        public async Task<IdentityResult> UpdateRoleAsync(RoleModel model)
        {
            var role = Mapper.Map<Role>(model);

            return await roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(long roleId)
        {
            var role = await SmartHomeAppDbContext.SingleAsync<Role>(roleId);

            return await roleManager.DeleteAsync(role);
        }
    }
}