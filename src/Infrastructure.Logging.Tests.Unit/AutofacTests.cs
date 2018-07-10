using Autofac;
using Infrastructure.Logging.Serilog.Autofac;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Infrastructure.Logging.Tests.Unit
{
    public class AutofacTests
    {
        [Fact]
        public void Test_Resolve_Default()
        {
            var builder = new ContainerBuilder();

            var configurationBuilder = new ConfigurationBuilder();

            var configuration = configurationBuilder.Build();

            builder.RegisterModule(new InfrastructureLoggingIoCModule(configuration));

            var container = builder.Build();

            var logger = container.Resolve<ILog>();

            Assert.NotNull(logger);
        }
    }
}
