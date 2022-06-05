using MinecraftBdsManager.Configuration;
using MinecraftBdsManager.Logging;
using System.Diagnostics;

namespace MinecraftBdsManager.Managers
{
    internal static class LogManager
    {
        static LogManager()
        {
            Trace.AutoFlush = true;
        }

        internal static string? CurrentLogFilePath { get; private set; }

        public static void LogError(string message)
        {
            Trace.TraceError(string.Concat(LoggingLeadIn.BuildLeadIn(LoggingLeadIn.SystemErrorMessage), " ", message));
        }

        public static void LogInformation(string message)
        {
            Trace.TraceInformation(string.Concat(LoggingLeadIn.BuildLeadIn(LoggingLeadIn.SystemInfoMessage), " ", message));
        }

        public static void LogVerbose(string loggingLeadIn, string message)
        {
            // Nothing to log so just leave
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(loggingLeadIn))
            {
                Trace.WriteLine(message);
            }
            else
            {
                Trace.WriteLine(string.Concat(loggingLeadIn, " ", message));
            }
        }

        public static void LogWarning(string message)
        {
            Trace.TraceInformation(string.Concat(LoggingLeadIn.BuildLeadIn(LoggingLeadIn.SystemWarningMessage), " ", message));
        }

        public static void RegisterFileLogger(string loggingFilePath, string listenerName = "FileLogger", bool unregisterExistingListener = false)
        {
            if (string.IsNullOrWhiteSpace(loggingFilePath))
            {
                LogWarning($"Unable to enable logging to file as the {nameof(Settings.LoggingSettings.FileLoggingDirectoryPath)} was invalid and/or not supplied.");
                return;
            }

            var internalLoggingFilePath = Path.GetFullPath(loggingFilePath);
            if (!Directory.Exists(internalLoggingFilePath))
            {
                Directory.CreateDirectory(internalLoggingFilePath);
            }

            // Format the log filename to be "MinecraftBdsManager_2009-06-15T134530Z.log" using UTC time and...
            var formattedCurrentUtcDateTime = $"{DateTime.UtcNow:O}";
            // ... taking out the colons (:) in order to not make Windows file system upset and...
            formattedCurrentUtcDateTime = formattedCurrentUtcDateTime.Replace(":", string.Empty);
            // ... remove the milliseconds/fractional seconds as they are not super useful and the plop the Z back on the end to keep the UTC signifier and...
            formattedCurrentUtcDateTime = string.Concat(formattedCurrentUtcDateTime.Substring(0, formattedCurrentUtcDateTime.IndexOf(".")), "Z");
            // ... finally slap .log on the end with the name of the app as a lead in.
            var loggingFileName = $"{nameof(MinecraftBdsManager)}_{formattedCurrentUtcDateTime}.log";

            if (Trace.Listeners[listenerName] != null || unregisterExistingListener)
            {
                Trace.Listeners.Remove(listenerName);
            }

            if (Trace.Listeners[listenerName] == null)
            {
                CurrentLogFilePath = Path.GetFullPath(Path.Combine(loggingFilePath, loggingFileName));

                Trace.Listeners.Add(new TextWriterTraceListener(CurrentLogFilePath, listenerName));

                RemoveOldLogFiles();
            }
        }

        public static void RegisterLogMonitor()
        {
            var listenerName = "LogMonitorListener";
            if (Trace.Listeners[listenerName] == null)
            {
                Trace.Listeners.Add(new LogMonitoringTraceListener(listenerName));
            }
        }

        public static void RegisterUILogger(RichTextBox richTextBox, string listenerName = "RichTextBoxLogger", bool unregisterExistingListener = false)
        {
            if (unregisterExistingListener)
            {
                UnregisterUILogger(richTextBox, listenerName);
            }

            if (Trace.Listeners[listenerName] == null)
            {
                Trace.Listeners.Add(new RichTextboxTraceListener(richTextBox, listenerName));
            }
        }

        private static void RemoveOldLogFiles()
        {
            // Grab the root of the logging directory path to get information about its files
            DirectoryInfo loggingDirectoryInfo = new(Settings.CurrentSettings.LoggingSettings.FileLoggingDirectoryPath);

            FileInfo[] logFileInfos = loggingDirectoryInfo.GetFiles("*.log");

            // Establish time borders for when logs should be removed.
            //  If they user has specified 0 keep days, this means keep forever so set the threshold to DateTime.MaxValue, otherwise compute the appropriate threshold
            DateTime logDeleteDateThreshold =
                Settings.CurrentSettings.LoggingSettings.KeepLogsForNumberOfDays == 0
                ? DateTime.MaxValue
                : DateTime.Now.AddDays(-Settings.CurrentSettings.LoggingSettings.KeepLogsForNumberOfDays);

            foreach(FileInfo logInfo in logFileInfos)
            {
                if (logInfo.LastWriteTime <= logDeleteDateThreshold)
                {
                    logInfo.Delete();
                }
            }
        }

        public static void UnregisterUILogger(RichTextBox richTextBox, string listenerName = "RichTextBoxLogger")
        {
            if (Trace.Listeners[listenerName] != null)
            {
                Trace.Listeners.Remove(listenerName);
            }
        }
    }
}
