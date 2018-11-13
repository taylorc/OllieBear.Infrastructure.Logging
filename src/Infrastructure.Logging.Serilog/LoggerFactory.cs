using Infrastructure.Logging.Enums;
using Infrastructure.Logging.Serilog.Extensions;
using Infrastructure.Logging.Utils;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Enrichers;
using Serilog.Formatting.Display;
using Serilog.Sinks.RollingFile;
using System;
using System.IO;

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
            if (!Enum.TryParse(_loggingConfigurationOptions.ConsoleMinimumLogLevel, out LogLevel logLevel))
            {
                throw new Exception($"Unrecognised log level {_loggingConfigurationOptions.ConsoleMinimumLogLevel}");
            }

            _loggerConfiguration
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.With(new MachineNameEnricher())
                .Enrich.With(new EnvironmentUserNameEnricher())
                .Enrich.WithProperty("Application", _loggingConfigurationOptions.ApplicationName)
                .MinimumLevel.Verbose();

            AddConsoleLogger(_loggerConfiguration, logLevel);

            foreach (var loggileFileConfiguration in
                _loggingConfigurationOptions.LoggingFileConfigurations ??
                new LoggingFileConfiguration[] { })
            {
                AddRollingFileLogger(
                    _loggerConfiguration,
                    _loggingConfigurationOptions.ApplicationName,
                    loggileFileConfiguration);
            }

            return new Logger(_loggerConfiguration.CreateLogger());
        }

        private static void AddConsoleLogger(
            LoggerConfiguration loggerConfiguration,
            LogLevel minimumLoggingLevel)
        {
            loggerConfiguration
                .WriteTo
                .Console(minimumLoggingLevel.ToLogEventLevel());
        }

        private static void AddRollingFileLogger(
            LoggerConfiguration loggerConfiguration,
            string applicationName,
            LoggingFileConfiguration fileConfiguration)
        {
            if (!Enum.TryParse(fileConfiguration.MinimumLogLevel, out LogLevel logLevel))
            {
                throw new Exception($"Unrecognised log level {fileConfiguration.MinimumLogLevel}");
            }

            var logFilePath = SystemDriveUtils.GetFilePath(applicationName);

            if (!string.IsNullOrEmpty(fileConfiguration.FilePath))
            {
                string logFileName = $"{applicationName}.log";

                logFilePath = Path.Combine(fileConfiguration.FilePath, logFileName);
            }

            loggerConfiguration
                .WriteTo
                .Sink(new RollingFileSink(
                    logFilePath,
                    new MessageTemplateTextFormatter(fileConfiguration.Format, null),
                    fileConfiguration.FileSizeLimitBytes,
                    fileConfiguration.NumberOfFilesRetained,
                    shared: fileConfiguration.IsMultiProcessShared),
                    logLevel.ToLogEventLevel());


        }
    }
}
