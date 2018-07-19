namespace Infrastructure.Logging.Sample.Host
{
    public class Service : IService
    {
        private readonly ILog _logger;

        public Service(ILog logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.Info("This is a test log entry");
        }
    }
}
