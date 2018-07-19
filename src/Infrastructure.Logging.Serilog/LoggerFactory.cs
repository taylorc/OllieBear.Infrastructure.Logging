using Infrastructure.Logging.Interfaces;
using Infrastructure.Logging.Utils;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Enrichers;
using Serilog.Formatting.Display;
using Serilog.Sinks.RollingFile;

namespace Infrastructure.Logging.Serilog
{
    public class LoggerFactory : ILogFactory
    {
        private readonly LoggingConfigurationOptions _loggingConfigurationOptions;
        private readonly LoggerConfiguration _loggerConfiguration;

        public LoggerFactory(IOptions<LoggingConfigurationOptions> loggingConfigurationOptions)
        {
            _loggingConfigurationOptions = loggingConfigurationOptions.Value;
            _loggerConfiguration = new LoggerConfiguration();
        }

        public ILog BuildLog()
        {
            _loggerConfiguration
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.With(new MachineNameEnricher())
                .Enrich.With(new EnvironmentUserNameEnricher())
                .Enrich.WithProperty("Application", _loggingConfigurationOptions.ApplicationName)
                .MinimumLevel.Verbose();

            AddConsoleLogger(_loggerConfiguration);

            if (_loggingConfigurationOptions.HasFileConfiguration)
                AddRollingFileLogger(
                    _loggerConfiguration,
                    _loggingConfigurationOptions.ApplicationName,
                    _loggingConfigurationOptions.LoggingFileConfiguration);

            return new Logger(_loggerConfiguration.CreateLogger());
        }

        private static void AddConsoleLogger(LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                .WriteTo
                .Console();
        }

        private static void AddRollingFileLogger(
            LoggerConfiguration loggerConfiguration,
            string applicationName,
            LoggingFileConfiguration fileConfiguration)
        {
            loggerConfiguration
                .WriteTo
                .Sink(new RollingFileSink(
                    SystemDriveUtils.GetFilePath(applicationName),
                    new MessageTemplateTextFormatter(fileConfiguration.Format, null),
                    fileConfiguration.FileSizeLimitBytes,
                    fileConfiguration.NumberOfFilesRetained));
        }
    }
}
