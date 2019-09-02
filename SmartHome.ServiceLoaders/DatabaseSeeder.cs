using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database.Entities;

namespace SmartHome.ServiceLoaders
{
    public class DatabaseSeeder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;

        public DatabaseSeeder(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }
        
        public async Task<DatabaseSeeder> SeedInitialUserAsync()
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();
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