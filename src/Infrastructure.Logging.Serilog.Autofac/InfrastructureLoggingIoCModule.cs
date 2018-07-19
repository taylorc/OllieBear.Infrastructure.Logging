using Autofac;
using Infrastructure.Logging.Interfaces;

namespace Infrastructure.Logging.Serilog.Autofac
{
    public class InfrastructureLoggingIoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoggerFactory>()
                .As<ILogFactory>();

            builder
                .Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var factory = context.Resolve<ILogFactory>();

                    return factory.BuildLog();
                })
                .As<ILog>()
                .SingleInstance();
        }
    }
}
