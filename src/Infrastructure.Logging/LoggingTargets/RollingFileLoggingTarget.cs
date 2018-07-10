using Infrastructure.Logging.Enums;
using Infrastructure.Logging.Utils;

namespace Infrastructure.Logging.LoggingTargets
{
    public class RollingFileLoggingTarget : ILogTarget
    {
        public int FileSizeLimitBytes { get; set; } = int.MaxValue;

        public int NumberOfFilesRetained { get; set; } = 14;

        public string FilePath { get; set; }

        public string Format { get; set; } = Constants.DefaultLogFormat;

        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        public static RollingFileLoggingTarget DefaultConfiguration(string applicationName)
        {
            return new RollingFileLoggingTarget
            {
                FilePath = SystemDriveUtils.GetFilePath(applicationName)
            };
        }
    }
}
