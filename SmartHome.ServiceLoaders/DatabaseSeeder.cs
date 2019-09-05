using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database;
using SmartHome.Database.Entities;
using SmartHome.Repositories;
using SmartHome.Shared;

namespace SmartHome.ServiceLoaders
{
    public class DatabaseSeeder
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public DatabaseSeeder(IConfiguration configuration, UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        /// <summary>
        /// Seeds database with required initial data if it not exists.
        /// </summary>
        /// <returns></returns>
        public async Task SeedAllAsync()
        {
            await SeedUserAsync();
        }

        public async Task<DatabaseSeeder> SeedUserAsync()
        {
            // if not exists
            string userName = configuration["InitialUser:UserName"];
            string email = configuration["InitialUser:Email"];
            if (await userManager.FindByNameAsync(userName) == null
                && await userManager.FindByEmailAsync(email) == null)
            {
                await userManager.CreateAsync(new User()
                {
                    Email = email,
                    UserName = userName,
                }, configuration["InitialUser:Password"]);
            }

            return this;
        }
    }
}