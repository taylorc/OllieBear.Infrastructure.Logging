using System.Collections.Generic;
using System.Linq;
using Infrastructure.Logging.LoggingTargets;

namespace Infrastructure.Logging.Serilog.Factories
{
    public static class DefaultTargetsFactory
    {
        public static void AddDefaults(
            this ICollection<ILogTarget> targets,
            string applicationName)
        {
            if (targets.DoesNotIncludeFileLogger())
                targets.Add(RollingFileLoggingTarget.DefaultConfiguration(applicationName));
        }

        private static bool DoesNotIncludeFileLogger(this IEnumerable<ILogTarget> targets)
        {
            return targets.All(t => t.GetType() != typeof(RollingFileLoggingTarget));
        }
    }
}
