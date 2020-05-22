using Serilog.Context;
using System;

namespace Infrastructure.Logging.Serilog
{
    public class SerilogLogContext : ILogContext
    {
        public IDisposable PushProperty(string key, string value)
        {
            return LogContext.PushProperty(key, value);
        }
    }
}
