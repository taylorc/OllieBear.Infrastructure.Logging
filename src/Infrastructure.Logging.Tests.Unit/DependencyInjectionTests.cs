using Infrastructure.Logging.Serilog;
using Infrastructure.Logging.Serilog.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.Logging.Tests.Unit
{
    public class DependencyInjectionTests
    {
        private readonly TestContext _context;

        public DependencyInjectionTests()
        {
            _context = new TestContext();
        }

        [Fact]
        public void Can_resolve_default_interfaces()
        {
            _context.ArrangeContainerConfiguration();
            _context.ActRegisterSerilogFileLogger();
            _context.ActBuildServiceProvider();
            _context.AssertResolved();
        }

        [Fact]
        public void Can_resolve_serilog()
        {
            _context.ArrangeContainerConfiguration();
            _context.ActRegisterSerilogFileLogger();
            _context.ActBuildServiceProvider();
            _context.AssertIncludesSingleLoggerOnly<SerilogFileLogger>();
        }

        [Fact]
        public void Can_resolve_concurrent_loggers()
        {
            _context.ArrangeContainerConfiguration();
            _context.ActRegisterSerilogFileLogger();
            _context.ActBuildServiceProvider();
            _context.AssertIncludesTwoLoggers();
        }

        private class TestContext : BaseResolverTestContext
        {
            public void ActRegisterSerilogFileLogger()
            {
                Services.AddSerilogLogging();
            }

            public void ActBuildServiceProvider()
            {
                ServiceProvider = Services.BuildServiceProvider();
            }
        }
    }
}
