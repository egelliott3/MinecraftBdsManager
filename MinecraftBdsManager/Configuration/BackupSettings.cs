namespace MinecraftBdsManager.Configuration
{
    internal class BackupSettings
    {
        public bool EnableBackupOnServerStart { get; set; } = false;

        public bool EnableBackupOnServerStop { get; set; } = true;

        public bool EnableAutomaticBackups { get; set; } = true;

        public int AutomaticBackupIntervalInMinutes { get; set; } = 60;

        public bool OnlyBackupIfUsersWereOnline { get; set; } = true;

        public int KeepLastNumberOfBackups { get; set; } = 5;

        public int KeepLastNumberOfDailyBackups { get; set; } = 7;
    }
}
