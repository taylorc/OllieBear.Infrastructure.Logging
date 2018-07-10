using System;
using System.Collections.Generic;
using Infrastructure.Logging.Enums;
using Infrastructure.Logging.Serilog.Extensions;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers;

namespace Infrastructure.Logging.Serilog.Factories
{
    public class SerilogFactory
    {
        private readonly IEnumerable<ILogEventSink> _targets;
        private readonly IDictionary<Type, LogLevel> _levelLookup;
        private readonly string _applicationName;

        public SerilogFactory(
            IEnumerable<ILogEventSink> targets,
            IDictionary<Type, LogLevel> levelLookup,
            string applicationName)
        {
            _targets = targets;
            _levelLookup = levelLookup;
            _applicationName = applicationName;
        }

        public ILogger CreateLogger()
        {
            var loggerConfiguration = new LoggerConfiguration();

            loggerConfiguration
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.With(new MachineNameEnricher())
                .Enrich.With(new EnvironmentUserNameEnricher())
                .Enrich.WithProperty("Application", _applicationName)
                .MinimumLevel.Verbose();

            foreach (var t in _targets)
            {
                var level = _levelLookup[t.GetType()].ToLogEventLevel();

                loggerConfiguration
                    .WriteTo
                    .Sink(t, level);
            }

            return loggerConfiguration
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
