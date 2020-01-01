using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.Infrastructure.Tests.Mocks;
using SmartHome.Shared.Tests;
using SmartHome.Shared.Tests.SmartHome.Shared.Tests;

namespace SmartHome.Infrastructure.Tests
{
    [TestFixture]
    public class PermissionRepositoryTests : DatabaseTests
    {
        private PermissionRepository permissionRepository;
        private User user;
        private Role role;
        private Permission permission1;
        private Permission permission2;
        
        [SetUp]
        public async Task SetUp()
        {
            permission1 = new Permission()
            {
                Name = "Permission1"
            };
            permission2 = new Permission()
            {
                Name = "Permission2"
            };
            DbContext.Add(permission1);
            DbContext.Add(permission2);
            user = new User()
            {
                Email = "permissionUser@janpalasek.com",
                UserName = "permissionUser",
                NormalizedEmail = "permissionUser@janpalasek.com",
                NormalizedUserName = "permissionUser",
                PasswordHash = "asdfghjkl",
            };
            DbContext.Add(user);
            role = new Role()
            {
                Name = "PermissionTestRole"
            };
            DbContext.Add(role);
            DbContext.Add(new IdentityUserRole<long>()
            {
                UserId = user.Id,
                RoleId = role.Id
            });
            DbContext.Add(new RolePermission()
            {
                PermissionId = permission1.Id,
                RoleId = role.Id
            });
            DbContext.Add(new RolePermission()
            {
                PermissionId = permission2.Id,
                RoleId = role.Id
            });
            DbContext.Add(new UserPermission()
            {
                PermissionId = permission1.Id,
                UserId = user.Id
            });

            await DbContext.SaveChangesAsync();
            
            var userManagerMock = new Mock<FakeUserManager>();
            userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string>() {role.Name});
            
            var appDbContext = new SmartHomeAppDbContext(DbContext);
            permissionRepository = new PermissionRepository(appDbContext, Mapper, userManagerMock.Object);
        }

        [Test]
        public async Task GetAllUserPermissionsAsyncTest()
        {
            var permissions = await permissionRepository.GetAllUserPermissionsDistinctAsync(user.Id);
            var permissionIds = permissions.Select(x => x.Id).ToList();
            Assert.That(permissions.Count, Is.EqualTo(2));
            Assert.That(permissionIds, Contains.Item(permission1.Id).And.Contains(permission2.Id));
        }
        
        [Test]
        public async Task GetUserOnlyPermissionsAsyncTest()
        {
            var permissions = await permissionRepository.GetUserOnlyPermissionsAsync(user.Id);
            var permissionIds = permissions.Select(x => x.Id).ToList();
            Assert.That(permissions.Count, Is.EqualTo(1));
            Assert.That(permissionIds, Contains.Item(permission1.Id));
        }

        [Test]
        public async Task GetRolePermissionsAsyncTest()
        {
            var permissions = await permissionRepository.GetRolePermissionsAsync(role.Id);
            var permissionIds = permissions.Select(x => x.Id).ToList();
            Assert.That(permissions.Count, Is.EqualTo(2));
            Assert.That(permissionIds, Contains.Item(permission1.Id).And.Contains(permission2.Id));
        }

        [Test]
        public async Task GetAllUserPermissionsWithRolesAsyncTest()
        {
            var permissions = await permissionRepository.GetAllUserPermissionsWithRolesAsync(user.Id);
            var permissionIds = permissions.Select(x => x.PermissionId).ToList();
            Assert.That(permissions.Count, Is.EqualTo(3));
            Assert.That(permissionIds, Contains.Item(permission1.Id).And.Contains(permission2.Id));
        }
    }
}