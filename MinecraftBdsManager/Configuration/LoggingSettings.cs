namespace MinecraftBdsManager.Configuration
{
    internal class LoggingSettings
    {
        public bool EnableLoggingToFile { get; set; } = false;

        public string FileLoggingDirectoryPath { get; set; } = @".\";

        public int KeepLogsForNumberOfDays { get; set; } = 3;
    }
}
