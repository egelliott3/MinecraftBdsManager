using MinecraftBdsManager.Managers;
using System.Collections.Concurrent;

namespace MinecraftBdsManager.Logging
{
    internal static class LogMonitor
    {
        private static string _currentLine = string.Empty;
        private static long _currentLineNumber = 0;
        private static readonly object _currentLineUpdateLockableObject = new();
        private static readonly object _monitoredLinesLockableObject = new();

        public static event EventHandler<LineReadEventArgs>? LineRead;

        static LogMonitor()
        {
            MonitoredLogLines = new();
        }

        public static long CurrentLineNumber => _currentLineNumber;

        public static ConcurrentDictionary<long, string> MonitoredLogLines { get; private set; }

        public static void Append(string message)
        {
            // Going to be brave and believe that all calls to both Write and Writeline are concurrent and not async.  Meaning, that if we get a Write, or series of Writes, they should always
            //  be followed by a WriteLine
            lock (_currentLineUpdateLockableObject)
            {
                _currentLine = string.Concat(_currentLine, message);
            }
        }

        public static void AppendLine(string message)
        {
            // Going to be brave and believe that all calls to both Write and Writeline are concurrent and not async.  Meaning, that if we get a Write, or series of Writes, they should always
            //  be followed by a WriteLine
            lock (_currentLineUpdateLockableObject)
            {
                _currentLine = string.Concat(_currentLine, message);

                lock (_monitoredLinesLockableObject)
                {
                    Interlocked.Increment(ref _currentLineNumber);
                    MonitoredLogLines.AddOrUpdate(_currentLineNumber, _currentLine, (key, oldLine) => _currentLine);

                    if (LineRead != null)
                    {
                        LineRead(typeof(LogMonitor), new LineReadEventArgs { Line = _currentLine });
                    }
                }

                _currentLine = string.Empty;
            }
        }

        public static DateTime? ReadDateTimeFromLogLine(string logLine)
        {
            var unableToParseMessage = $"Unable to parse date and time from log line {logLine}.";

            // Format for lines with a timestamp is MinecraftBdsManager Information: 0 : [2021-12-24 11:50:57:895 INFO] log message
            int afterOpeningBracketIndex = logLine.IndexOf('[') + 1;
            int beforeClosingBracketIndex = logLine.IndexOf(']');

            // Check the indexes to be sure they are valid.  If they are not then return null
            if (afterOpeningBracketIndex == -1 || beforeClosingBracketIndex == -1 || beforeClosingBracketIndex < afterOpeningBracketIndex)
            {
                LogManager.LogWarning(unableToParseMessage);
                return null;
            }

            // Now we should have 2021-12-24 11:50:57:895 INFO
            string logLineSubstring = logLine[afterOpeningBracketIndex..beforeClosingBracketIndex];

            // Pull out the part that should just be DateTime
            int spaceIndex = logLineSubstring.LastIndexOf(" ");
            if (spaceIndex == -1)
            {
                LogManager.LogWarning(unableToParseMessage);
                return null;
            }
            string expectedDateTimeString = logLineSubstring[0..spaceIndex];

            // The log format has milliseconds on the end which default DateTime parse will not like.  Will strip them off since we don't need them
            int lastColonIndex = expectedDateTimeString.LastIndexOf(':');
            if (lastColonIndex == -1)
            {
                LogManager.LogWarning(unableToParseMessage);
                return null;
            }
            expectedDateTimeString = expectedDateTimeString.Substring(0, lastColonIndex);

            // Finally try to parse it into a DateTime
            if (DateTime.TryParse(expectedDateTimeString, out DateTime dateTime))
            {
                return dateTime;
            }

            LogManager.LogWarning(unableToParseMessage);

            return null;
        }

        internal class LineReadEventArgs : EventArgs
        {
            public string Line { get; set; } = string.Empty;
        }
    }
}
