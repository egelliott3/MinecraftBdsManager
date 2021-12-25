using System.Diagnostics;

namespace MinecraftBdsManager.Logging
{
    /// <summary>
    /// TraceListener that holds all log lines in memory to allow easy inspection of the log by any code/thread running
    /// </summary>
    internal class LogMonitoringTraceListener : TraceListener
    {
        public LogMonitoringTraceListener(string? listenerName = default) : base(listenerName) { }

        public override void Write(string? message)
        {
            LogMonitor.Append(message!);
        }

        public override void WriteLine(string? message)
        {
            LogMonitor.AppendLine(message!);
        }
    }
}
