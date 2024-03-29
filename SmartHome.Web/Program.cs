﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SmartHome.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext,
                    configurationBuilder) =>
                {
                    var hostingEnvironment =
                        builderContext.HostingEnvironment;
                    configurationBuilder
                        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile(
                        $"appsettings.{hostingEnvironment.EnvironmentName.ToLower()}.json",
                        optional: true, reloadOnChange: true);
                })
                .UseStartup<Startup>()
                .Build();
    }
}