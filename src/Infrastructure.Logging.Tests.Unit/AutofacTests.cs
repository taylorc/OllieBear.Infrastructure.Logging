using System.IO;
using Autofac;
using Infrastructure.Logging.Serilog.Autofac;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Infrastructure.Logging.Tests.Unit
{
    [TestFixture]
    internal class AutofacTests
    {
        [Test]
        public void Test_Resolve_Default()
        {
            var builder = new ContainerBuilder();

            var configurationBuilder = new ConfigurationBuilder();

            var configuration = configurationBuilder.Build();

            builder.RegisterModule(new InfrastructureLoggingIoCModule(configuration));

            var container = builder.Build();

            var logger = container.Resolve<ILog>();

            Assert.IsNotNull(logger);
        }
    }
}
