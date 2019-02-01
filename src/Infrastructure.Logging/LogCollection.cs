using System;
using System.Collections.Generic;

namespace Infrastructure.Logging
{
    public class LogCollection : ILog
    {
        private readonly IEnumerable<ILoggerEntity> _loggers;

        public IEnumerable<ILoggerEntity> GetLoggers() => _loggers;

        public LogCollection(IEnumerable<ILoggerEntity> loggers)
        {
            _loggers = loggers;
        }

        private void WriteToLogs(Action<ILoggerEntity, string> logAction, string messageTemplate)
        {
            foreach (var logger in _loggers)
                logAction(logger, messageTemplate);
        }

        private void WriteToLogs(Action<ILoggerEntity, string, object[]> logAction, string messageTemplate, params object[] propertyValues)
        {
            foreach (var logger in _loggers)
                logAction(logger, messageTemplate, propertyValues);
        }

        public void Verbose(string message) => WriteToLogs((l, s) => l.Verbose(s), message);
        public void Debug(string message) => WriteToLogs((l, s) => l.Debug(s), message);
        public void Info(string message) => WriteToLogs((l, s) => l.Info(s), message);
        public void Warning(string message) => WriteToLogs((l, s) => l.Warning(s), message);
        public void Error(string message) => WriteToLogs((l, s) => l.Error(s), message);
        public void Fatal(string message) => WriteToLogs((l, s) => l.Fatal(s), message);

        public void Verbose(string messageTemplate, params object[] propertyValues) => WriteToLogs((l, s, p) => l.Verbose(s, p), messageTemplate, propertyValues);
        public void Debug(string messageTemplate, params object[] propertyValues) => WriteToLogs((l, s, p) => l.Debug(s, p), messageTemplate, propertyValues);
        public void Info(string messageTemplate, params object[] propertyValues) => WriteToLogs((l, s, p) => l.Info(s, p), messageTemplate, propertyValues);
        public void Warning(string messageTemplate, params object[] propertyValues) => WriteToLogs((l, s, p) => l.Warning(s, p), messageTemplate, propertyValues);
        public void Error(string messageTemplate, params object[] propertyValues) => WriteToLogs((l, s, p) => l.Error(s, p), messageTemplate, propertyValues);
        public void Fatal(string messageTemplate, params object[] propertyValues) => WriteToLogs((l, s, p) => l.Fatal(s, p), messageTemplate, propertyValues);
    }
}
