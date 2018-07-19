using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
