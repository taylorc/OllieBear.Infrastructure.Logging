using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Logging.Serilog.Autofac;
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

            var containerBuilder = new ContainerBuilder();

            services
                .Configure<LoggingConfigurationOptions>(configuration.GetSection("LoggingConfigurationOptions"));

            containerBuilder.Populate(services);

            containerBuilder
                .RegisterModule(new InfrastructureLoggingIoCModule());

            containerBuilder.RegisterType<Service>()
                .AsImplementedInterfaces();
            
            var container = containerBuilder.Build();

            var service = container.Resolve<IService>();

            service.Run();
        }
    }
}
