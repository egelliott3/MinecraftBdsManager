namespace MinecraftBdsManager.Configuration
{
    internal class LoggingSettings
    {
        public bool EnableLoggingToFile { get; set; } = false;

        public string FileLoggingDirectoryPath { get; set; } = @".\";

        public int MaximumNumberOfLogFilesToKeep { get; set; } = 5;
    }
}
