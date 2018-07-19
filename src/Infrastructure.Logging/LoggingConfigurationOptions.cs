namespace Infrastructure.Logging
{
    public class LoggingConfigurationOptions
    {
        public string ApplicationName { get; set; }

        public LoggingFileConfiguration LoggingFileConfiguration { get; set; }

        public bool HasFileConfiguration => LoggingFileConfiguration != null;
    }
}
         
