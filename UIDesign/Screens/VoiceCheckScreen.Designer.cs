namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class VoiceCheckScreen
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

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblChecksHdr = new System.Windows.Forms.Label();
            this.dgvChecks = new System.Windows.Forms.DataGridView();
            this.lblOrigHdr = new System.Windows.Forms.Label();
            this.lblRecogHdr = new System.Windows.Forms.Label();
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.txtRecognized = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChecks)).BeginInit();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 8 — Voice Confirmation";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "Verify the generated audio actually says the right words.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblFile
            this.lblFile.Name = "lblFile";
            this.lblFile.Location = new System.Drawing.Point(20, 80);
            this.lblFile.Text = "Audio File:";
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblFile.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblFile.AutoSize = true;
            // btnPlay
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Location = new System.Drawing.Point(20, 108);
            this.btnPlay.Size = new System.Drawing.Size(80, 30);
            this.btnPlay.Text = "Play";
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // btnStop
            this.btnStop.Name = "btnStop";
            this.btnStop.Location = new System.Drawing.Point(110, 108);
            this.btnStop.Size = new System.Drawing.Size(80, 30);
            this.btnStop.Text = "Stop";
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnStop.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // btnRun
            this.btnRun.Name = "btnRun";
            this.btnRun.Location = new System.Drawing.Point(200, 108);
            this.btnRun.Size = new System.Drawing.Size(160, 30);
            this.btnRun.Text = "Run Quality Check";
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // lblChecksHdr
            this.lblChecksHdr.Name = "lblChecksHdr";
            this.lblChecksHdr.Location = new System.Drawing.Point(20, 150);
            this.lblChecksHdr.Text = "Quality Check";
            this.lblChecksHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChecksHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblChecksHdr.AutoSize = true;
            // dgvChecks
            this.dgvChecks.Name = "dgvChecks";
            this.dgvChecks.Location = new System.Drawing.Point(20, 172);
            this.dgvChecks.Size = new System.Drawing.Size(820, 150);
            this.dgvChecks.ReadOnly = true;
            this.dgvChecks.AllowUserToAddRows = false;
            this.dgvChecks.RowHeadersVisible = false;
            this.dgvChecks.BackgroundColor = System.Drawing.Color.White;
            this.dgvChecks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChecks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChecks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChecks.Font = new System.Drawing.Font("Segoe UI", 9F);
            // lblOrigHdr
            this.lblOrigHdr.Name = "lblOrigHdr";
            this.lblOrigHdr.Location = new System.Drawing.Point(20, 332);
            this.lblOrigHdr.Text = "Original";
            this.lblOrigHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblOrigHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblOrigHdr.AutoSize = true;
            // lblRecogHdr
            this.lblRecogHdr.Name = "lblRecogHdr";
            this.lblRecogHdr.Location = new System.Drawing.Point(430, 332);
            this.lblRecogHdr.Text = "Recognized";
            this.lblRecogHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRecogHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblRecogHdr.AutoSize = true;
            // txtOriginal
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.Location = new System.Drawing.Point(20, 354);
            this.txtOriginal.Size = new System.Drawing.Size(400, 150);
            this.txtOriginal.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.txtOriginal.Multiline = true;
            this.txtOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOriginal.ReadOnly = true;
            // txtRecognized
            this.txtRecognized.Name = "txtRecognized";
            this.txtRecognized.Location = new System.Drawing.Point(430, 354);
            this.txtRecognized.Size = new System.Drawing.Size(410, 150);
            this.txtRecognized.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.txtRecognized.Multiline = true;
            this.txtRecognized.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecognized.ReadOnly = true;
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 514);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // VoiceCheckScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lblChecksHdr);
            this.Controls.Add(this.dgvChecks);
            this.Controls.Add(this.lblOrigHdr);
            this.Controls.Add(this.lblRecogHdr);
            this.Controls.Add(this.txtOriginal);
            this.Controls.Add(this.txtRecognized);
            this.Controls.Add(this.lblStatus);
            this.Name = "VoiceCheckScreen";
            this.Size = new System.Drawing.Size(880, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChecks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblChecksHdr;
        private System.Windows.Forms.DataGridView dgvChecks;
        private System.Windows.Forms.Label lblOrigHdr;
        private System.Windows.Forms.Label lblRecogHdr;
        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.TextBox txtRecognized;
        private System.Windows.Forms.Label lblStatus;
    }
}