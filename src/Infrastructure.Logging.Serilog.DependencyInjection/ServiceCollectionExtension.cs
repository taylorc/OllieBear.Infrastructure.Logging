using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Logging.Serilog.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
        {
            services.TryAddSingleton<ILog, LogCollection>();

            services.AddSingleton<ISerilogFactory, LoggerFactory>();

            services.AddSingleton(typeof(ILoggerItem), s => s.GetService<ISerilogFactory>().BuildLoggerItem());

            return services;
        }
    }
}