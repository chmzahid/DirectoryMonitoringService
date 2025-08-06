using DirectoryMonitorService;
using System.Runtime.Versioning;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace DirectoryMonitorService
{
    public class Program
    {
        [SupportedOSPlatform("windows")]
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        [SupportedOSPlatform("windows")]
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService() // 👈 Important for running as a service
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}