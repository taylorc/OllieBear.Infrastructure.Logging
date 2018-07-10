using System.IO;
using Autofac;
using Infrastructure.Logging.Serilog;
using Infrastructure.Logging.Serilog.Autofac;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Logging.Sample.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterModule(new InfrastructureLoggingIoCModule(configuration));

            containerBuilder.RegisterType<Service>()
                .AsImplementedInterfaces();

            var container = containerBuilder.Build();

            var service = container.Resolve<IService>();

            service.Run();
        }
    }
}
