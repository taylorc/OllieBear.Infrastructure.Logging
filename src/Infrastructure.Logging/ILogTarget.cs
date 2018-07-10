using Infrastructure.Logging.Enums;

namespace Infrastructure.Logging
{
    public interface ILogTarget
    {
        LogLevel LogLevel { get; set; }
    }
}
    