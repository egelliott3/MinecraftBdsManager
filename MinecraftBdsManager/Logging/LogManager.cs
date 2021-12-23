using System.Diagnostics;

namespace MinecraftBdsManager.Logging
{
    internal class LogManager
    {
        public static void LogError(string message)
        {
            Trace.TraceError(string.Concat(LoggingLeadIn.SystemError, message));
        }

        public static void LogInformation(string message)
        {
            Trace.TraceInformation(string.Concat(LoggingLeadIn.SystemInfo, message));
        }

        public static void LogVerbose(string loggingLeadIn, string message)
        {
            // Nothing to log so just leave
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (string.IsNullOrEmpty(loggingLeadIn))
            {
                Trace.WriteLine(string.Concat(loggingLeadIn, message));
            }
            else
            {
                Trace.WriteLine(message);
            }
        }

        public static void LogWarning(string message)
        {
            Trace.TraceInformation(string.Concat(LoggingLeadIn.SystemWarning, message));
        }

        public static void RegisterFileLogger(string loggingFilePath, string listenerName = "FileLogger", bool unregisterExistingListener = false)
        {
            if (Trace.Listeners[listenerName] != null || unregisterExistingListener)
            {
                Trace.Listeners.Remove(listenerName);
            }

            if (Trace.Listeners[listenerName] == null)
            {
                Trace.Listeners.Add(new TextWriterTraceListener(loggingFilePath, listenerName));
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
