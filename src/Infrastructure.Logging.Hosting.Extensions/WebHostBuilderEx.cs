using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging.Hosting.Extensions
{
    public static class WebHostBuilderEx
    {
        public static IWebHostBuilder RerouteDefaultLogging(
            this IWebHostBuilder webHostBuilder,
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
