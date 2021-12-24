using MinecraftBdsManager.Configuration;
using System.Diagnostics;

namespace MinecraftBdsManager.Logging
{
    internal static class LogManager
    {
        static LogManager()
        {
            Trace.AutoFlush = true;
        }

        internal static string? CurrentLogFilePath { get; private set; }

        private static void RemoveOldLogFiles()
        {
            var loggingFilePaths = Directory.GetFiles(Settings.CurrentSettings.LoggingSettings.FileLoggingDirectoryPath, "*.log");
            // Get the number of files that we already have and count the one that we're creating with this new instance, even though it is not on disk yet.
            var numberOfLogFilesOnDisk = loggingFilePaths.Length + 1;

            if (numberOfLogFilesOnDisk > Settings.CurrentSettings.LoggingSettings.MaximumNumberOfLogFilesToKeep)
            {
                for(int i = 0; i < (numberOfLogFilesOnDisk - Settings.CurrentSettings.LoggingSettings.MaximumNumberOfLogFilesToKeep); i++)
                {
                    File.Delete(loggingFilePaths[i]);
                }
            }
        }

        public static void LogError(string message)
        {
            Trace.TraceError(string.Concat(LoggingLeadIn.SystemError, " ", message));
        }

        public static void LogInformation(string message)
        {
            Trace.TraceInformation(string.Concat(LoggingLeadIn.SystemInfo, " ", message));
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
                Trace.WriteLine(string.Concat(loggingLeadIn, " ", message));
            }
            else
            {
                Trace.WriteLine(message);
            }
        }

        public static void LogWarning(string message)
        {
            Trace.TraceInformation(string.Concat(LoggingLeadIn.SystemWarning, " ", message));
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

            // Format the log filename to be "MinecraftBdsManager_2009-06-15T134530Z.log using UTC time and...
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

        public static void RegisterUILogger(RichTextBox richTextBox, string listenerName = "RichTextBoxLogger", bool unregisterExistingListener = false)
        {
            if (Trace.Listeners[listenerName] != null || unregisterExistingListener)
            {
                Trace.Listeners.Remove(listenerName);
            }


            if (Trace.Listeners[listenerName] == null)
            {
                Trace.Listeners.Add(new RichTextboxTraceListener(richTextBox, listenerName));
            }
        }
    }
}
