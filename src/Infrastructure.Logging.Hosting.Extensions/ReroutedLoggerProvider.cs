using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging.Hosting.Extensions
{
    public class ReroutedLoggerProvider : ILoggerProvider
    {
        private readonly ILog _logger;

        public ReroutedLoggerProvider(ILog logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ReroutedLogger(_logger);
        }
    }
}
