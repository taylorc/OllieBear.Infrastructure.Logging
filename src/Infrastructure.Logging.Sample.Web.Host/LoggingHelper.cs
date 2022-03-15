using Infrastructure.Logging.Serilog.DependencyInjection;

namespace Infrastructure.Logging.Sample.Web.Host
{
    public static class LoggingHelper
    {
        public static void AddStructureLogging(IServiceCollection services, ConfigurationManager builderConfiguration)
        {
            services
                .Configure<LoggingConfigurationOptions>(builderConfiguration.GetSection("LoggingConfigurationOptions"));

            services
                .AddSerilogLogging();
        }

}
}
