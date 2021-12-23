namespace MinecraftBdsManager
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBtnStart = new System.Windows.Forms.ToolStripButton();
            this.toolBtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolBtnViewLog = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpenLogsFolder = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpenSavesFolder = new System.Windows.Forms.ToolStripButton();
            this.toolBtnShowMap = new System.Windows.Forms.ToolStripButton();
            this.toolBtnBackupNow = new System.Windows.Forms.ToolStripButton();
            this.txtCustomCommand = new System.Windows.Forms.TextBox();
            this.btnIssueCommand = new System.Windows.Forms.Button();
            this.lblStatusBox = new System.Windows.Forms.Label();
            this.lblCustomCommand = new System.Windows.Forms.Label();
            this.toolBtnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.rtbStatus = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnStart,
            this.toolBtnStop,
            this.toolBtnViewLog,
            this.toolBtnOpenLogsFolder,
            this.toolBtnOpenSavesFolder,
            this.toolBtnShowMap,
            this.toolBtnBackupNow});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1373, 33);
            this.toolStrip1.TabIndex = 6;
            // 
            // toolBtnStart
            // 
            this.toolBtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnStart.Image = global::MinecraftBdsManager.Properties.Resources.media_play_green;
            this.toolBtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnStart.Name = "toolBtnStart";
            this.toolBtnStart.Size = new System.Drawing.Size(34, 28);
            this.toolBtnStart.Text = "toolStripButton1";
            this.toolBtnStart.ToolTipText = "Start Bedrock Dedicated Server";
            this.toolBtnStart.Click += new System.EventHandler(this.toolBtnStart_Click);
            // 
            // toolBtnStop
            // 
            this.toolBtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnStop.Enabled = false;
            this.toolBtnStop.Image = global::MinecraftBdsManager.Properties.Resources.media_stop_red;
            this.toolBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnStop.Name = "toolBtnStop";
            this.toolBtnStop.Size = new System.Drawing.Size(34, 28);
            this.toolBtnStop.Text = "toolStripButton1";
            this.toolBtnStop.ToolTipText = "Stop Bedrock Dedicated Server";
            this.toolBtnStop.Click += new System.EventHandler(this.toolBtnStop_Click);
            // 
            // toolBtnViewLog
            // 
            this.toolBtnViewLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnViewLog.Image = global::MinecraftBdsManager.Properties.Resources.notebook;
            this.toolBtnViewLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnViewLog.Name = "toolBtnViewLog";
            this.toolBtnViewLog.Size = new System.Drawing.Size(34, 28);
            this.toolBtnViewLog.Text = "toolStripButton2";
            this.toolBtnViewLog.ToolTipText = "Show current log file";
            this.toolBtnViewLog.Click += new System.EventHandler(this.toolBtnViewLog_Click);
            // 
            // toolBtnOpenLogsFolder
            // 
            this.toolBtnOpenLogsFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnOpenLogsFolder.Image = global::MinecraftBdsManager.Properties.Resources.folder_document;
            this.toolBtnOpenLogsFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpenLogsFolder.Name = "toolBtnOpenLogsFolder";
            this.toolBtnOpenLogsFolder.Size = new System.Drawing.Size(34, 28);
            this.toolBtnOpenLogsFolder.Text = "toolStripButton3";
            this.toolBtnOpenLogsFolder.ToolTipText = "Open Logs folder";
            this.toolBtnOpenLogsFolder.Click += new System.EventHandler(this.toolBtnOpenLogsFolder_Click);
            // 
            // toolBtnOpenSavesFolder
            // 
            this.toolBtnOpenSavesFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnOpenSavesFolder.Image = global::MinecraftBdsManager.Properties.Resources.folder_cubes;
            this.toolBtnOpenSavesFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpenSavesFolder.Name = "toolBtnOpenSavesFolder";
            this.toolBtnOpenSavesFolder.Size = new System.Drawing.Size(34, 28);
            this.toolBtnOpenSavesFolder.Text = "toolStripButton4";
            this.toolBtnOpenSavesFolder.ToolTipText = "Open backups folder";
            this.toolBtnOpenSavesFolder.Click += new System.EventHandler(this.toolBtnOpenSavesFolder_Click);
            // 
            // toolBtnShowMap
            // 
            this.toolBtnShowMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnShowMap.Image = global::MinecraftBdsManager.Properties.Resources.earth_view;
            this.toolBtnShowMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnShowMap.Name = "toolBtnShowMap";
            this.toolBtnShowMap.Size = new System.Drawing.Size(34, 28);
            this.toolBtnShowMap.Text = "toolStripButton5";
            this.toolBtnShowMap.ToolTipText = "Show world map";
            this.toolBtnShowMap.Click += new System.EventHandler(this.toolBtnShowMap_Click);
            // 
            // toolBtnBackupNow
            // 
            this.toolBtnBackupNow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnBackupNow.Image = global::MinecraftBdsManager.Properties.Resources.data_disk;
            this.toolBtnBackupNow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBackupNow.Name = "toolBtnBackupNow";
            this.toolBtnBackupNow.Size = new System.Drawing.Size(34, 28);
            this.toolBtnBackupNow.Text = "toolStripButton1";
            this.toolBtnBackupNow.ToolTipText = "Create a backup now";
            this.toolBtnBackupNow.Click += new System.EventHandler(this.toolBtnBackupNow_Click);
            // 
            // txtCustomCommand
            // 
            this.txtCustomCommand.Location = new System.Drawing.Point(12, 690);
            this.txtCustomCommand.Name = "txtCustomCommand";
            this.txtCustomCommand.Size = new System.Drawing.Size(1168, 31);
            this.txtCustomCommand.TabIndex = 2;
            // 
            // btnIssueCommand
            // 
            this.btnIssueCommand.Location = new System.Drawing.Point(1202, 688);
            this.btnIssueCommand.Name = "btnIssueCommand";
            this.btnIssueCommand.Size = new System.Drawing.Size(159, 34);
            this.btnIssueCommand.TabIndex = 3;
            this.btnIssueCommand.Text = "Send Command";
            this.btnIssueCommand.UseVisualStyleBackColor = true;
            this.btnIssueCommand.Click += new System.EventHandler(this.btnIssueCommand_Click);
            // 
            // lblStatusBox
            // 
            this.lblStatusBox.AutoSize = true;
            this.lblStatusBox.Location = new System.Drawing.Point(12, 43);
            this.lblStatusBox.Name = "lblStatusBox";
            this.lblStatusBox.Size = new System.Drawing.Size(187, 25);
            this.lblStatusBox.TabIndex = 4;
            this.lblStatusBox.Text = "Server status and logs";
            // 
            // lblCustomCommand
            // 
            this.lblCustomCommand.AutoSize = true;
            this.lblCustomCommand.Location = new System.Drawing.Point(12, 662);
            this.lblCustomCommand.Name = "lblCustomCommand";
            this.lblCustomCommand.Size = new System.Drawing.Size(212, 25);
            this.lblCustomCommand.TabIndex = 5;
            this.lblCustomCommand.Text = "Issue command to server";
            // 
            // toolBtnSettings
            // 
            this.toolBtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSettings.Image = global::MinecraftBdsManager.Properties.Resources.gears;
            this.toolBtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSettings.Name = "toolBtnSettings";
            this.toolBtnSettings.Size = new System.Drawing.Size(34, 28);
            this.toolBtnSettings.Text = "toolStripButton1";
            this.toolBtnSettings.ToolTipText = "Minecraft BDS Manager Settings";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(34, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // rtbStatus
            // 
            this.rtbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rtbStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbStatus.Location = new System.Drawing.Point(12, 71);
            this.rtbStatus.Name = "rtbStatus";
            this.rtbStatus.ReadOnly = true;
            this.rtbStatus.Size = new System.Drawing.Size(1331, 569);
            this.rtbStatus.TabIndex = 7;
            this.rtbStatus.Text = "";
            this.rtbStatus.TextChanged += new System.EventHandler(this.rtbStatus_TextChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1373, 753);
            this.Controls.Add(this.rtbStatus);
            this.Controls.Add(this.lblCustomCommand);
            this.Controls.Add(this.lblStatusBox);
            this.Controls.Add(this.btnIssueCommand);
            this.Controls.Add(this.txtCustomCommand);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minecraft BDS Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolBtnStart;
        private ToolStripButton toolBtnStop;
        private ToolStripButton toolBtnViewLog;
        private ToolStripButton toolBtnOpenLogsFolder;
        private ToolStripButton toolBtnOpenSavesFolder;
        private ToolStripButton toolBtnShowMap;
        private ToolStripButton toolBtnBackupNow;
        private TextBox txtCustomCommand;
        private Button btnIssueCommand;
        private Label lblStatusBox;
        private Label lblCustomCommand;
        private ToolStripButton toolBtnSettings;
        private ToolStripButton toolStripButton1;
        private RichTextBox rtbStatus;
    }
}