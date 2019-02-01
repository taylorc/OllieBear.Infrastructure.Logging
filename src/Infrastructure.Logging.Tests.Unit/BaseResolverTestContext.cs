using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infrastructure.Logging.Serilog;
using Xunit;

namespace Infrastructure.Logging.Tests.Unit
{
    internal class BaseResolverTestContext
    {
        protected ServiceCollection Services;
        protected IServiceProvider ServiceProvider;
        protected IConfigurationRoot Configuration;

        public BaseResolverTestContext()
        {
            Services = new ServiceCollection();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public void ArrangeContainerConfiguration()
        {
            Services
                .Configure<LoggingConfigurationOptions>(Configuration.GetSection("LoggingConfigurationOptions"));
        }

        public void AssertResolved()
        {
            Assert.NotNull(ServiceProvider.GetService<ILog>());
        }

        public void AssertIncludesSingleLoggerOnly<T>()
        {
            var logger = ServiceProvider.GetRequiredService<ILog>();

            var loggers = GetLoggersInCollection(logger);

            Assert.True(loggers.Count == 1);

            Assert.IsType<T>(loggers.ElementAt(0));
        }

        public void AssertIncludesTwoLoggers()
        {
            var logger = ServiceProvider.GetRequiredService<ILog>();

            var loggers = GetLoggersInCollection(logger);

            Assert.True(loggers.Count == 2);
        }

        private static ICollection<ILoggerItem> GetLoggersInCollection(ILog logger)
        {
            Assert.IsType<LogCollection>(logger);

            return ((LogCollection)logger)
                .GetLoggers()
                .ToList();
        }
    }
}
