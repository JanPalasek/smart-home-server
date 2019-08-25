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
            services.AddSingleton(provider =>
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

                return mapper;
            });

            return this;
        }

        protected internal virtual ServiceLoader LoadRepositories(IServiceCollection services)
        {
            services.AddScoped<SmartHomeAppDbContext>();
            
            services.AddScoped<ITemperatureMeasurementRepository, TemperatureMeasurementRepository>();
            services.AddScoped<IBatteryMeasurementRepository, BatteryMeasurementRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IBatteryPowerSourceTypeRepository, BatteryPowerSourceTypeRepository>();
            services.AddScoped<ISensorTypeRepository, SensorTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();

            return this;
        }

        protected internal virtual ServiceLoader LoadAuthentication(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<SmartHomeDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                
                options.Password.RequireNonAlphanumeric = false;
                
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });
            
            return this;
        }
    }
}