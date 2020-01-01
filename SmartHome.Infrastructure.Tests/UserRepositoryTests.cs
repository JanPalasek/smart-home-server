using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.Infrastructure.Tests.Mocks;
using SmartHome.Shared.Tests;

namespace SmartHome.Infrastructure.Tests
{
    [TestFixture]
    public class UserRepositoryTests : DatabaseTests
    {
        private UserRepository repository;
        private User user;

        [SetUp]
        public async Task SetUp()
        {
            user = new User()
            {
                Email = "repositoryTestUser@janpalasek.com",
                UserName = "repositoryTestUser",
                NormalizedEmail = "repositoryTestUser@janpalasek.com",
                NormalizedUserName = "repositoryTestUser",
                PasswordHash = "asdfghjkl",
            };
            DbContext.Add(user);

            await DbContext.SaveChangesAsync();

            var userManagerMock = new FakeUserManagerMock(DbContext, true);
            
            repository = new UserRepository(new SmartHomeAppDbContext(DbContext), Mapper,
                userManagerMock.Object, null);
        }

        [Test]
        [TestCase("admin", "User", true)]
        [TestCase("user", "Admin", true)]
        public async Task AddToRolesAsyncTest(string userName, string roleName, bool success)
        {
            var user = await DbContext.Set<User>().FirstAsync(x => x.UserName == userName);
            var role = await DbContext.Set<Role>().FirstAsync(x => x.Name == roleName);
            
            var result = await repository.AddToRolesAsync(user.Id, new List<long>(){ role.Id });
            
            Assert.That(result.Succeeded, Is.EqualTo(success));
            Assert.That(await DbContext.Set<IdentityUserRole<long>>().AnyAsync(x => x.RoleId == role.Id
                                                                         && x.UserId == user.Id), Is.True);
        }

        [Test]
        [TestCase("admin", "Admin", true)]
        [TestCase("admin", "User", false)]
        [TestCase("user", "User", true)]
        [TestCase("user", "Admin", false)]
        public async Task RemoveFromRolesAsyncTest(string userName, string roleName, bool success)
        {
            var user = await DbContext.Set<User>().FirstAsync(x => x.UserName == userName);
            var role = await DbContext.Set<Role>().FirstAsync(x => x.Name == roleName);
            
            var result = await repository.RemoveFromRolesAsync(user.Id, new List<long> {role.Id});
            Assert.That(result.Succeeded, Is.EqualTo(success));
            Assert.That(await DbContext.Set<IdentityUserRole<long>>().AnyAsync(x => x.RoleId == role.Id
                                                                         && x.UserId == user.Id), Is.False);
        }

        [Test]
        [TestCase("user", "Measurement.Temperature.View", true)]
        [TestCase("user", "Measurement.Temperature.Edit", true)]
        [TestCase("user", "File.View", true)]
        [TestCase("user", "File.Edit", false)]
        public async Task UserHasPermissionAsyncTest(string userName, string permission, bool expected)
        {
            var user = await DbContext.Set<User>().FirstAsync(x => x.UserName == userName);

            var actual = await repository.HasPermissionAsync(user.Id, permission);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("user", "Measurement.Temperature.Edit")]
        [TestCase("user", "Measurement.Temperature.View")]
        public async Task AddPermissionsToUserAsyncTest(string userName, string permissionName)
        {
            var user = await DbContext.Set<User>().FirstAsync(x => x.UserName == userName);
            var permission = await DbContext.Set<Permission>().FirstAsync(x => x.Name == permissionName);

            Assert.That(() => repository.AddPermissionsToUserAsync(
                user.Id, new List<string>() { permissionName }), Throws.Nothing);
            Assert.That(await DbContext.Set<UserPermission>()
                    .AnyAsync(x => x.PermissionId == permission.Id && x.UserId == user.Id),
                Is.True);
        }
    }
}