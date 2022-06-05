namespace MinecraftBdsManager
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.dlgOpenBdsZip = new System.Windows.Forms.OpenFileDialog();
            this.btnPickUpgradeFile = new System.Windows.Forms.Button();
            this.rtbStatus = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // dlgOpenBdsZip
            // 
            this.dlgOpenBdsZip.FileName = "dlgOpenBdsZip";
            this.dlgOpenBdsZip.Filter = "Zip files (*.zip)|*.zip";
            // 
            // btnPickUpgradeFile
            // 
            this.btnPickUpgradeFile.Location = new System.Drawing.Point(12, 12);
            this.btnPickUpgradeFile.Name = "btnPickUpgradeFile";
            this.btnPickUpgradeFile.Size = new System.Drawing.Size(184, 34);
            this.btnPickUpgradeFile.TabIndex = 0;
            this.btnPickUpgradeFile.Text = "Pick Upgrade File";
            this.btnPickUpgradeFile.UseVisualStyleBackColor = true;
            this.btnPickUpgradeFile.Click += new System.EventHandler(this.btnPickUpgradeFile_Click);
            // 
            // rtbStatus
            // 
            this.rtbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rtbStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbStatus.Location = new System.Drawing.Point(12, 52);
            this.rtbStatus.Name = "rtbStatus";
            this.rtbStatus.ReadOnly = true;
            this.rtbStatus.Size = new System.Drawing.Size(1025, 579);
            this.rtbStatus.TabIndex = 1;
            this.rtbStatus.Text = "";
            this.rtbStatus.TextChanged += new System.EventHandler(this.rtbStatus_TextChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 643);
            this.Controls.Add(this.rtbStatus);
            this.Controls.Add(this.btnPickUpgradeFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.Text = "Minecraft BDS Manager Server Upgrade Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenFileDialog dlgOpenBdsZip;
        private Button btnPickUpgradeFile;
        private RichTextBox rtbStatus;
    }
}