using System.IO;
using Infrastructure.Logging.Hosting.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Logging.Sample.Web.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(BuildConfiguration())
                .UseStartup<Startup>()
                .RerouteDefaultLogging(s => Startup.AddInjectedLogging(s, BuildConfiguration()));

        public static IConfigurationRoot BuildConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
    }
}
