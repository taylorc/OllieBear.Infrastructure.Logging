using Infrastructure.Logging.Enums;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Logging
{
    public class LoggingDatabaseConfiguration
    {
        public string ConnectionString { get; set; }

        public string MinimumLogLevel { get; set; } = LogLevel.Verbose.ToString();
        public IEnumerable<LoggingAdditionalColumns> LoggingAdditionalColumns { get; set; }
    }
}
