using MinecraftBdsManager.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftBdsManager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void btnIssueCommand_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnIssueCommand.Text))
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
            // TODO : Setup trace listener(s)
            //  Need one for the status textbox
            LogManager.RegisterUILogger(rtbStatus);
            //  Need another one for log files
            //  Any more needed?
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

            rtbStatus.Clear();
            await BdsManager.StartAsync();

            toolBtnStop.Enabled = true;
        }

        private async void toolBtnStop_Click(object sender, EventArgs e)
        {
            toolBtnStop.Enabled = false;

            await BdsManager.StopAsync();

            toolBtnStart.Enabled = true;
        }

        private void toolBtnViewLog_Click(object sender, EventArgs e)
        {

        }

    }
}
