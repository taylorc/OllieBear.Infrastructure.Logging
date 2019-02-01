using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Logging.HsdConnect.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHsdConnectLogging(this IServiceCollection services)
        {
            services.TryAddSingleton<ILog, LogCollection>();

            services.AddSingleton<IHsdConnectFactory, HsdConnectFactory>();

            services.AddSingleton(typeof(ILoggerEntity), s => s.GetService<IHsdConnectFactory>().BuildLoggerEntity());

            return services;
        }
    }
}