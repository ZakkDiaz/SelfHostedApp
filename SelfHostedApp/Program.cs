using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DotNetServiceLib;
using Microsoft.AspNetCore.Hosting;

namespace SelfHostedApp
{
    internal class Program
    {
        private static IContainer Container { get; set; }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!"); // Create your builder.
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
                    Thread.Sleep(1000);
                }
            }
        }
    }
}