using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Logging.Serilog.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILogFactory, LoggerFactory>();

            services.AddSingleton(typeof(ILog), s => s.GetService<ILogFactory>().BuildLog());

            return services;
        }
    }
}