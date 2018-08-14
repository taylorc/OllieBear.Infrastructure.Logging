using System.Collections.Generic;
using Infrastructure.Logging.Enums;

namespace Infrastructure.Logging
{
    public class LoggingConfigurationOptions
    {
        public string ApplicationName { get; set; }
        
        public string ConsoleMinimumLogLevel { get; set; } = LogLevel.Verbose.ToString();

        public IEnumerable<LoggingFileConfiguration> LoggingFileConfigurations { get; set; }
    }
}
         
