using System;
using System.Collections.Generic;
using Autofac;
using Infrastructure.Logging.Enums;
using Infrastructure.Logging.LoggingTargets;
using Infrastructure.Logging.Serilog.Factories;
using Serilog.Core;
using Serilog.Sinks.RollingFile;

namespace Infrastructure.Logging.Serilog.Autofac.Extensions
{
    internal static class ContainerBuilderEx
    {
        public static void RegisterSinksFor(this ContainerBuilder builder, IEnumerable<ILogTarget> targets)
        {
            var levelLookup = new Dictionary<Type, LogLevel>();

            foreach (var t in targets)
            {
                if (t is RollingFileLoggingTarget)
                {
                    RegisterSink(t, typeof(RollingFileSink),
                        () => builder
                            .Register(c => SinkFactory.For(t as RollingFileLoggingTarget))
                            .As<ILogEventSink>(),
                        levelLookup);
                }
            }

            builder
                .Register(c => levelLookup)
                .As<IDictionary<Type, LogLevel>>();
        }

        private static void RegisterSink<T>(
            T target,
            Type type,
            Action registerSink,
            IDictionary<Type, LogLevel> levelLookup) where T : ILogTarget
        {
            registerSink();

            levelLookup.Add(type, target.LogLevel);
        }
    }
}
