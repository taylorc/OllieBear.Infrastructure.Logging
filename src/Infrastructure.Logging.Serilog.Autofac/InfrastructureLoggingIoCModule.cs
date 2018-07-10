using System.Collections.Generic;
using Autofac;
using Infrastructure.Logging.Serilog.Autofac.Extensions;
using Infrastructure.Logging.Serilog.Factories;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Infrastructure.Logging.Serilog.Autofac
{
    public class InfrastructureLoggingIoCModule : Module
    {
        private readonly IConfiguration _configuration;

        public InfrastructureLoggingIoCModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        { 
            var applicationName = GetApplicationName();

            var targets = new List<ILogTarget>();

            targets.AddDefaults(applicationName);

            builder.RegisterSinksFor(targets);

            builder.RegisterType<SerilogFactory>()
                .WithParameter("applicationName", applicationName)
                .AsSelf();

            builder
                .Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var factory = context.Resolve<SerilogFactory>();

                    return factory.CreateLogger();
                })
                .As<ILogger>()
                .SingleInstance();

            builder
                .RegisterType<Logger>()
                .As<ILog>()
                .SingleInstance();
        }

        private string GetApplicationName()
        {
            var name = _configuration["ApplicationName"];

            return name;
        }
    }
}
