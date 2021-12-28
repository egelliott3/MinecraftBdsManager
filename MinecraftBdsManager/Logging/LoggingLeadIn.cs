using System.Text;

namespace MinecraftBdsManager.Logging
{
    internal static class LoggingLeadIn
    {
        public static string None = string.Empty;

        public static string SystemChatMessage => $"SYSTEM] [ CHAT ]";
               
        public static string SystemErrorMessage => $"SYSTEM] [ ERROR ]";
               
        public static string SystemInfoMessage => $"SYSTEM]";
               
        public static string SystemWarningMessage => $"SYSTEM] [ WARN ]";

        public static string Timestamp => $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}";
               
        public static string UserSentMessage => $"USER]";

        public static string BuildLeadIn(string leadInMessage)
        {
            return $"[{Timestamp} {leadInMessage}";
        }
    }
}
