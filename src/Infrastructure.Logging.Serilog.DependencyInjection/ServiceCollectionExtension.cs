using Infrastructure.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Logging.Serilog.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
        {
            services.AddTransient<ILogFactory, LoggerFactory>();

            services.AddTransient(typeof(ILog), s => s.GetService<ILogFactory>().BuildLog());

            return services;
        }
    }
}