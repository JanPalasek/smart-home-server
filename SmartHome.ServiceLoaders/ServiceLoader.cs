using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database;
using SmartHome.Database.Entities;
using SmartHome.Repositories;
using SmartHome.Repositories.Interfaces;

namespace SmartHome.ServiceLoaders
{
    public abstract class ServiceLoader
    {
        public abstract IServiceCollection Load(IServiceCollection services);
        
        protected internal virtual ServiceLoader LoadAutoMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                // get all non-tests assemblies
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName.StartsWith("SmartHome"));
                
                cfg.AddMaps(assemblies);
            });
            config.AssertConfigurationIsValid();
            
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return this;
        }

        protected internal virtual ServiceLoader LoadRepositories(IServiceCollection services)
        {
            services.AddScoped<SmartHomeAppDbContext>();
            
            services.AddScoped<ITemperatureMeasurementRepository, TemperatureMeasurementRepository>();
            services.AddScoped<IBatteryMeasurementRepository, BatteryMeasurementRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IBatteryPowerSourceTypeRepository, BatteryPowerSourceTypeRepository>();
            services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return this;
        }

        protected internal virtual ServiceLoader LoadAuthentication(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<SmartHomeDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;
            });
            return this;
        }
    }
}