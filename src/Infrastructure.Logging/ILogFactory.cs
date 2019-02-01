namespace Infrastructure.Logging
{
    public interface ILogFactory
    {
        ILoggerEntity BuildLoggerEntity();
    }
}
