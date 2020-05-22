using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Sample.Host
{
    public class Service : IService
    {
        private readonly IJobRun _jobRun;
        

        public Service(IJobRun jobRun)
        {
           _jobRun =jobRun;
        }

        public void Run()
        {            
            //_jobRun.RunLogs(-1);

            Parallel.For(0,10,i=>_jobRun.RunLogs(i));            

            Console.ReadKey();
        }

    }

    public class JobRun : IJobRun
    {
        private readonly ILog _logger;
        private readonly IJobRunTwo _jobRunTwo;

        public JobRun(ILog logger, IJobRunTwo jobRunTwo)
        {
            _logger = logger;
            _jobRunTwo = jobRunTwo;
        }
        public void RunLogs(int i) {

                using(LogContext.PushProperty("JobName", $"{i} name")){
                _logger.Info("Run {0}", i);
                _logger.Verbose("Verbose log entry {0}", i);
                _logger.Debug("Debug log entry {0}", i);
                _logger.Info("Info log entry {0}", i);
                _logger.Warning("Warning log entry {0}", i);
                _logger.Error("Error log entry {0}", i);
                _logger.Fatal("Fatal log entry {0}", i);
            };


            _jobRunTwo.RunLogs(i*100);
        }

    }

    public class JobRunTwo: IJobRunTwo{
        private readonly ILog _logger;

        public JobRunTwo( ILog logger)
        {
            _logger = logger;
        }

        public void RunLogs(int i) {
                _logger.Info("Too Run {0}", i);
        }

        }

    public interface IJobRunTwo:IJobRun
    {
    }

    public interface IJobRun
    {
        void RunLogs(int i);
    }
}
