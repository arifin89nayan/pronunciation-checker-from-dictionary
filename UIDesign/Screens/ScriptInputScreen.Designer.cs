namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class ScriptInputScreen
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
            this.lblProject = new System.Windows.Forms.Label();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.lblVoice = new System.Windows.Forms.Label();
            this.cmbVoice = new System.Windows.Forms.ComboBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.cmbSpeed = new System.Windows.Forms.ComboBox();
            this.lblScriptHdr = new System.Windows.Forms.Label();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblSummaryHdr = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 2 — Script Input";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "Paste the Japanese narration script and extract kanji words.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblProject
            this.lblProject.Name = "lblProject";
            this.lblProject.Location = new System.Drawing.Point(20, 80);
            this.lblProject.Text = "Project Name:";
            this.lblProject.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblProject.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblProject.AutoSize = true;
            // txtProject
            this.txtProject.Name = "txtProject";
            this.txtProject.Location = new System.Drawing.Point(140, 77);
            this.txtProject.Size = new System.Drawing.Size(360, 23);
            this.txtProject.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            // lblVoice
            this.lblVoice.Name = "lblVoice";
            this.lblVoice.Location = new System.Drawing.Point(20, 112);
            this.lblVoice.Text = "Speaker Voice:";
            this.lblVoice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblVoice.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblVoice.AutoSize = true;
            // cmbVoice
            this.cmbVoice.Name = "cmbVoice";
            this.cmbVoice.Location = new System.Drawing.Point(140, 109);
            this.cmbVoice.Size = new System.Drawing.Size(220, 25);
            this.cmbVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // lblSpeed
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Location = new System.Drawing.Point(380, 112);
            this.lblSpeed.Text = "Speed:";
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblSpeed.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblSpeed.AutoSize = true;
            // cmbSpeed
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Location = new System.Drawing.Point(440, 109);
            this.cmbSpeed.Size = new System.Drawing.Size(110, 25);
            this.cmbSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeed.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // lblScriptHdr
            this.lblScriptHdr.Name = "lblScriptHdr";
            this.lblScriptHdr.Location = new System.Drawing.Point(20, 148);
            this.lblScriptHdr.Text = "Original Japanese Script";
            this.lblScriptHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblScriptHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblScriptHdr.AutoSize = true;
            // txtScript
            this.txtScript.Name = "txtScript";
            this.txtScript.Location = new System.Drawing.Point(20, 172);
            this.txtScript.Size = new System.Drawing.Size(760, 180);
            this.txtScript.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.txtScript.Multiline = true;
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // btnExtract
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Location = new System.Drawing.Point(20, 364);
            this.btnExtract.Size = new System.Drawing.Size(160, 32);
            this.btnExtract.Text = "Extract Kanji Words";
            this.btnExtract.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExtract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtract.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnExtract.ForeColor = System.Drawing.Color.White;
            this.btnExtract.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnExtract.UseVisualStyleBackColor = false;
            this.btnExtract.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // btnCheck
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Location = new System.Drawing.Point(190, 364);
            this.btnCheck.Size = new System.Drawing.Size(150, 32);
            this.btnCheck.Text = "Check Dictionary";
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnCheck.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnCheck.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // btnClear
            this.btnClear.Name = "btnClear";
            this.btnClear.Location = new System.Drawing.Point(350, 364);
            this.btnClear.Size = new System.Drawing.Size(90, 32);
            this.btnClear.Text = "Clear";
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // lblSummaryHdr
            this.lblSummaryHdr.Name = "lblSummaryHdr";
            this.lblSummaryHdr.Location = new System.Drawing.Point(20, 410);
            this.lblSummaryHdr.Text = "Extraction Summary";
            this.lblSummaryHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSummaryHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblSummaryHdr.AutoSize = true;
            // lblSummary
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Location = new System.Drawing.Point(20, 436);
            this.lblSummary.Text = "";
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblSummary.AutoSize = true;
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 470);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // ScriptInputScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.txtProject);
            this.Controls.Add(this.lblVoice);
            this.Controls.Add(this.cmbVoice);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.cmbSpeed);
            this.Controls.Add(this.lblScriptHdr);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblSummaryHdr);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblStatus);
            this.Name = "ScriptInputScreen";
            this.Size = new System.Drawing.Size(880, 600);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Label lblVoice;
        private System.Windows.Forms.ComboBox cmbVoice;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.ComboBox cmbSpeed;
        private System.Windows.Forms.Label lblScriptHdr;
        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblSummaryHdr;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label lblStatus;
    }
}