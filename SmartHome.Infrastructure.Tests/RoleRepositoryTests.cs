using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.Infrastructure.Tests.Mocks;
using SmartHome.Shared.Tests;

namespace SmartHome.Infrastructure.Tests
{
    [TestFixture]
    public class RoleRepositoryTests : DatabaseTests
    {
        private RoleRepository repository;

        [SetUp]
        public void SetUp()
        {
            var userManagerMock = new FakeUserManagerMock(DbContext, true);
            
            repository = new RoleRepository(new SmartHomeAppDbContext(DbContext), Mapper, null, userManagerMock.Object);
        }

        [Test]
        [TestCase("Admin", "Measurement.Temperature.View", true)]
        [TestCase("Admin", "Non.Existing.Permission", false)]
        [TestCase("User", "Measurement.Temperature.Edit", true)]
        public async Task AddPermissionsToRoleAsyncTest(string roleName, string permissionName, bool successful)
        {
            var role = await DbContext.Set<Role>().FirstAsync(x => x.Name == roleName);
            var permission = await DbContext.Set<Permission>().FirstOrDefaultAsync(x => x.Name == permissionName);

            await repository.AddPermissionsToRoleAsync(role.Id,
                new List<string> {permissionName});
            if (successful)
            {
                Assert.That(permission, Is.Not.Null);
                Assert.That(await DbContext.Set<RolePermission>()
                        .AnyAsync(x => x.PermissionId == permission!.Id && x.RoleId == role.Id),
                    Is.True);
            }
            else if (permission != null)
            {
                Assert.That(await DbContext.Set<RolePermission>()
                        .AnyAsync(x => x.PermissionId == permission!.Id && x.RoleId == role.Id),
                    Is.False);
            }
        }

        [Test]
        [TestCase("Admin", "Measurement.Temperature.View", true)]
        [TestCase("Admin", "Non.Existing.Permission", false)]
        [TestCase("User", "Measurement.Temperature.Edit", true)]
        public async Task RemovePermissionsFromRolesAsyncTest(string roleName, string permissionName, bool successful)
        {
            var role = await DbContext.Set<Role>().FirstAsync(x => x.Name == roleName);
            var permission = await DbContext.Set<Permission>().FirstOrDefaultAsync(x => x.Name == permissionName);
            
            if (successful)
            {
                Assert.That(permission, Is.Not.Null);
                await repository.RemovePermissionsFromRoleAsync(role.Id,
                    new List<long> {permission.Id});
                Assert.That(await DbContext.Set<RolePermission>()
                        .AnyAsync(x => x.PermissionId == permission!.Id && x.RoleId == role.Id),
                    Is.False);
            }
            else if (permission != null)
            {
                await repository.RemovePermissionsFromRoleAsync(role.Id,
                    new List<long> {permission.Id});
                Assert.That(await DbContext.Set<RolePermission>()
                        .AnyAsync(x => x.PermissionId == permission!.Id && x.RoleId == role.Id),
                    Is.False);
            }
        }
    }
}