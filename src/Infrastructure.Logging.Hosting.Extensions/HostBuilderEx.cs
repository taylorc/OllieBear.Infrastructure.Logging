using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging.Hosting.Extensions
{
    public static class HostBuilderEx
    {
        public static IHostBuilder RerouteDefaultLogging(
            this IHostBuilder webHostBuilder,
            Action<IServiceCollection> action)
        {
            var serviceCollection = new ServiceCollection();

            action(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var log = serviceProvider.GetRequiredService<ILog>();

            webHostBuilder
                .ConfigureLogging(logging =>
                    logging
                        .ClearProviders()
                        .AddProvider(new ReroutedLoggerProvider(log)));

            return webHostBuilder;
        }
    }
}
