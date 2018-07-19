namespace Infrastructure.Logging.Interfaces
{
    public interface ILog
    {
        void Verbose(string message);

        void Verbose(string messageTemplate, params object[] propertyValues);

        void Debug(string message);

        void Debug(string messageTemplate, params object[] propertyValues);

        void Info(string message);

        void Info(string messageTemplate, params object[] propertyValues);

        void Warning(string message);

        void Warning(string messageTemplate, params object[] propertyValues);

        void Error(string message);

        void Error(string messageTemplate, params object[] propertyValues);

        void Fatal(string message);

        void Fatal(string messageTemplate, params object[] propertyValues);
    }
}
