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
            _context.ActInjectDependencies();
            _context.AssertResolved();
        }

        private class TestContext : BaseResolverTestContext
        {
            public void ActInjectDependencies()
            {
                Services.AddSerilogLogging();

                ServiceProvider = Services.BuildServiceProvider();
            }
        }
    }
}
