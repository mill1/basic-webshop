using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Earless.WebApi.Data;

namespace Earless.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            IHost host = hostBuilder.Build();

            using (var scope = host.Services.CreateScope())
            {
                DbInitializer initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                await initializer.Initialize();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);

            Action<IWebHostBuilder> configure = InitializeWebHostBuilder;
            return hostBuilder.ConfigureWebHostDefaults(configure);
        }

        static void InitializeWebHostBuilder(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseStartup<Startup>();
        }
    }
}
