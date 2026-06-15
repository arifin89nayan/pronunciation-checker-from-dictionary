namespace TTSAgent.UI.Screens
{
    partial class ScriptInputScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label headingLabel;
        private System.Windows.Forms.TableLayoutPanel settingsLayout;
        private System.Windows.Forms.Label projectLabel;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.Label voiceLabel;
        private System.Windows.Forms.ComboBox voiceComboBox;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.ComboBox speedComboBox;
        private System.Windows.Forms.RichTextBox scriptTextBox;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoadSample;
        private System.Windows.Forms.Label progressLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            headingLabel = new System.Windows.Forms.Label();
            settingsLayout = new System.Windows.Forms.TableLayoutPanel();
            projectLabel = new System.Windows.Forms.Label();
            projectNameTextBox = new System.Windows.Forms.TextBox();
            voiceLabel = new System.Windows.Forms.Label();
            voiceComboBox = new System.Windows.Forms.ComboBox();
            speedLabel = new System.Windows.Forms.Label();
            speedComboBox = new System.Windows.Forms.ComboBox();
            scriptTextBox = new System.Windows.Forms.RichTextBox();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnExtract = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            btnLoadSample = new System.Windows.Forms.Button();
            progressLabel = new System.Windows.Forms.Label();
            rootLayout.SuspendLayout();
            settingsLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(headingLabel, 0, 0);
            rootLayout.Controls.Add(settingsLayout, 0, 1);
            rootLayout.Controls.Add(scriptTextBox, 0, 2);
            rootLayout.Controls.Add(buttonPanel, 0, 3);
            rootLayout.Controls.Add(progressLabel, 0, 4);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 5;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // headingLabel
            // 
            headingLabel.AutoSize = true;
            headingLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            headingLabel.Location = new System.Drawing.Point(3, 0);
            headingLabel.Name = "headingLabel";
            headingLabel.Size = new System.Drawing.Size(557, 25);
            headingLabel.TabIndex = 0;
            headingLabel.Text = "Paste Japanese source script, choose voice settings, then extract terms.";
            // 
            // settingsLayout
            // 
            settingsLayout.ColumnCount = 6;
            settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            settingsLayout.Controls.Add(projectLabel, 0, 0);
            settingsLayout.Controls.Add(projectNameTextBox, 1, 0);
            settingsLayout.Controls.Add(voiceLabel, 2, 0);
            settingsLayout.Controls.Add(voiceComboBox, 3, 0);
            settingsLayout.Controls.Add(speedLabel, 4, 0);
            settingsLayout.Controls.Add(speedComboBox, 5, 0);
            settingsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsLayout.Location = new System.Drawing.Point(3, 51);
            settingsLayout.Name = "settingsLayout";
            settingsLayout.RowCount = 1;
            settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsLayout.Size = new System.Drawing.Size(993, 80);
            settingsLayout.TabIndex = 1;
            // 
            // projectLabel
            // 
            projectLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            projectLabel.AutoSize = true;
            projectLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            projectLabel.Location = new System.Drawing.Point(3, 30);
            projectLabel.Name = "projectLabel";
            projectLabel.Size = new System.Drawing.Size(60, 19);
            projectLabel.TabIndex = 0;
            projectLabel.Text = "Project";
            // 
            // projectNameTextBox
            // 
            projectNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            projectNameTextBox.Location = new System.Drawing.Point(98, 28);
            projectNameTextBox.Name = "projectNameTextBox";
            projectNameTextBox.Size = new System.Drawing.Size(291, 23);
            projectNameTextBox.TabIndex = 1;
            // 
            // voiceLabel
            // 
            voiceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            voiceLabel.AutoSize = true;
            voiceLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            voiceLabel.Location = new System.Drawing.Point(395, 30);
            voiceLabel.Name = "voiceLabel";
            voiceLabel.Size = new System.Drawing.Size(46, 19);
            voiceLabel.TabIndex = 2;
            voiceLabel.Text = "Voice";
            // 
            // voiceComboBox
            // 
            voiceComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            voiceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            voiceComboBox.FormattingEnabled = true;
            voiceComboBox.Items.AddRange(new object[] { "ja-JP-NanamiNeural", "ja-JP-KeitaNeural", "ja-JP-AoiNeural", "ja-JP-DaichiNeural", "ja-JP-MayuNeural", "ja-JP-ShioriNeural" });
            voiceComboBox.Location = new System.Drawing.Point(475, 28);
            voiceComboBox.Name = "voiceComboBox";
            voiceComboBox.Size = new System.Drawing.Size(254, 23);
            voiceComboBox.TabIndex = 3;
            // 
            // speedLabel
            // 
            speedLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            speedLabel.AutoSize = true;
            speedLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            speedLabel.Location = new System.Drawing.Point(735, 30);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new System.Drawing.Size(50, 19);
            speedLabel.TabIndex = 4;
            speedLabel.Text = "Speed";
            // 
            // speedComboBox
            // 
            speedComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            speedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            speedComboBox.FormattingEnabled = true;
            speedComboBox.Items.AddRange(new object[] { "Slow", "Normal", "Fast" });
            speedComboBox.Location = new System.Drawing.Point(810, 28);
            speedComboBox.Name = "speedComboBox";
            speedComboBox.Size = new System.Drawing.Size(180, 23);
            speedComboBox.TabIndex = 5;
            // 
            // scriptTextBox
            // 
            scriptTextBox.AcceptsTab = true;
            scriptTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            scriptTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            scriptTextBox.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            scriptTextBox.Location = new System.Drawing.Point(3, 137);
            scriptTextBox.Name = "scriptTextBox";
            scriptTextBox.Size = new System.Drawing.Size(993, 444);
            scriptTextBox.TabIndex = 2;
            scriptTextBox.Text = "";
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnExtract);
            buttonPanel.Controls.Add(btnClear);
            buttonPanel.Controls.Add(btnLoadSample);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPanel.Location = new System.Drawing.Point(3, 587);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(993, 52);
            buttonPanel.TabIndex = 3;
            // 
            // btnExtract
            // 
            btnExtract.Location = new System.Drawing.Point(3, 3);
            btnExtract.Name = "btnExtract";
            btnExtract.Size = new System.Drawing.Size(190, 38);
            btnExtract.TabIndex = 0;
            btnExtract.Text = "Extract Kanji Words";
            btnExtract.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(199, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(120, 34);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnLoadSample
            // 
            btnLoadSample.Location = new System.Drawing.Point(325, 3);
            btnLoadSample.Name = "btnLoadSample";
            btnLoadSample.Size = new System.Drawing.Size(160, 34);
            btnLoadSample.TabIndex = 2;
            btnLoadSample.Text = "Load Sample Text";
            btnLoadSample.UseVisualStyleBackColor = true;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            progressLabel.Location = new System.Drawing.Point(3, 642);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new System.Drawing.Size(39, 15);
            progressLabel.TabIndex = 4;
            progressLabel.Text = "Ready";
            // 
            // ScriptInputScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "ScriptInputScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            settingsLayout.ResumeLayout(false);
            settingsLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
