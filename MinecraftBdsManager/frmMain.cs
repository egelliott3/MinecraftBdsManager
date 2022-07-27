using MinecraftBdsManager.Configuration;
using MinecraftBdsManager.Managers;
using System.Reflection;

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
            if (BdsManager.ServerIsRunning) 
            {
                await BdsManager.SendCommandAsync(txtCustomCommand.Text, userSentCommand: true);
                txtCustomCommand.Text = String.Empty;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessManager.Dispose(true);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Get version number so we can show it in the title.
            Version version = Assembly.GetExecutingAssembly().GetName().Version!;
            this.Text = String.Concat(this.Text, $" {version.Major}.{version.Minor}.{version.Build}");

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
        }

        private void rtbStatus_TextChanged(object sender, EventArgs e)
        {
            rtbStatus.SelectionStart = rtbStatus.Text.Length;
            rtbStatus.ScrollToCaret();
        }

        private async void toolBtnBackupNow_Click(object sender, EventArgs e)
        {
            LogManager.LogInformation("Starting backup...");

            var backupResult = await BackupManager.CreateBackupAsync();

            if (backupResult.WasSuccessful)
            {
                LogManager.LogInformation("Backup completed successfully");
            }
            else
            {
                LogManager.LogError("Backup failed.");
            }
        }

        private async void toolBtnMapNow_Click(object sender, EventArgs e)
        {
            LogManager.LogInformation("Starting mapping process...");

            var mappingWasSuccessful = await MapManager.GenerateMap(skipOnlineUserCheck: true);

            if (mappingWasSuccessful)
            {
                LogManager.LogInformation("Mapping process completed successfully");
            }
            else
            {
                LogManager.LogError("Mapping process failed.");
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

            var success = await BdsManager.StartAsync();

            while (!BdsManager.ServerIsRunning)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            toolBtnStop.Enabled = success;
            toolBtnStart.Enabled = !success;
            _clearStatusBoxOnStart = true;
        }

        private async void toolBtnStop_Click(object sender, EventArgs e)
        {
            toolBtnStop.Enabled = false;

            await BdsManager.StopAsync();

            toolBtnStart.Enabled = true;
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

        private void toolBtnSettings_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings.SettingsLocation))
            {
                LogManager.LogWarning($"An invalid Settings location has been specified {Settings.SettingsLocation}");
                return;
            }

            ProcessManager.StartProcess(ProcessName.FireAndForget, "explorer.exe", Settings.SettingsLocation);
        }
    }
}
