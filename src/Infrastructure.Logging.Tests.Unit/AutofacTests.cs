using Autofac;
using Autofac.Extensions.DependencyInjection;
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
            _context.ActRegisterAutofacModule();
            _context.AssertResolved();
        }

        private class TestContext : BaseResolverTestContext
        {
            public void ActRegisterAutofacModule()
            {
                var containerBuilder = new ContainerBuilder();

                containerBuilder.Populate(Services);

                containerBuilder.RegisterModule<InfrastructureLoggingIoCModule>();

                var container = containerBuilder.Build();

                ServiceProvider = new AutofacServiceProvider(container);
            }
        }
    }
}
