namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class TtsListScreen
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
            this.lblStats = new System.Windows.Forms.Label();
            this.btnGenGeneral = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnGenTts = new System.Windows.Forms.Button();
            this.lblFinalHdr = new System.Windows.Forms.Label();
            this.dgvFinal = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinal)).BeginInit();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 6 — TTS List Generation";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "Merge the approved Fixed List with general script terms.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblStats
            this.lblStats.Name = "lblStats";
            this.lblStats.Location = new System.Drawing.Point(20, 80);
            this.lblStats.Text = "";
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblStats.AutoSize = true;
            // btnGenGeneral
            this.btnGenGeneral.Name = "btnGenGeneral";
            this.btnGenGeneral.Location = new System.Drawing.Point(20, 130);
            this.btnGenGeneral.Size = new System.Drawing.Size(170, 32);
            this.btnGenGeneral.Text = "Generate General List";
            this.btnGenGeneral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenGeneral.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnGenGeneral.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnGenGeneral.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnGenGeneral.UseVisualStyleBackColor = false;
            this.btnGenGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenGeneral.Click += new System.EventHandler(this.btnGenGeneral_Click);
            // btnMerge
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Location = new System.Drawing.Point(200, 130);
            this.btnMerge.Size = new System.Drawing.Size(120, 32);
            this.btnMerge.Text = "Merge Lists";
            this.btnMerge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMerge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMerge.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnMerge.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnMerge.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnMerge.UseVisualStyleBackColor = false;
            this.btnMerge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // btnGenTts
            this.btnGenTts.Name = "btnGenTts";
            this.btnGenTts.Location = new System.Drawing.Point(330, 130);
            this.btnGenTts.Size = new System.Drawing.Size(160, 32);
            this.btnGenTts.Text = "Generate TTS List";
            this.btnGenTts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenTts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenTts.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnGenTts.ForeColor = System.Drawing.Color.White;
            this.btnGenTts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnGenTts.UseVisualStyleBackColor = false;
            this.btnGenTts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenTts.Click += new System.EventHandler(this.btnGenTts_Click);
            // lblFinalHdr
            this.lblFinalHdr.Name = "lblFinalHdr";
            this.lblFinalHdr.Location = new System.Drawing.Point(20, 174);
            this.lblFinalHdr.Text = "Final TTS List";
            this.lblFinalHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFinalHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblFinalHdr.AutoSize = true;
            // dgvFinal
            this.dgvFinal.Name = "dgvFinal";
            this.dgvFinal.Location = new System.Drawing.Point(20, 196);
            this.dgvFinal.Size = new System.Drawing.Size(820, 320);
            this.dgvFinal.ReadOnly = true;
            this.dgvFinal.AllowUserToAddRows = false;
            this.dgvFinal.RowHeadersVisible = false;
            this.dgvFinal.BackgroundColor = System.Drawing.Color.White;
            this.dgvFinal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFinal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFinal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFinal.Font = new System.Drawing.Font("Segoe UI", 9F);
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 524);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // TtsListScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.btnGenGeneral);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnGenTts);
            this.Controls.Add(this.lblFinalHdr);
            this.Controls.Add(this.dgvFinal);
            this.Controls.Add(this.lblStatus);
            this.Name = "TtsListScreen";
            this.Size = new System.Drawing.Size(880, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button btnGenGeneral;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Button btnGenTts;
        private System.Windows.Forms.Label lblFinalHdr;
        private System.Windows.Forms.DataGridView dgvFinal;
        private System.Windows.Forms.Label lblStatus;
    }
}