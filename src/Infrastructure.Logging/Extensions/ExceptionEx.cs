using System;
using System.Text;

namespace Infrastructure.Logging.Extensions
{
    public static class ExceptionEx
    {
        public static string DeepException(this Exception exception)
        {
            var message = new StringBuilder();

            do
            {
                message.AppendLine($"-> {exception?.Message}");
            }
            while ((exception = exception?.InnerException) != null);

            return message.ToString();
        }
    }
}
