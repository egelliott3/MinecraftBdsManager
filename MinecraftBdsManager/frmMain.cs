using MinecraftBdsManager.Configuration;
using MinecraftBdsManager.Logging;
using MinecraftBdsManager.Managers;

namespace MinecraftBdsManager
{
    public partial class frmMain : Form
    {
        private bool _clearStatusBoxOnStart = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private async void btnIssueCommand_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btnIssueCommand.Text))
            {
                return;
            }

            await BdsManager.SendCommandAsync(txtCustomCommand.Text, userSentCommand: true);
            txtCustomCommand.Text = String.Empty;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessManager.Dispose(true);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Get those settings so we know how to handle things
            _ = Settings.LoadSettings();

            // Setup trace listener(s)

            //  Need one for the status textbox, which will always be present
            LogManager.RegisterUILogger(rtbStatus);

            //  Need one for the log monitor, which will always be present
            LogManager.RegisterLogMonitor();

            // If autostart is enabled then start the server
            if (Settings.CurrentSettings.AutoStartBedrockDedicatedServer)
            {
                LogManager.LogInformation("Auto starting Bedrock Dedicated Server.");
                toolBtnStart_Click(sender, e);
            }

            // Check to see if we should enable auto restart.  These settings should apply even if the server has not started yet.
            if (Settings.CurrentSettings.RestartSettings.EnableAutoRestart)
            {
                if (Settings.CurrentSettings.RestartSettings.EnableRestartOnInterval)
                {
                    RestartManager.EnableIntervalBasedRestart();
                }

                if (Settings.CurrentSettings.RestartSettings.EnableRestartOnSchedule)
                {
                    RestartManager.EnableScheduleBasedRestart();
                }
            }
        }

        private void rtbStatus_TextChanged(object sender, EventArgs e)
        {
            rtbStatus.SelectionStart = rtbStatus.Text.Length;
            rtbStatus.ScrollToCaret();
        }

        private async void toolBtnBackupNow_Click(object sender, EventArgs e)
        {
            LogManager.LogInformation("Starting backup...");

            var backupWasSuccessful = await BackupManager.CreateBackupAsync();

            if (backupWasSuccessful)
            {
                LogManager.LogInformation("Backup completed successfully");
            }
            else
            {
                LogManager.LogError("Backup failed.");
            }
        }

        private void toolBtnOpenLogsFolder_Click(object sender, EventArgs e)
        {
            ProcessManager.StartProcess(ProcessName.FireAndForget, "explorer.exe", Settings.CurrentSettings.LoggingSettings.FileLoggingDirectoryPath);
        }

        private void toolBtnOpenSavesFolder_Click(object sender, EventArgs e)
        {
            ProcessManager.StartProcess(ProcessName.FireAndForget, "explorer.exe", Settings.CurrentSettings.BackupSettings.BackupDirectoryPath);
        }

        private async void toolBtnStart_Click(object sender, EventArgs e)
        {
            toolBtnStart.Enabled = false;

            if (_clearStatusBoxOnStart)
            {
                rtbStatus.Clear();
            }

            //  If the user has asked for log files, create a new log file per start
            if (Settings.CurrentSettings.LoggingSettings.EnableLoggingToFile)
            {
                LogManager.RegisterFileLogger(Settings.CurrentSettings.LoggingSettings.FileLoggingDirectoryPath, unregisterExistingListener: true);
            }

            if (Settings.CurrentSettings.BackupSettings.BackupOnServerStart)
            {
                LogManager.LogInformation("Performing backup on start per user settings.");
                var backupWasSuccessful = await BackupManager.CreateBackupAsync();

                if (backupWasSuccessful)
                {
                    LogManager.LogInformation("Backup completed successfully");
                }
                else
                {
                    LogManager.LogError("Backup failed.");
                }
            }

            var success = await BdsManager.StartAsync();

            while (!BdsManager.ServerIsRunning)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            toolBtnStop.Enabled = success;
            toolBtnStart.Enabled = !success;
            _clearStatusBoxOnStart = true;

            // Check if the user has requested to enable automatic backups.
            if (Settings.CurrentSettings.BackupSettings.EnableAutomaticBackups)
            {
                BackupManager.EnableIntervalBasedBackups();
            }

            // Check if the user has requested to enable automatic map generation.
            if (Settings.CurrentSettings.MapSettings.EnableMapGeneration)
            {
                MapManager.EnableIntervalBasedMapGeneration();
            }
        }

        private async void toolBtnStop_Click(object sender, EventArgs e)
        {
            toolBtnStop.Enabled = false;

            await BdsManager.StopAsync();

            while (BdsManager.ServerIsRunning)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            toolBtnStart.Enabled = true;

            // Check if the user has requested to enable automatic backups.
            if (Settings.CurrentSettings.BackupSettings.EnableAutomaticBackups)
            {
                BackupManager.DisableIntervalBasedBackups();
            }

            // Check if the user wanted a backup on stop and if so, take one
            if (Settings.CurrentSettings.BackupSettings.BackupOnServerStop)
            {
                LogManager.LogInformation("Performing backup on stop per user settings.");
                var backupWasSuccessful = await BackupManager.CreateBackupAsync();

                if (backupWasSuccessful)
                {
                    LogManager.LogInformation("Backup completed successfully");
                }
                else
                {
                    LogManager.LogError("Backup failed.");
                }
            }
        }

        private void toolBtnViewLog_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LogManager.CurrentLogFilePath))
            {
                LogManager.LogWarning($"Unable to locate current log file path {LogManager.CurrentLogFilePath}");
                return;
            }

            ProcessManager.StartProcess(ProcessName.FireAndForget, "explorer.exe", LogManager.CurrentLogFilePath);
        }
    }
}
