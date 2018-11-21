using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetServiceLib
{
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
}