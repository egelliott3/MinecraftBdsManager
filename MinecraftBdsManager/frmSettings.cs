using MinecraftBdsManager.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftBdsManager
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private async void btnPickUpgradeFile_Click(object sender, EventArgs e)
        {
            if (dlgOpenBdsZip.ShowDialog(this) == DialogResult.OK)
            {
                await UpgradeServerAsync(dlgOpenBdsZip.FileName);
            }
        }

        private async Task UpgradeServerAsync(string zipFileName)
        {
            LogManager.LogInformation($"Performing BDS upgrade using the file {zipFileName}.");

            string existingBdsFolder = Configuration.Settings.CurrentSettings.BedrockDedicateServerDirectoryPath;
            if (!Directory.Exists(existingBdsFolder))
            {
                LogManager.LogError($"Current BDS folder {existingBdsFolder} does not exist.  Unable to upgrade server since it does not exist.");
                return;
            }

            // Format the log filename to be "<worldName>_2009-06-15T134530Z" using UTC time and...
            var formattedCurrentUtcDateTime = $"{DateTime.UtcNow:O}";

            // ... taking out the colons (:) in order to not make Windows file system upset and...
            formattedCurrentUtcDateTime = formattedCurrentUtcDateTime.Replace(":", string.Empty);

            // ... remove the milliseconds/fractional seconds as they are not super useful and the plop the Z back on the end to keep the UTC signifier and...
            formattedCurrentUtcDateTime = string.Concat(formattedCurrentUtcDateTime.Substring(0, formattedCurrentUtcDateTime.IndexOf(".")), "Z");


            string bdsBackupFilePath = Path.GetFullPath(Path.Combine(existingBdsFolder, $"../BDS_Backup_{formattedCurrentUtcDateTime}.zip"));

            ZipFile.CreateFromDirectory(existingBdsFolder, bdsBackupFilePath);


        }

        private void rtbStatus_TextChanged(object sender, EventArgs e)
        {
            rtbStatus.SelectionStart = rtbStatus.Text.Length;
            rtbStatus.ScrollToCaret();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            // Register listener for traces from this dialog
            LogManager.RegisterUILogger(rtbStatus, "UpgradeStatusBoxLogger");
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogManager.UnregisterUILogger(rtbStatus, "UpgradeStatusBoxLogger");
        }
    }
}
