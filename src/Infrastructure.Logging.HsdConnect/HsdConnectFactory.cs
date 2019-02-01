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
        private const string LogsTable = "Logs";

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
                .MinimumLevel.Verbose();

            foreach (var loggingDatabaseConfiguration in
                _loggingConfigurationOptions.LoggingDatabaseConfigurations ??
                new LoggingDatabaseConfiguration[] { })
            {
                AddSqlServerLogger(
                    _loggerConfiguration,
                    loggingDatabaseConfiguration);
            }

            return new HsdConnectLogger(_loggerConfiguration.CreateLogger());
        }

        private static void AddSqlServerLogger(
            LoggerConfiguration loggerConfiguration,
            LoggingDatabaseConfiguration databaseConfiguration)
        {
            if (!Enum.TryParse(databaseConfiguration.MinimumLogLevel, out LogLevel logLevel))
            {
                throw new Exception($"Unrecognised log level {databaseConfiguration.MinimumLogLevel}");
            }

            loggerConfiguration
                .WriteTo
                .MSSqlServer(
                    connectionString: databaseConfiguration.ConnectionString,
                    tableName: LogsTable,
                    restrictedToMinimumLevel: logLevel.ToLogEventLevel());
        }
    }
}
