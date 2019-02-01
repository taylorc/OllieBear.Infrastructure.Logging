using Autofac;

namespace Infrastructure.Logging.HsdConnect.Autofac
{
    public class HsdConnectLoggingIoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<LogCollection>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(ILog));

            builder.RegisterType<HsdConnectFactory>()
                .As<IHsdConnectFactory>();

            builder
                .Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var factory = context.Resolve<IHsdConnectFactory>();

                    return factory.BuildLoggerEntity();
                })
                .As<ILoggerEntity>()
                .SingleInstance();
        }
    }
}
