using System;

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
            _logger.Verbose("Verbose log entry");
            _logger.Debug("Debug log entry");
            _logger.Info("Info log entry");
            _logger.Warning("Warning log entry");
            _logger.Error("Error log entry");
            _logger.Fatal("Fatal log entry");

            Console.ReadKey();
        }
    }
}
