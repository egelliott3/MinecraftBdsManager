namespace MinecraftBdsManager.Configuration
{
    internal class RestartSettings
    {

        /// <summary>
        /// True or false setting on whether or not the app should restart automatically if it closes unexpectedly.
        /// </summary>
        public bool EnableAutoRestart { get; set; } = true;

        public bool EnableRestartOnInterval { get; set; } = false;

        public int RestartIntervalInMinutes { get; set; } = 360;

        public bool EnableRestartOnSchedule { get; set; } = false;

        public TimeOnly[] RestartScheduleTimes24h { get; set; } = new TimeOnly[] { new TimeOnly(0, 0), new TimeOnly(6, 0), new TimeOnly(12, 0), new TimeOnly(18, 0) };
    }
}
