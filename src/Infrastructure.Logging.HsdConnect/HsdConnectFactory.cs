using System;
using Infrastructure.Logging.Enums;
using Infrastructure.Logging.HsdConnect.Extensions;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Infrastructure.Logging.HsdConnect
{
    public class HsdConnectFactory : IHsdConnectFactory
    {
        private readonly LoggingConfigurationOptions _loggingConfigurationOptions;
        private readonly LoggerConfiguration _loggerConfiguration;

        public HsdConnectFactory(IOptions<LoggingConfigurationOptions> loggingConfigurationOptions)
        {
            _loggingConfigurationOptions = loggingConfigurationOptions.Value;
            _loggerConfiguration = new LoggerConfiguration();
        }

        public ILoggerItem BuildLoggerItem()
        {
            _loggerConfiguration
               /* .Enrich.With(new ThreadIdEnricher())
                .Enrich.With(new MachineNameEnricher())
                .Enrich.With(new EnvironmentUserNameEnricher())*/
                .Enrich.WithProperty("Application", _loggingConfigurationOptions.ApplicationName)
                .MinimumLevel.Verbose();

            foreach (var loggileFileConfiguration in
                _loggingConfigurationOptions.LoggingFileConfigurations ??
                new LoggingFileConfiguration[] { })
            {
                AddSqlServerLogger(
                    _loggerConfiguration,
                    _loggingConfigurationOptions.ApplicationName,
                    loggileFileConfiguration);
            }

            return new HsdConnectLogger(_loggerConfiguration.CreateLogger());
        }

        private static void AddSqlServerLogger(
            LoggerConfiguration loggerConfiguration,
            string applicationName,
            LoggingFileConfiguration fileConfiguration)
        {
            if (!Enum.TryParse(fileConfiguration.MinimumLogLevel, out LogLevel logLevel))
            {
                throw new Exception($"Unrecognised log level {fileConfiguration.MinimumLogLevel}");
            }

            loggerConfiguration
                .WriteTo
                .MSSqlServer(
                    connectionString: "test",
                    tableName: "test",
                    columnOptions: BuildColumnOptions(), 
                    restrictedToMinimumLevel: logLevel.ToLogEventLevel());
        }

        private static ColumnOptions BuildColumnOptions()
        {
            return new ColumnOptions();
        }
    }
}
