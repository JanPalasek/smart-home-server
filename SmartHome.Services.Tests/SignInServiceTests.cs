using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.Services.Account;
using SmartHome.Shared.Tests;
using SmartHome.Shared.Tests.SmartHome.Shared.Tests;

namespace SmartHome.Services.Tests
{
    [TestFixture]
    public class SignInServiceTests : TestsBase
    {
        private SignInService signInService;
        
        [SetUp]
        public void SetUp()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var validUser = new UserModel()
            {
                Id = 1, Email = "valid@email.cz", PasswordHash = "pwdhash", UserName = "valid"
            };
            userRepositoryMock.Setup(x => x.GetUserByEmailAsync("valid@email.cz"))
                .ReturnsAsync(validUser);
            userRepositoryMock.Setup(x => x.GetUserByNameAsync("valid"))
                .ReturnsAsync(validUser);
            userRepositoryMock.Setup(x => x.SignInAsync(validUser, It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
            userRepositoryMock.Setup(x => x.SignInAsync(validUser, "validPassword", It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            
            signInService = new SignInService(userRepositoryMock.Object);
        }

        [Test]
        public void SignInHappyPathTest()
        {
            var loginModel = new LoginModel()
            {
                Login = "valid",
                Password = "validPassword"
            };
            var result = signInService.SignInAsync(loginModel).Result;
            Assert.That(result, Is.EqualTo(SignInResult.Success));
        }
        
        [Test]
        public void SignInInvalidPasswordTest()
        {
            var loginModel = new LoginModel()
            {
                Login = "valid",
                Password = "invalidPassword"
            };
            var result = signInService.SignInAsync(loginModel).Result;
            Assert.That(result, Is.EqualTo(SignInResult.Failed));
        }

        [Test]
        public void SignInInvalidNameTest()
        {
            var loginModel = new LoginModel()
            {
                Login = "invalid",
                Password = "validPassword"
            };
            var result = signInService.SignInAsync(loginModel).Result;
            Assert.That(result, Is.EqualTo(SignInResult.Failed));
        }
    }
}