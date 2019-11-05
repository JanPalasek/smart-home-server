using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartHome.Database;
using SmartHome.Shared.Configurations;

namespace SmartHome.ServiceLoaders
{
    public class WebLoader : ServiceLoader
    {
        private readonly IConfiguration configuration;
        private readonly bool isDevelopmentEnvironment;

        public WebLoader(IConfiguration configuration, bool isDevelopmentEnvironment)
        {
            this.configuration = configuration;
            this.isDevelopmentEnvironment = isDevelopmentEnvironment;
        }
        
        public override IServiceCollection Load(IServiceCollection services)
        {
            // db context
            if (isDevelopmentEnvironment)
            {
                services.AddDbContext<SmartHomeDbContext>(
                    options => options
                        .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database"))
                        // log data to know where is the mistake
                        .EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContext<SmartHomeDbContext>(
                    options => options
                        .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database")));
            }

            LoadConfiguration(services).LoadAuthentication(services)
                .LoadRepositories(services).LoadAutoMapper(services);
            
            services.AddScoped<TransactionFilter>();
            
            return services;
        }
    }
}