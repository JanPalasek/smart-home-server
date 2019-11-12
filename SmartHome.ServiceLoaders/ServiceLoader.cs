using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Configurations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Account;
using SmartHome.DomainCore.ServiceInterfaces.Admin;
using SmartHome.DomainCore.ServiceInterfaces.Place;
using SmartHome.Infrastructure;
using SmartHome.Services.Account;
using SmartHome.Services.Admin;
using SmartHome.Services.Place;

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
                        .Where(a => a.FullName!.StartsWith("SmartHome"));
                
                    cfg.AddMaps(assemblies);
                });
                config.AssertConfigurationIsValid();
            
                var mapper = config.CreateMapper();

                return mapper;
            });

            return this;
        }

        protected internal virtual ServiceLoader LoadRepositoriesAndServices(IServiceCollection services)
        {
            services.AddScoped<SmartHomeAppDbContext>();
            
            services.AddScoped<ITemperatureMeasurementRepository, TemperatureMeasurementRepository>();
            services.AddScoped<IBatteryMeasurementRepository, BatteryMeasurementRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IBatteryPowerSourceTypeRepository, BatteryPowerSourceTypeRepository>();
            services.AddScoped<ISensorTypeRepository, SensorTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<ICreatePlaceService, CreatePlaceService>();
            services.AddScoped<IGetPlacesService, GetPlacesService>();
            services.AddScoped<IUpdatePlaceService, UpdatePlaceService>();

            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ISignOutService, SignOutService>();

            services.AddScoped<IChangePasswordService, ChangePasswordService>();
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<IGetUsersService, GetUsersService>();

            return this;
        }

        protected internal virtual ServiceLoader LoadAuthentication(IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>()
                .AddRoles<Role>()
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

        protected internal virtual ServiceLoader LoadConfiguration(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var configurationProvider = provider.GetRequiredService<IConfiguration>();
                var parsedConfiguration = configurationProvider.GetSection("FileManager").Get<FileManagerConfiguration>();
                  
                return parsedConfiguration;
            });

            return this;
        }
    }
}