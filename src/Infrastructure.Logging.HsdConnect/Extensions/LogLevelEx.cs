using System;
using Infrastructure.Logging.Enums;
using Serilog.Events;

namespace Infrastructure.Logging.HsdConnect.Extensions
{
    internal static class LogLevelEx
    {
        public static LogEventLevel ToLogEventLevel(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return LogEventLevel.Debug;

                case LogLevel.Error:
                    return LogEventLevel.Error;

                case LogLevel.Fatal:
                    return LogEventLevel.Fatal;

                case LogLevel.Information:
                    return LogEventLevel.Information;

                case LogLevel.Verbose:
                    return LogEventLevel.Verbose;

                case LogLevel.Warning:
                    return LogEventLevel.Warning;

                case LogLevel.NotSet:
                    return LogEventLevel.Verbose;

                default:
                    throw new ArgumentNullException(nameof(logLevel));
            }
        }
    }
}