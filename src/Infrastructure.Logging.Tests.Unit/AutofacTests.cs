using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Logging.Serilog;
using Infrastructure.Logging.Serilog.Autofac;
using Xunit;

namespace Infrastructure.Logging.Tests.Unit
{
    public class AutofacTests
    {
        private readonly TestContext _context;

        public AutofacTests()
        {
            _context = new TestContext();
        }

        [Fact]
        public void Can_resolve_default_interfaces()
        {
            _context.ArrangeContainerConfiguration();
            _context.ArrangeContainerBuilder();
            _context.ActRegisterSerilogFileLogger();
            _context.ActBuildServiceProvider();
            _context.AssertResolved();
        }

        [Fact]
        public void Can_resolve_serilog()
        {
            _context.ArrangeContainerConfiguration();
            _context.ArrangeContainerBuilder();
            _context.ActRegisterSerilogFileLogger();
            _context.ActBuildServiceProvider();
            _context.AssertIncludesSingleLoggerOnly<SerilogFileLogger>();
        }

        [Fact]
        public void Can_resolve_concurrent_loggers()
        {
            _context.ArrangeContainerConfiguration();
            _context.ArrangeContainerBuilder();
            _context.ActRegisterSerilogFileLogger();
            _context.ActBuildServiceProvider();
            _context.AssertIncludesTwoLoggers();
        }

        private class TestContext : BaseResolverTestContext
        {
            private readonly ContainerBuilder _containerBuilder;

            public TestContext()
            {
                _containerBuilder = new ContainerBuilder();
            }

            public void ArrangeContainerBuilder()
            {
                _containerBuilder.Populate(Services);
            }

            public void ActRegisterSerilogFileLogger()
            {
                _containerBuilder.RegisterModule<InfrastructureLoggingIoCModule>();
            }

            public void ActBuildServiceProvider()
            {
                var container = _containerBuilder.Build();

                ServiceProvider = new AutofacServiceProvider(container);
            }
        }
    }
}
