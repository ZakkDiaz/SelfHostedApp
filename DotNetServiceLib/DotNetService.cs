using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetServiceLib
{
    public class DotNetService : IWebHost
    {
        public DotNetService(IClassInstance classInstance)
        {
        }

        public IFeatureCollection ServerFeatures { get; set; } = new FeatureCollection();

        public IServiceProvider Services { get; set; } = ServiceProviderFactory();

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
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i}");
                Thread.Sleep(1000);
            }

            return null;
        }

        public Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine("Stopping");
            Thread.Sleep(1000);
            return null;
        }


        private static IServiceProvider ServiceProviderFactory()
        {
            var env = new HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();
            env.EnvironmentName = "Development";

            var startup = new Startup(env);
            var sc = new ServiceCollection();
            startup.ConfigureServices(sc);
            return sc.BuildServiceProvider();
        }
    }
}