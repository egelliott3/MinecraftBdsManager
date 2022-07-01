namespace MinecraftBdsManager.Configuration
{
    internal class BackupSettings
    {
        public enum CloudType
        {
            None = 0,
            AzureBlob,
            AmazonS3
        }

        public string BackupDirectoryPath { get; set; } = ".\\Backups";

        public bool BackupOnServerStart { get; set; } = false;

        public bool BackupOnServerStop { get; set; } = true;

        public string CloudBackupConnectionString { get; set; } = string.Empty;

        public CloudType CloudBackupType { get; set; } = BackupSettings.CloudType.None;

        public bool EnableAutomaticBackups { get; set; } = true;

        public int AutomaticBackupIntervalInMinutes { get; set; } = 60;

        public bool OnlyBackupIfUsersWereOnline { get; set; } = true;

        public int KeepBackupsForNumberOfDays { get; set; } = 2;

        public int KeepDailyBackupsForNumberOfDays { get; set; } = 7;

        public bool CompressToArchiveFile { get; set; } = false;
    }
}
