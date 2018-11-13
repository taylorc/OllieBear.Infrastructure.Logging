using Infrastructure.Logging.Enums;
using Infrastructure.Logging.Utils;

namespace Infrastructure.Logging
{
    public class LoggingFileConfiguration
    {
        public string FilePath { get; set; }

        public string Format { get; set; } = Constants.DefaultLogFormat;

        public long FileSizeLimitBytes { get; set; } = int.MaxValue;

        public int? NumberOfFilesRetained { get; set; } = 14;

        public string MinimumLogLevel { get; set; } = LogLevel.Verbose.ToString();
        public bool IsMultiProcessShared { get; set; }
    }
}
