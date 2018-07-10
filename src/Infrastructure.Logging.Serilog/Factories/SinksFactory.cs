using Infrastructure.Logging.LoggingTargets;
using Serilog.Core;
using Serilog.Formatting.Display;
using Serilog.Sinks.RollingFile;

namespace Infrastructure.Logging.Serilog.Factories
{
    public static class SinkFactory
    {
        public static ILogEventSink For(RollingFileLoggingTarget config)
        {
            return new RollingFileSink(
                config.FilePath,
                new MessageTemplateTextFormatter(config.Format, null),
                config.FileSizeLimitBytes,
                config.NumberOfFilesRetained);
        }
    }
}
