using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database;
using SmartHome.Database.Repositories;
using SmartHome.Repositories;
using SmartHome.Repositories.Interfaces;

namespace SmartHome.ServiceLoaders
{
    public class WebLoader : ServiceLoader
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;

        public WebLoader(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }
        
        public override IServiceCollection Load(IServiceCollection services)
        {
            // db context
            if (hostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<SmartHomeDbContext>(
                    options => options
                        .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database"))
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        // log data to know where is the mistake
                        .EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContext<SmartHomeDbContext>(
                    options => options
                        .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database")));
            }

            services = LoadRepositories(services);
            
            services = LoadAutoMapper(services);


            return services;
        }
    }
}