namespace Infrastructure.Logging
{
    public interface ILogFactory
    {
        ILog BuildLog();
    }
}
