using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetServiceLib
{
    public class DotNetService : IWebHost
    {
        public IFeatureCollection ServerFeatures { get; set; } = new FeatureCollection();

        public IServiceProvider Services { get; set; } = ServiceProviderFactory();

        public DotNetService(IClassInstance classInstance)
        {
        }

        public void Dispose()
        {
            Console.WriteLine("Stopping");
        }

        public void Start()
        {
            Console.WriteLine("Starting");
        }

        public Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            for(var i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i}");
                System.Threading.Thread.Sleep(1000);
            }
            return null;
        }

        public Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine("Stopping");
            System.Threading.Thread.Sleep(1000);
            return null;
        }


        private static IServiceProvider ServiceProviderFactory()
        {
            HostingEnvironment env = new HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();
            env.EnvironmentName = "Development";

            Startup startup = new Startup(env);
            ServiceCollection sc = new ServiceCollection();
            startup.ConfigureServices(sc);
            return sc.BuildServiceProvider();
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

        }

        internal void ConfigureServices(IServiceCollection sc)
        {
            sc.AddSingleton<IClassInstance, MyInstance>();
            sc.AddSingleton(sp => new WebHostOptions());
        }
    }

    public class MyInstance : IClassInstance
    {

    }

    public interface IClassInstance
    {
    }
}
