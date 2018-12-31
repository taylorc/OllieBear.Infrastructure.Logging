using System;
using System.Diagnostics;

namespace Infrastructure.Logging.Extensions
{
    public static class ExceptionEx
    {
        public static string DeepException(this Exception exception)
        {
            // https://github.com/benaadams/Ben.Demystifier
            return exception.Demystify().ToString();
        }
    }
}
