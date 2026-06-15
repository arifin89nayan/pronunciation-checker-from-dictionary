namespace TTSAgent.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label appTitleLabel;
        private System.Windows.Forms.Label appSubtitleLabel;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnScriptInput;
        private System.Windows.Forms.Button btnKanjiReview;
        private System.Windows.Forms.Button btnHumanReview;
        private System.Windows.Forms.Button btnDictionary;
        private System.Windows.Forms.Button btnTtsList;
        private System.Windows.Forms.Button btnAzureTts;
        private System.Windows.Forms.Button btnVoiceCheck;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label screenTitleLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel contentPanel;

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
            sidebarPanel = new System.Windows.Forms.Panel();
            btnSettings = new System.Windows.Forms.Button();
            btnVoiceCheck = new System.Windows.Forms.Button();
            btnAzureTts = new System.Windows.Forms.Button();
            btnTtsList = new System.Windows.Forms.Button();
            btnDictionary = new System.Windows.Forms.Button();
            btnHumanReview = new System.Windows.Forms.Button();
            btnKanjiReview = new System.Windows.Forms.Button();
            btnScriptInput = new System.Windows.Forms.Button();
            btnDashboard = new System.Windows.Forms.Button();
            logoPanel = new System.Windows.Forms.Panel();
            appSubtitleLabel = new System.Windows.Forms.Label();
            appTitleLabel = new System.Windows.Forms.Label();
            topPanel = new System.Windows.Forms.Panel();
            statusLabel = new System.Windows.Forms.Label();
            screenTitleLabel = new System.Windows.Forms.Label();
            contentPanel = new System.Windows.Forms.Panel();
            sidebarPanel.SuspendLayout();
            logoPanel.SuspendLayout();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            sidebarPanel.Controls.Add(btnSettings);
            sidebarPanel.Controls.Add(btnVoiceCheck);
            sidebarPanel.Controls.Add(btnAzureTts);
            sidebarPanel.Controls.Add(btnTtsList);
            sidebarPanel.Controls.Add(btnDictionary);
            sidebarPanel.Controls.Add(btnHumanReview);
            sidebarPanel.Controls.Add(btnKanjiReview);
            sidebarPanel.Controls.Add(btnScriptInput);
            sidebarPanel.Controls.Add(btnDashboard);
            sidebarPanel.Controls.Add(logoPanel);
            sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            sidebarPanel.Location = new System.Drawing.Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new System.Drawing.Size(245, 780);
            sidebarPanel.TabIndex = 0;
            // 
            // btnSettings
            // 
            btnSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSettings.ForeColor = System.Drawing.Color.White;
            btnSettings.Location = new System.Drawing.Point(0, 730);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnSettings.Size = new System.Drawing.Size(245, 50);
            btnSettings.TabIndex = 9;
            btnSettings.Text = "⚙  Settings";
            btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnVoiceCheck
            // 
            btnVoiceCheck.Dock = System.Windows.Forms.DockStyle.Top;
            btnVoiceCheck.FlatAppearance.BorderSize = 0;
            btnVoiceCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnVoiceCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnVoiceCheck.ForeColor = System.Drawing.Color.White;
            btnVoiceCheck.Location = new System.Drawing.Point(0, 435);
            btnVoiceCheck.Name = "btnVoiceCheck";
            btnVoiceCheck.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnVoiceCheck.Size = new System.Drawing.Size(245, 50);
            btnVoiceCheck.TabIndex = 8;
            btnVoiceCheck.Text = "⑧ Voice Check";
            btnVoiceCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnVoiceCheck.UseVisualStyleBackColor = true;
            // 
            // btnAzureTts
            // 
            btnAzureTts.Dock = System.Windows.Forms.DockStyle.Top;
            btnAzureTts.FlatAppearance.BorderSize = 0;
            btnAzureTts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAzureTts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnAzureTts.ForeColor = System.Drawing.Color.White;
            btnAzureTts.Location = new System.Drawing.Point(0, 385);
            btnAzureTts.Name = "btnAzureTts";
            btnAzureTts.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnAzureTts.Size = new System.Drawing.Size(245, 50);
            btnAzureTts.TabIndex = 7;
            btnAzureTts.Text = "⑦ Azure TTS";
            btnAzureTts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnAzureTts.UseVisualStyleBackColor = true;
            // 
            // btnTtsList
            // 
            btnTtsList.Dock = System.Windows.Forms.DockStyle.Top;
            btnTtsList.FlatAppearance.BorderSize = 0;
            btnTtsList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTtsList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnTtsList.ForeColor = System.Drawing.Color.White;
            btnTtsList.Location = new System.Drawing.Point(0, 335);
            btnTtsList.Name = "btnTtsList";
            btnTtsList.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnTtsList.Size = new System.Drawing.Size(245, 50);
            btnTtsList.TabIndex = 6;
            btnTtsList.Text = "⑥ TTS Script";
            btnTtsList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnTtsList.UseVisualStyleBackColor = true;
            // 
            // btnDictionary
            // 
            btnDictionary.Dock = System.Windows.Forms.DockStyle.Top;
            btnDictionary.FlatAppearance.BorderSize = 0;
            btnDictionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDictionary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnDictionary.ForeColor = System.Drawing.Color.White;
            btnDictionary.Location = new System.Drawing.Point(0, 285);
            btnDictionary.Name = "btnDictionary";
            btnDictionary.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnDictionary.Size = new System.Drawing.Size(245, 50);
            btnDictionary.TabIndex = 5;
            btnDictionary.Text = "⑤ Dictionary";
            btnDictionary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnDictionary.UseVisualStyleBackColor = true;
            // 
            // btnHumanReview
            // 
            btnHumanReview.Dock = System.Windows.Forms.DockStyle.Top;
            btnHumanReview.FlatAppearance.BorderSize = 0;
            btnHumanReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHumanReview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnHumanReview.ForeColor = System.Drawing.Color.White;
            btnHumanReview.Location = new System.Drawing.Point(0, 235);
            btnHumanReview.Name = "btnHumanReview";
            btnHumanReview.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnHumanReview.Size = new System.Drawing.Size(245, 50);
            btnHumanReview.TabIndex = 4;
            btnHumanReview.Text = "④ Human Review";
            btnHumanReview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnHumanReview.UseVisualStyleBackColor = true;
            // 
            // btnKanjiReview
            // 
            btnKanjiReview.Dock = System.Windows.Forms.DockStyle.Top;
            btnKanjiReview.FlatAppearance.BorderSize = 0;
            btnKanjiReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnKanjiReview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnKanjiReview.ForeColor = System.Drawing.Color.White;
            btnKanjiReview.Location = new System.Drawing.Point(0, 185);
            btnKanjiReview.Name = "btnKanjiReview";
            btnKanjiReview.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnKanjiReview.Size = new System.Drawing.Size(245, 50);
            btnKanjiReview.TabIndex = 3;
            btnKanjiReview.Text = "③ Kanji Review";
            btnKanjiReview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnKanjiReview.UseVisualStyleBackColor = true;
            // 
            // btnScriptInput
            // 
            btnScriptInput.Dock = System.Windows.Forms.DockStyle.Top;
            btnScriptInput.FlatAppearance.BorderSize = 0;
            btnScriptInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnScriptInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnScriptInput.ForeColor = System.Drawing.Color.White;
            btnScriptInput.Location = new System.Drawing.Point(0, 135);
            btnScriptInput.Name = "btnScriptInput";
            btnScriptInput.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnScriptInput.Size = new System.Drawing.Size(245, 50);
            btnScriptInput.TabIndex = 2;
            btnScriptInput.Text = "② Script Input";
            btnScriptInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnScriptInput.UseVisualStyleBackColor = true;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnDashboard.ForeColor = System.Drawing.Color.White;
            btnDashboard.Location = new System.Drawing.Point(0, 85);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            btnDashboard.Size = new System.Drawing.Size(245, 50);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "① Dashboard";
            btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = true;
            // 
            // logoPanel
            // 
            logoPanel.Controls.Add(appSubtitleLabel);
            logoPanel.Controls.Add(appTitleLabel);
            logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            logoPanel.Location = new System.Drawing.Point(0, 0);
            logoPanel.Name = "logoPanel";
            logoPanel.Size = new System.Drawing.Size(245, 85);
            logoPanel.TabIndex = 0;
            // 
            // appSubtitleLabel
            // 
            appSubtitleLabel.AutoSize = true;
            appSubtitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            appSubtitleLabel.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            appSubtitleLabel.Location = new System.Drawing.Point(22, 48);
            appSubtitleLabel.Name = "appSubtitleLabel";
            appSubtitleLabel.Size = new System.Drawing.Size(172, 13);
            appSubtitleLabel.TabIndex = 1;
            appSubtitleLabel.Text = "Human-in-the-loop pronunciation";
            // 
            // appTitleLabel
            // 
            appTitleLabel.AutoSize = true;
            appTitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            appTitleLabel.ForeColor = System.Drawing.Color.White;
            appTitleLabel.Location = new System.Drawing.Point(20, 19);
            appTitleLabel.Name = "appTitleLabel";
            appTitleLabel.Size = new System.Drawing.Size(101, 25);
            appTitleLabel.TabIndex = 0;
            appTitleLabel.Text = "TTS Agent";
            // 
            // topPanel
            // 
            topPanel.BackColor = System.Drawing.Color.White;
            topPanel.Controls.Add(statusLabel);
            topPanel.Controls.Add(screenTitleLabel);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.Location = new System.Drawing.Point(245, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new System.Drawing.Size(1035, 70);
            topPanel.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            statusLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            statusLabel.Location = new System.Drawing.Point(615, 23);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(390, 20);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Ready";
            statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // screenTitleLabel
            // 
            screenTitleLabel.AutoSize = true;
            screenTitleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            screenTitleLabel.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            screenTitleLabel.Location = new System.Drawing.Point(25, 18);
            screenTitleLabel.Name = "screenTitleLabel";
            screenTitleLabel.Size = new System.Drawing.Size(135, 32);
            screenTitleLabel.TabIndex = 0;
            screenTitleLabel.Text = "Dashboard";
            // 
            // contentPanel
            // 
            contentPanel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            contentPanel.Location = new System.Drawing.Point(245, 70);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new System.Windows.Forms.Padding(18);
            contentPanel.Size = new System.Drawing.Size(1035, 710);
            contentPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(1280, 780);
            Controls.Add(contentPanel);
            Controls.Add(topPanel);
            Controls.Add(sidebarPanel);
            MinimumSize = new System.Drawing.Size(1150, 720);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "TTS Script Generation Agent";
            sidebarPanel.ResumeLayout(false);
            logoPanel.ResumeLayout(false);
            logoPanel.PerformLayout();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}
