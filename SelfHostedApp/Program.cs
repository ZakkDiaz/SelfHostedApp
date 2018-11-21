using Autofac;
using DotNetServiceLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SelfHostedApp
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");// Create your builder.
            var builder = new ContainerBuilder();
            builder.RegisterType<DotNetService>().As<IWebHost>();
            builder.RegisterType<MyInstance>().As<IClassInstance>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IWebHost>();
                var serviceTask = service.StartAsync();
                while (serviceTask != null && serviceTask.Status == TaskStatus.Running)
                {
                    Console.WriteLine("Waiting...");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
    //public class Startup
    //{
    //    private readonly IHostingEnvironment _env;
    //    private readonly IConfiguration _config;
    //    private readonly ILoggerFactory _loggerFactory;

    //    public Startup(IHostingEnvironment env, IConfiguration config,
    //        ILoggerFactory loggerFactory)
    //    {
    //        _env = env;
    //        _config = config;
    //        _loggerFactory = loggerFactory;
    //    }

    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        var logger = _loggerFactory.CreateLogger<Startup>();

    //        if (_env.IsDevelopment())
    //        {
    //            // Development service configuration

    //            logger.LogInformation("Development environment");
    //        }
    //        else
    //        {
    //            // Non-development service configuration

    //            logger.LogInformation($"Environment: {_env.EnvironmentName}");
    //        }

    //        // Configuration is available during startup.
    //        // Examples:
    //        //   _config["key"]
    //        //   _config["subsection:suboption1"]
    //    }
    //}


