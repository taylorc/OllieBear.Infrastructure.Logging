using System.IO;
using Infrastructure.Logging.Serilog.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Logging.Sample.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var services = new ServiceCollection();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();



            services
                .Configure<LoggingConfigurationOptions>(configuration.GetSection("LoggingConfigurationOptions"));

            services.AddSerilogLogging();

            services.AddTransient<IJobRun, JobRun>();
            services.AddTransient<IJobRunTwo, JobRunTwo>();

            services.AddTransient<IService, Service>();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var service = serviceProvider.GetService<IService>();
            service.Run();
            }


        }
    }
}
