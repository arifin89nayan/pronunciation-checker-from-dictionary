namespace WindowsFormsApp1.UIDesign
{
    partial class TtsResultPreview
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTtsList = new System.Windows.Forms.Button();
            this.btnSsmlPreview = new System.Windows.Forms.Button();
            this.lblVoice = new System.Windows.Forms.Label();
            this.cmbVoice = new System.Windows.Forms.ComboBox();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblFixedCount = new System.Windows.Forms.Label();
            this.lblGeneralCount = new System.Windows.Forms.Label();
            this.lblFinalCount = new System.Windows.Forms.Label();
            this.dgvTtsList = new System.Windows.Forms.DataGridView();
            this.txtSsml = new System.Windows.Forms.RichTextBox();
            this.chkWordWrap = new System.Windows.Forms.CheckBox();
            this.btnCopySsml = new System.Windows.Forms.Button();
            this.pnlAudio = new System.Windows.Forms.Panel();
            this.btnGenerateAudio = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTtsList)).BeginInit();
            this.pnlAudio.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnTtsList);
            this.pnlHeader.Controls.Add(this.btnSsmlPreview);
            this.pnlHeader.Controls.Add(this.lblVoice);
            this.pnlHeader.Controls.Add(this.cmbVoice);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1423, 150);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(40, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "TTS Result Preview";
            // 
            // btnTtsList
            // 
            this.btnTtsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTtsList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsList.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.btnTtsList.ForeColor = System.Drawing.Color.Black;
            this.btnTtsList.Location = new System.Drawing.Point(44, 80);
            this.btnTtsList.Name = "btnTtsList";
            this.btnTtsList.Size = new System.Drawing.Size(380, 56);
            this.btnTtsList.TabIndex = 0;
            this.btnTtsList.Text = "Final TTS List";
            this.btnTtsList.UseVisualStyleBackColor = false;
            this.btnTtsList.Click += new System.EventHandler(this.btnTtsList_Click);
            // 
            // btnSsmlPreview
            // 
            this.btnSsmlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(76)))), ((int)(((byte)(92)))));
            this.btnSsmlPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSsmlPreview.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.btnSsmlPreview.ForeColor = System.Drawing.Color.White;
            this.btnSsmlPreview.Location = new System.Drawing.Point(440, 80);
            this.btnSsmlPreview.Name = "btnSsmlPreview";
            this.btnSsmlPreview.Size = new System.Drawing.Size(420, 56);
            this.btnSsmlPreview.TabIndex = 1;
            this.btnSsmlPreview.Text = "Azure SSML Preview";
            this.btnSsmlPreview.UseVisualStyleBackColor = false;
            this.btnSsmlPreview.Click += new System.EventHandler(this.btnSsmlPreview_Click);
            // 
            // lblVoice
            // 
            this.lblVoice.AutoSize = true;
            this.lblVoice.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(188)))));
            this.lblVoice.Location = new System.Drawing.Point(944, 28);
            this.lblVoice.Name = "lblVoice";
            this.lblVoice.Text = "Azure Voice";
            // 
            // cmbVoice
            // 
            this.cmbVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoice.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbVoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVoice.Location = new System.Drawing.Point(944, 84);
            this.cmbVoice.Name = "cmbVoice";
            this.cmbVoice.Size = new System.Drawing.Size(435, 33);
            this.cmbVoice.TabIndex = 2;
            this.cmbVoice.SelectedIndexChanged += new System.EventHandler(this.cmbVoice_SelectedIndexChanged);
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlSummary.Controls.Add(this.lblFixedCount);
            this.pnlSummary.Controls.Add(this.lblGeneralCount);
            this.pnlSummary.Controls.Add(this.lblFinalCount);
            this.pnlSummary.Location = new System.Drawing.Point(40, 168);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1343, 56);
            this.pnlSummary.TabIndex = 2;
            // 
            // lblFixedCount
            // 
            this.lblFixedCount.AutoSize = true;
            this.lblFixedCount.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblFixedCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblFixedCount.Location = new System.Drawing.Point(16, 12);
            this.lblFixedCount.Name = "lblFixedCount";
            this.lblFixedCount.Text = "Fixed: 0";
            // 
            // lblGeneralCount
            // 
            this.lblGeneralCount.AutoSize = true;
            this.lblGeneralCount.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblGeneralCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(120)))), ((int)(((byte)(0)))));
            this.lblGeneralCount.Location = new System.Drawing.Point(240, 12);
            this.lblGeneralCount.Name = "lblGeneralCount";
            this.lblGeneralCount.Text = "General: 0";
            // 
            // lblFinalCount
            // 
            this.lblFinalCount.AutoSize = true;
            this.lblFinalCount.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblFinalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.lblFinalCount.Location = new System.Drawing.Point(480, 12);
            this.lblFinalCount.Name = "lblFinalCount";
            this.lblFinalCount.Text = "Final TTS: 0";
            // 
            // dgvTtsList
            // 
            this.dgvTtsList.AllowUserToAddRows = false;
            this.dgvTtsList.AllowUserToDeleteRows = false;
            this.dgvTtsList.BackgroundColor = System.Drawing.Color.White;
            this.dgvTtsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvTtsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTtsList.Location = new System.Drawing.Point(40, 236);
            this.dgvTtsList.Name = "dgvTtsList";
            this.dgvTtsList.ReadOnly = true;
            this.dgvTtsList.RowHeadersVisible = false;
            this.dgvTtsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTtsList.Size = new System.Drawing.Size(1343, 470);
            this.dgvTtsList.TabIndex = 3;
            // 
            // txtSsml
            // 
            this.txtSsml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtSsml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSsml.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtSsml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(228)))));
            this.txtSsml.Location = new System.Drawing.Point(40, 236);
            this.txtSsml.Name = "txtSsml";
            this.txtSsml.ReadOnly = true;
            this.txtSsml.Size = new System.Drawing.Size(1343, 430);
            this.txtSsml.TabIndex = 4;
            this.txtSsml.Text = "";
            this.txtSsml.Visible = false;
            // 
            // chkWordWrap
            // 
            this.chkWordWrap.AutoSize = true;
            this.chkWordWrap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.chkWordWrap.Location = new System.Drawing.Point(44, 674);
            this.chkWordWrap.Name = "chkWordWrap";
            this.chkWordWrap.Size = new System.Drawing.Size(110, 27);
            this.chkWordWrap.TabIndex = 5;
            this.chkWordWrap.Text = "Word wrap";
            this.chkWordWrap.Visible = false;
            this.chkWordWrap.CheckedChanged += new System.EventHandler(this.chkWordWrap_CheckedChanged);
            // 
            // btnCopySsml
            // 
            this.btnCopySsml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopySsml.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCopySsml.Location = new System.Drawing.Point(1233, 670);
            this.btnCopySsml.Name = "btnCopySsml";
            this.btnCopySsml.Size = new System.Drawing.Size(150, 36);
            this.btnCopySsml.TabIndex = 6;
            this.btnCopySsml.Text = "Copy SSML";
            this.btnCopySsml.UseVisualStyleBackColor = true;
            this.btnCopySsml.Visible = false;
            this.btnCopySsml.Click += new System.EventHandler(this.btnCopySsml_Click);
            // 
            // pnlAudio
            // 
            this.pnlAudio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlAudio.Controls.Add(this.btnGenerateAudio);
            this.pnlAudio.Controls.Add(this.btnPlay);
            this.pnlAudio.Controls.Add(this.btnStop);
            this.pnlAudio.Controls.Add(this.btnOpenFolder);
            this.pnlAudio.Controls.Add(this.progress);
            this.pnlAudio.Controls.Add(this.lblStatus);
            this.pnlAudio.Location = new System.Drawing.Point(40, 730);
            this.pnlAudio.Name = "pnlAudio";
            this.pnlAudio.Size = new System.Drawing.Size(1343, 160);
            this.pnlAudio.TabIndex = 7;
            // 
            // btnGenerateAudio
            // 
            this.btnGenerateAudio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGenerateAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateAudio.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.btnGenerateAudio.ForeColor = System.Drawing.Color.Black;
            this.btnGenerateAudio.Location = new System.Drawing.Point(20, 20);
            this.btnGenerateAudio.Name = "btnGenerateAudio";
            this.btnGenerateAudio.Size = new System.Drawing.Size(300, 64);
            this.btnGenerateAudio.TabIndex = 0;
            this.btnGenerateAudio.Text = "Generate Audio";
            this.btnGenerateAudio.UseVisualStyleBackColor = false;
            this.btnGenerateAudio.Click += new System.EventHandler(this.btnGenerateAudio_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(340, 20);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(150, 64);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "▶ Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(500, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(150, 64);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "■ Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Enabled = false;
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.btnOpenFolder.Location = new System.Drawing.Point(670, 20);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(220, 64);
            this.btnOpenFolder.TabIndex = 3;
            this.btnOpenFolder.Text = "Open in Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(20, 100);
            this.progress.MarqueeAnimationSpeed = 30;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(870, 24);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress.TabIndex = 4;
            this.progress.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(86)))), ((int)(((byte)(96)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 130);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "No audio generated yet.";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1190, 920);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(193, 70);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Back";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TtsResultPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1423, 1013);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.dgvTtsList);
            this.Controls.Add(this.txtSsml);
            this.Controls.Add(this.chkWordWrap);
            this.Controls.Add(this.btnCopySsml);
            this.Controls.Add(this.pnlAudio);
            this.Controls.Add(this.btnClose);
            this.Name = "TtsResultPreview";
            this.Text = "TTS Result Preview";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTtsList)).EndInit();
            this.pnlAudio.ResumeLayout(false);
            this.pnlAudio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTtsList;
        private System.Windows.Forms.Button btnSsmlPreview;
        private System.Windows.Forms.Label lblVoice;
        private System.Windows.Forms.ComboBox cmbVoice;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblFixedCount;
        private System.Windows.Forms.Label lblGeneralCount;
        private System.Windows.Forms.Label lblFinalCount;
        private System.Windows.Forms.DataGridView dgvTtsList;
        private System.Windows.Forms.RichTextBox txtSsml;
        private System.Windows.Forms.CheckBox chkWordWrap;
        private System.Windows.Forms.Button btnCopySsml;
        private System.Windows.Forms.Panel pnlAudio;
        private System.Windows.Forms.Button btnGenerateAudio;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Button btnClose;
    }
}