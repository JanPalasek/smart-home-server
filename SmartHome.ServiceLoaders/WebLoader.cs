using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database;

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

            LoadAuthentication(services)
                .LoadRepositories(services).LoadAutoMapper(services);
            
            services.AddScoped<TransactionFilter>();
            
            return services;
        }
    }
}