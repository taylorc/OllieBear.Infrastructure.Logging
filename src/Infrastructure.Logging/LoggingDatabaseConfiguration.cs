using Infrastructure.Logging.Enums;

namespace Infrastructure.Logging
{
    public class LoggingDatabaseConfiguration
    {
        public string ConnectionString { get; set; }

        public string MinimumLogLevel { get; set; } = LogLevel.Verbose.ToString();
    }
}
