namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class AzureTtsScreen
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
            this.lblVoice = new System.Windows.Forms.Label();
            this.cmbVoice = new System.Windows.Forms.ComboBox();
            this.lblStyle = new System.Windows.Forms.Label();
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.numRate = new System.Windows.Forms.NumericUpDown();
            this.lblPitch = new System.Windows.Forms.Label();
            this.numPitch = new System.Windows.Forms.NumericUpDown();
            this.btnGenSsml = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblSsmlHdr = new System.Windows.Forms.Label();
            this.txtSsml = new System.Windows.Forms.TextBox();
            this.lblOutName = new System.Windows.Forms.Label();
            this.txtOutName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 7 — Azure TTS Script Generator";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "Wrap confirmed readings into SSML, validate, then synthesize.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblVoice
            this.lblVoice.Name = "lblVoice";
            this.lblVoice.Location = new System.Drawing.Point(20, 84);
            this.lblVoice.Text = "Voice:";
            this.lblVoice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblVoice.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblVoice.AutoSize = true;
            // cmbVoice
            this.cmbVoice.Name = "cmbVoice";
            this.cmbVoice.Location = new System.Drawing.Point(90, 81);
            this.cmbVoice.Size = new System.Drawing.Size(220, 25);
            this.cmbVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // lblStyle
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Location = new System.Drawing.Point(20, 116);
            this.lblStyle.Text = "Style:";
            this.lblStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblStyle.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblStyle.AutoSize = true;
            // cmbStyle
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Location = new System.Drawing.Point(90, 113);
            this.cmbStyle.Size = new System.Drawing.Size(150, 25);
            this.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // lblSpeed
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Location = new System.Drawing.Point(260, 116);
            this.lblSpeed.Text = "Speed %:";
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblSpeed.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblSpeed.AutoSize = true;
            // numRate
            this.numRate.Name = "numRate";
            this.numRate.Location = new System.Drawing.Point(330, 113);
            this.numRate.Size = new System.Drawing.Size(70, 23);
            this.numRate.Minimum = new decimal(new int[] { 50, 0, 0, -2147483648 });
            this.numRate.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            this.numRate.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            // lblPitch
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Location = new System.Drawing.Point(420, 116);
            this.lblPitch.Text = "Pitch %:";
            this.lblPitch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblPitch.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblPitch.AutoSize = true;
            // numPitch
            this.numPitch.Name = "numPitch";
            this.numPitch.Location = new System.Drawing.Point(490, 113);
            this.numPitch.Size = new System.Drawing.Size(70, 23);
            this.numPitch.Minimum = new decimal(new int[] { 50, 0, 0, -2147483648 });
            this.numPitch.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            this.numPitch.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            // btnGenSsml
            this.btnGenSsml.Name = "btnGenSsml";
            this.btnGenSsml.Location = new System.Drawing.Point(20, 150);
            this.btnGenSsml.Size = new System.Drawing.Size(140, 32);
            this.btnGenSsml.Text = "Generate SSML";
            this.btnGenSsml.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenSsml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenSsml.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnGenSsml.ForeColor = System.Drawing.Color.White;
            this.btnGenSsml.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnGenSsml.UseVisualStyleBackColor = false;
            this.btnGenSsml.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenSsml.Click += new System.EventHandler(this.btnGenSsml_Click);
            // btnValidate
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Location = new System.Drawing.Point(170, 150);
            this.btnValidate.Size = new System.Drawing.Size(130, 32);
            this.btnValidate.Text = "Validate SSML";
            this.btnValidate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnValidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidate.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnValidate.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnValidate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnValidate.UseVisualStyleBackColor = false;
            this.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // btnSend
            this.btnSend.Name = "btnSend";
            this.btnSend.Location = new System.Drawing.Point(310, 150);
            this.btnSend.Size = new System.Drawing.Size(160, 32);
            this.btnSend.Text = "Send to Azure TTS";
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // lblSsmlHdr
            this.lblSsmlHdr.Name = "lblSsmlHdr";
            this.lblSsmlHdr.Location = new System.Drawing.Point(20, 192);
            this.lblSsmlHdr.Text = "SSML Preview";
            this.lblSsmlHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSsmlHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblSsmlHdr.AutoSize = true;
            // txtSsml
            this.txtSsml.Name = "txtSsml";
            this.txtSsml.Location = new System.Drawing.Point(20, 214);
            this.txtSsml.Size = new System.Drawing.Size(820, 280);
            this.txtSsml.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtSsml.Multiline = true;
            this.txtSsml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // lblOutName
            this.lblOutName.Name = "lblOutName";
            this.lblOutName.Location = new System.Drawing.Point(20, 504);
            this.lblOutName.Text = "Output File Name:";
            this.lblOutName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblOutName.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblOutName.AutoSize = true;
            // txtOutName
            this.txtOutName.Name = "txtOutName";
            this.txtOutName.Location = new System.Drawing.Point(160, 501);
            this.txtOutName.Size = new System.Drawing.Size(320, 23);
            this.txtOutName.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 536);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // AzureTtsScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblVoice);
            this.Controls.Add(this.cmbVoice);
            this.Controls.Add(this.lblStyle);
            this.Controls.Add(this.cmbStyle);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.numRate);
            this.Controls.Add(this.lblPitch);
            this.Controls.Add(this.numPitch);
            this.Controls.Add(this.btnGenSsml);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblSsmlHdr);
            this.Controls.Add(this.txtSsml);
            this.Controls.Add(this.lblOutName);
            this.Controls.Add(this.txtOutName);
            this.Controls.Add(this.lblStatus);
            this.Name = "AzureTtsScreen";
            this.Size = new System.Drawing.Size(880, 600);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblVoice;
        private System.Windows.Forms.ComboBox cmbVoice;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.ComboBox cmbStyle;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.NumericUpDown numRate;
        private System.Windows.Forms.Label lblPitch;
        private System.Windows.Forms.NumericUpDown numPitch;
        private System.Windows.Forms.Button btnGenSsml;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblSsmlHdr;
        private System.Windows.Forms.TextBox txtSsml;
        private System.Windows.Forms.Label lblOutName;
        private System.Windows.Forms.TextBox txtOutName;
        private System.Windows.Forms.Label lblStatus;
    }
}