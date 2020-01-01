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
        public async Task AddToRolesAsyncTest()
        {
            var role = await GetAnyAsync<Role>();
            
            await repository.AddToRolesAsync(user.Id, new List<long>(){ role.Id });
            
            Assert.That(DbContext.Set<IdentityUserRole<long>>().Any(x => x.RoleId == role.Id
                                                                         && x.UserId == user.Id), Is.True);
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
    }
}