using Autofac;

namespace Infrastructure.Logging.Serilog.Autofac
{
    public class InfrastructureLoggingIoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<LogCollection>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(ILog));

            builder.RegisterType<SerilogFactory>()
                .As<ISerilogFactory>();

            builder
                .Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var factory = context.Resolve<ISerilogFactory>();

                    return factory.BuildLoggerItem();
                })
                .As<ILoggerItem>()
                .SingleInstance();
        }
    }
}
