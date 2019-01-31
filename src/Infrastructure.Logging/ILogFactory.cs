namespace Infrastructure.Logging
{
    public interface ILogFactory
    {
        ILoggerItem BuildLoggerItem();
    }
}
