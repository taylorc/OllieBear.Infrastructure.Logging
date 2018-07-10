namespace Infrastructure.Logging.Utils
{
    internal static class Constants
    {
        public const string DefaultLogFormat =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff}\t" +
            "{ThreadId}\t" +
            "{MachineName}\t" +
            "{EnvironmentUserName}\t" +
            "[{Application}]\t" +
            "[{Level}]\t" +
            "{Message}" +
            "{NewLine}" +
            "{Exception}";
    }
}