namespace MinecraftBdsManager.Logging
{
    internal static class LoggingLeadIn
    {
        public static string None = string.Empty;

        public static string SystemChat => "[ SYSTEM ] [ CHAT ]";
               
        public static string SystemError => "[ SYSTEM ] [ ERROR ]";
               
        public static string SystemInfo => "[ SYSTEM ]";
               
        public static string SystemWarning => "[ SYSTEM ] [ WARN ]";
               
        public static string UserSentMessage => "[ USER ]";
    }
}
