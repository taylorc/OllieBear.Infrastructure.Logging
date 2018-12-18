using System;
using Infrastructure.Logging.Extensions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging.Hosting.Extensions
{
    public class ReroutedLogger : ILogger
    {
        private readonly ILog _logger;

        public ReroutedLogger(ILog logger)
        {
            _logger = logger;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (exception != null)
                formatter = (s, e) => e.DeepException();

            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logger.Verbose($"{formatter(state, exception)}");
                    break;
                case LogLevel.Debug:
                    _logger.Debug($"{formatter(state, exception)}");
                    break;
                case LogLevel.Critical:
                    _logger.Fatal($"{formatter(state, exception)}");
                    break;
                case LogLevel.Error:
                    _logger.Error($"{formatter(state, exception)}");
                    break;
                case LogLevel.Warning:
                    _logger.Warning($"{formatter(state, exception)}");
                    break;
                case LogLevel.Information:
                    _logger.Info($"{formatter(state, exception)}");
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}