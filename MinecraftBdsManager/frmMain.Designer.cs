﻿namespace MinecraftBdsManager
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
            this.toolBarMain = new System.Windows.Forms.ToolStrip();
            this.toolBtnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolBtnStart = new System.Windows.Forms.ToolStripButton();
            this.toolBtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolBtnViewLog = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpenLogsFolder = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpenSavesFolder = new System.Windows.Forms.ToolStripButton();
            this.toolBtnBackupNow = new System.Windows.Forms.ToolStripButton();
            this.toolBtnMapNow = new System.Windows.Forms.ToolStripButton();
            this.txtCustomCommand = new System.Windows.Forms.TextBox();
            this.btnIssueCommand = new System.Windows.Forms.Button();
            this.lblStatusBox = new System.Windows.Forms.Label();
            this.lblCustomCommand = new System.Windows.Forms.Label();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.rtbStatus = new System.Windows.Forms.RichTextBox();
            this.toolBarMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBarMain
            // 
            this.toolBarMain.AutoSize = false;
            this.toolBarMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBarMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolBarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnSettings,
            this.toolBtnStart,
            this.toolBtnStop,
            this.toolBtnViewLog,
            this.toolBtnOpenLogsFolder,
            this.toolBtnOpenSavesFolder,
            this.toolBtnBackupNow,
            this.toolBtnMapNow});
            this.toolBarMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolBarMain.Location = new System.Drawing.Point(0, 0);
            this.toolBarMain.Name = "toolBarMain";
            this.toolBarMain.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.toolBarMain.Size = new System.Drawing.Size(690, 35);
            this.toolBarMain.TabIndex = 6;
            // 
            // toolBtnSettings
            // 
            this.toolBtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSettings.Image = global::MinecraftBdsManager.Properties.Resources.gears;
            this.toolBtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSettings.Name = "toolBtnSettings";
            this.toolBtnSettings.Size = new System.Drawing.Size(28, 32);
            this.toolBtnSettings.Text = "toolStripButton1";
            this.toolBtnSettings.ToolTipText = "Minecraft BDS Manager Settings";
            this.toolBtnSettings.Click += new System.EventHandler(this.toolBtnSettings_Click);
            // 
            // toolBtnStart
            // 
            this.toolBtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnStart.Image = global::MinecraftBdsManager.Properties.Resources.media_play_green;
            this.toolBtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnStart.Name = "toolBtnStart";
            this.toolBtnStart.Size = new System.Drawing.Size(28, 32);
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
            this.toolBtnStop.Size = new System.Drawing.Size(28, 32);
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
            this.toolBtnViewLog.Size = new System.Drawing.Size(28, 32);
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
            this.toolBtnOpenLogsFolder.Size = new System.Drawing.Size(28, 32);
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
            this.toolBtnOpenSavesFolder.Size = new System.Drawing.Size(28, 32);
            this.toolBtnOpenSavesFolder.Text = "toolStripButton4";
            this.toolBtnOpenSavesFolder.ToolTipText = "Open backups folder";
            this.toolBtnOpenSavesFolder.Click += new System.EventHandler(this.toolBtnOpenSavesFolder_Click);
            // 
            // toolBtnBackupNow
            // 
            this.toolBtnBackupNow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnBackupNow.Image = global::MinecraftBdsManager.Properties.Resources.data_disk;
            this.toolBtnBackupNow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBackupNow.Name = "toolBtnBackupNow";
            this.toolBtnBackupNow.Size = new System.Drawing.Size(28, 32);
            this.toolBtnBackupNow.Text = "toolStripButton1";
            this.toolBtnBackupNow.ToolTipText = "Create a backup now";
            this.toolBtnBackupNow.Click += new System.EventHandler(this.toolBtnBackupNow_Click);
            // 
            // toolBtnMapNow
            // 
            this.toolBtnMapNow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnMapNow.Image = global::MinecraftBdsManager.Properties.Resources.photo_scenery;
            this.toolBtnMapNow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnMapNow.Name = "toolBtnMapNow";
            this.toolBtnMapNow.Size = new System.Drawing.Size(28, 32);
            this.toolBtnMapNow.Text = "toolStripButton2";
            this.toolBtnMapNow.ToolTipText = "Create a new map";
            this.toolBtnMapNow.Click += new System.EventHandler(this.toolBtnMapNow_Click);
            // 
            // txtCustomCommand
            // 
            this.txtCustomCommand.AcceptsReturn = true;
            this.txtCustomCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomCommand.Location = new System.Drawing.Point(123, 61);
            this.txtCustomCommand.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomCommand.Name = "txtCustomCommand";
            this.txtCustomCommand.Size = new System.Drawing.Size(556, 23);
            this.txtCustomCommand.TabIndex = 2;
            // 
            // btnIssueCommand
            // 
            this.btnIssueCommand.Enabled = false;
            this.btnIssueCommand.Location = new System.Drawing.Point(9, 61);
            this.btnIssueCommand.Margin = new System.Windows.Forms.Padding(2);
            this.btnIssueCommand.Name = "btnIssueCommand";
            this.btnIssueCommand.Size = new System.Drawing.Size(111, 23);
            this.btnIssueCommand.TabIndex = 3;
            this.btnIssueCommand.Text = "&Send Command:";
            this.btnIssueCommand.UseVisualStyleBackColor = true;
            this.btnIssueCommand.Click += new System.EventHandler(this.btnIssueCommand_Click);
            // 
            // lblStatusBox
            // 
            this.lblStatusBox.AutoSize = true;
            this.lblStatusBox.Location = new System.Drawing.Point(9, 104);
            this.lblStatusBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatusBox.Name = "lblStatusBox";
            this.lblStatusBox.Size = new System.Drawing.Size(121, 15);
            this.lblStatusBox.TabIndex = 4;
            this.lblStatusBox.Text = "Server status and logs";
            // 
            // lblCustomCommand
            // 
            this.lblCustomCommand.AutoSize = true;
            this.lblCustomCommand.Location = new System.Drawing.Point(9, 44);
            this.lblCustomCommand.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomCommand.Name = "lblCustomCommand";
            this.lblCustomCommand.Size = new System.Drawing.Size(139, 15);
            this.lblCustomCommand.TabIndex = 5;
            this.lblCustomCommand.Text = "Issue command to server";
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
            this.rtbStatus.Location = new System.Drawing.Point(9, 121);
            this.rtbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.rtbStatus.Name = "rtbStatus";
            this.rtbStatus.ReadOnly = true;
            this.rtbStatus.Size = new System.Drawing.Size(670, 335);
            this.rtbStatus.TabIndex = 7;
            this.rtbStatus.Text = "";
            this.rtbStatus.TextChanged += new System.EventHandler(this.rtbStatus_TextChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 467);
            this.Controls.Add(this.rtbStatus);
            this.Controls.Add(this.lblCustomCommand);
            this.Controls.Add(this.lblStatusBox);
            this.Controls.Add(this.btnIssueCommand);
            this.Controls.Add(this.txtCustomCommand);
            this.Controls.Add(this.toolBarMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minecraft BDS Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolBarMain.ResumeLayout(false);
            this.toolBarMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolBarMain;
        private ToolStripButton toolBtnStart;
        private ToolStripButton toolBtnStop;
        private ToolStripButton toolBtnViewLog;
        private ToolStripButton toolBtnOpenLogsFolder;
        private ToolStripButton toolBtnOpenSavesFolder;
        private ToolStripButton toolBtnBackupNow;
        private TextBox txtCustomCommand;
        private Button btnIssueCommand;
        private Label lblStatusBox;
        private Label lblCustomCommand;
        private ToolStripButton toolBtnSettings;
        private ToolStripButton toolStripButton1;
        private RichTextBox rtbStatus;
        private ToolStripButton toolBtnMapNow;
    }
}