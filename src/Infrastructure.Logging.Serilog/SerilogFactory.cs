using Infrastructure.Logging.Enums;
using Infrastructure.Logging.Serilog.Extensions;
using Infrastructure.Logging.Utils;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Enrichers;
using Serilog.Formatting.Display;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;

namespace Infrastructure.Logging.Serilog
{
    public class SerilogFactory : ISerilogFactory
    {
        private readonly LoggingConfigurationOptions _loggingConfigurationOptions;
        private readonly LoggerConfiguration _loggerConfiguration;

        public SerilogFactory(IOptions<LoggingConfigurationOptions> loggingConfigurationOptions)
        {
            _loggingConfigurationOptions = loggingConfigurationOptions.Value;
            _loggerConfiguration = new LoggerConfiguration();
        }

        public ILoggerEntity BuildLoggerEntity()
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
                .Enrich.FromLogContext()
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

            foreach (var loggingDatabaseConfiguration in _loggingConfigurationOptions.LoggingDatabaseConfigurations ?? new LoggingDatabaseConfiguration[]{ })
            {
                 AddMsSqlLogger(
                    _loggerConfiguration,
                    _loggingConfigurationOptions.ApplicationName,
                    loggingDatabaseConfiguration);
            }

            return new SerilogFileLogger(_loggerConfiguration.CreateLogger());

        }

        private static void AddConsoleLogger(
            LoggerConfiguration loggerConfiguration,
            LogLevel minimumLoggingLevel)
        {
            loggerConfiguration
                .WriteTo
                .Console(minimumLoggingLevel.ToLogEventLevel());
        }

        private static void AddMsSqlLogger(
            LoggerConfiguration loggerConfiguration,
            string applicationName,
            LoggingDatabaseConfiguration databaseConfiguration)
        {
            var option = new SinkOptions();
            option.TableName = "Logs";
            ColumnOptions columnOptions= null;
            if(databaseConfiguration.LoggingAdditionalColumns!=null && databaseConfiguration.LoggingAdditionalColumns.Count()>0)
            {
                columnOptions = new ColumnOptions();
                foreach (var item in databaseConfiguration.LoggingAdditionalColumns)
                {
                    columnOptions.AdditionalColumns = new Collection<SqlColumn>
                        {
                            new SqlColumn
                                {ColumnName = item.ColumnName, PropertyName = item.PropertyName, DataType = item.SqlDbType, DataLength = item.DataLength},
                        };
                }
            }          


            if (!Enum.TryParse(databaseConfiguration.MinimumLogLevel, out LogLevel logLevel))
            {
                throw new Exception($"Unrecognised log level {databaseConfiguration.MinimumLogLevel}");
            }
            
            if(columnOptions!=null)
            {
                loggerConfiguration
                .WriteTo
                .MSSqlServer(databaseConfiguration.ConnectionString, option, null, null,
                    logLevel.ToLogEventLevel(), columnOptions: columnOptions);
            }
            else
            {
                
                loggerConfiguration
                .WriteTo
                .MSSqlServer(databaseConfiguration.ConnectionString, option, null, null,
                    logLevel.ToLogEventLevel());
            }
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
                .File(logFilePath, logLevel.ToLogEventLevel(), fileConfiguration.Format,
                    fileSizeLimitBytes: fileConfiguration.FileSizeLimitBytes,
                    retainedFileCountLimit: fileConfiguration.NumberOfFilesRetained,
                    shared: fileConfiguration.IsMultiProcessShared);
            //.Sink(new RollingFileSink(
            //    logFilePath,
            //    new MessageTemplateTextFormatter(fileConfiguration.Format, null),
            //    fileConfiguration.FileSizeLimitBytes,
            //    fileConfiguration.NumberOfFilesRetained,
            //    shared: fileConfiguration.IsMultiProcessShared),
            //    logLevel.ToLogEventLevel());


        }
    }
}
