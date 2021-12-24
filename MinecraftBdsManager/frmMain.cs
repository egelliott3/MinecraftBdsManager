using MinecraftBdsManager.Configuration;
using MinecraftBdsManager.Logging;

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

            //  Need another one for log files, if opted in with valid path
            if (Settings.CurrentSettings.LoggingSettings.EnableLoggingToFile)
            {
                LogManager.RegisterFileLogger(Settings.CurrentSettings.LoggingSettings.FileLoggingDirectoryPath);
            }

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

        private void toolBtnBackupNow_Click(object sender, EventArgs e)
        {

        }

        private void toolBtnOpenLogsFolder_Click(object sender, EventArgs e)
        {
            ProcessManager.StartProcess(ProcessName.FireAndForget, "explorer.exe", Settings.CurrentSettings.LoggingSettings.FileLoggingDirectoryPath);
        }

        private void toolBtnOpenSavesFolder_Click(object sender, EventArgs e)
        {

        }

        private void toolBtnShowMap_Click(object sender, EventArgs e)
        {

        }

        private async void toolBtnStart_Click(object sender, EventArgs e)
        {
            toolBtnStart.Enabled = false;

            if (_clearStatusBoxOnStart)
            {
                rtbStatus.Clear();
            }
            var success = await BdsManager.StartAsync();

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
            ProcessManager.StartProcess(ProcessName.FireAndForget, "explorer.exe", LogManager.CurrentLogFilePath);
        }

    }
}
