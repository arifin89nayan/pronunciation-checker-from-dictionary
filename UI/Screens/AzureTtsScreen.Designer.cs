namespace TTSAgent.UI.Screens
{
    partial class AzureTtsScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.TableLayoutPanel optionsLayout;
        private System.Windows.Forms.Label styleLabel;
        private System.Windows.Forms.ComboBox styleComboBox;
        private System.Windows.Forms.Label rateLabel;
        private System.Windows.Forms.NumericUpDown rateNumeric;
        private System.Windows.Forms.Label pitchLabel;
        private System.Windows.Forms.NumericUpDown pitchNumeric;
        private System.Windows.Forms.TextBox ssmlTextBox;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnGenerateSsml;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnSynthesize;
        private System.Windows.Forms.Button btnSaveSsml;
        private System.Windows.Forms.Button btnVoiceCheck;
        private System.Windows.Forms.Label outputLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            optionsLayout = new System.Windows.Forms.TableLayoutPanel();
            styleLabel = new System.Windows.Forms.Label();
            styleComboBox = new System.Windows.Forms.ComboBox();
            rateLabel = new System.Windows.Forms.Label();
            rateNumeric = new System.Windows.Forms.NumericUpDown();
            pitchLabel = new System.Windows.Forms.Label();
            pitchNumeric = new System.Windows.Forms.NumericUpDown();
            ssmlTextBox = new System.Windows.Forms.TextBox();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnGenerateSsml = new System.Windows.Forms.Button();
            btnValidate = new System.Windows.Forms.Button();
            btnSynthesize = new System.Windows.Forms.Button();
            btnSaveSsml = new System.Windows.Forms.Button();
            btnVoiceCheck = new System.Windows.Forms.Button();
            outputLabel = new System.Windows.Forms.Label();
            rootLayout.SuspendLayout();
            optionsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rateNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pitchNumeric).BeginInit();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(optionsLayout, 0, 0);
            rootLayout.Controls.Add(ssmlTextBox, 0, 1);
            rootLayout.Controls.Add(buttonPanel, 0, 2);
            rootLayout.Controls.Add(outputLabel, 0, 3);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // optionsLayout
            // 
            optionsLayout.ColumnCount = 6;
            optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            optionsLayout.Controls.Add(styleLabel, 0, 0);
            optionsLayout.Controls.Add(styleComboBox, 1, 0);
            optionsLayout.Controls.Add(rateLabel, 2, 0);
            optionsLayout.Controls.Add(rateNumeric, 3, 0);
            optionsLayout.Controls.Add(pitchLabel, 4, 0);
            optionsLayout.Controls.Add(pitchNumeric, 5, 0);
            optionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            optionsLayout.Location = new System.Drawing.Point(3, 3);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 1;
            optionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            optionsLayout.Size = new System.Drawing.Size(993, 56);
            optionsLayout.TabIndex = 0;
            // 
            // styleLabel
            // 
            styleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            styleLabel.AutoSize = true;
            styleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            styleLabel.Location = new System.Drawing.Point(3, 18);
            styleLabel.Name = "styleLabel";
            styleLabel.Size = new System.Drawing.Size(42, 19);
            styleLabel.TabIndex = 0;
            styleLabel.Text = "Style";
            // 
            // styleComboBox
            // 
            styleComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            styleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            styleComboBox.FormattingEnabled = true;
            styleComboBox.Items.AddRange(new object[] { "narration", "chat", "cheerful", "calm", "serious", "none" });
            styleComboBox.Location = new System.Drawing.Point(73, 16);
            styleComboBox.Name = "styleComboBox";
            styleComboBox.Size = new System.Drawing.Size(537, 23);
            styleComboBox.TabIndex = 1;
            // 
            // rateLabel
            // 
            rateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            rateLabel.AutoSize = true;
            rateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            rateLabel.Location = new System.Drawing.Point(616, 18);
            rateLabel.Name = "rateLabel";
            rateLabel.Size = new System.Drawing.Size(55, 19);
            rateLabel.TabIndex = 2;
            rateLabel.Text = "Rate %";
            // 
            // rateNumeric
            // 
            rateNumeric.Anchor = System.Windows.Forms.AnchorStyles.Left;
            rateNumeric.Location = new System.Drawing.Point(686, 16);
            rateNumeric.Minimum = new decimal(new int[] { 50, 0, 0, int.MinValue });
            rateNumeric.Name = "rateNumeric";
            rateNumeric.Size = new System.Drawing.Size(95, 23);
            rateNumeric.TabIndex = 3;
            // 
            // pitchLabel
            // 
            pitchLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            pitchLabel.AutoSize = true;
            pitchLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            pitchLabel.Location = new System.Drawing.Point(806, 18);
            pitchLabel.Name = "pitchLabel";
            pitchLabel.Size = new System.Drawing.Size(60, 19);
            pitchLabel.TabIndex = 4;
            pitchLabel.Text = "Pitch %";
            // 
            // pitchNumeric
            // 
            pitchNumeric.Anchor = System.Windows.Forms.AnchorStyles.Left;
            pitchNumeric.Location = new System.Drawing.Point(876, 16);
            pitchNumeric.Minimum = new decimal(new int[] { 50, 0, 0, int.MinValue });
            pitchNumeric.Name = "pitchNumeric";
            pitchNumeric.Size = new System.Drawing.Size(95, 23);
            pitchNumeric.TabIndex = 5;
            // 
            // ssmlTextBox
            // 
            ssmlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            ssmlTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            ssmlTextBox.Location = new System.Drawing.Point(3, 65);
            ssmlTextBox.Multiline = true;
            ssmlTextBox.Name = "ssmlTextBox";
            ssmlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            ssmlTextBox.Size = new System.Drawing.Size(993, 516);
            ssmlTextBox.TabIndex = 1;
            ssmlTextBox.WordWrap = false;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnGenerateSsml);
            buttonPanel.Controls.Add(btnValidate);
            buttonPanel.Controls.Add(btnSynthesize);
            buttonPanel.Controls.Add(btnSaveSsml);
            buttonPanel.Controls.Add(btnVoiceCheck);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPanel.Location = new System.Drawing.Point(3, 587);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(993, 52);
            buttonPanel.TabIndex = 2;
            // 
            // btnGenerateSsml
            // 
            btnGenerateSsml.Location = new System.Drawing.Point(3, 3);
            btnGenerateSsml.Name = "btnGenerateSsml";
            btnGenerateSsml.Size = new System.Drawing.Size(150, 38);
            btnGenerateSsml.TabIndex = 0;
            btnGenerateSsml.Text = "Generate SSML";
            btnGenerateSsml.UseVisualStyleBackColor = true;
            // 
            // btnValidate
            // 
            btnValidate.Location = new System.Drawing.Point(159, 3);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new System.Drawing.Size(120, 34);
            btnValidate.TabIndex = 1;
            btnValidate.Text = "Validate SSML";
            btnValidate.UseVisualStyleBackColor = true;
            // 
            // btnSynthesize
            // 
            btnSynthesize.Location = new System.Drawing.Point(285, 3);
            btnSynthesize.Name = "btnSynthesize";
            btnSynthesize.Size = new System.Drawing.Size(150, 34);
            btnSynthesize.TabIndex = 2;
            btnSynthesize.Text = "Send to Azure TTS";
            btnSynthesize.UseVisualStyleBackColor = true;
            // 
            // btnSaveSsml
            // 
            btnSaveSsml.Location = new System.Drawing.Point(441, 3);
            btnSaveSsml.Name = "btnSaveSsml";
            btnSaveSsml.Size = new System.Drawing.Size(120, 34);
            btnSaveSsml.TabIndex = 3;
            btnSaveSsml.Text = "Save SSML";
            btnSaveSsml.UseVisualStyleBackColor = true;
            // 
            // btnVoiceCheck
            // 
            btnVoiceCheck.Location = new System.Drawing.Point(567, 3);
            btnVoiceCheck.Name = "btnVoiceCheck";
            btnVoiceCheck.Size = new System.Drawing.Size(130, 34);
            btnVoiceCheck.TabIndex = 4;
            btnVoiceCheck.Text = "Voice Check";
            btnVoiceCheck.UseVisualStyleBackColor = true;
            // 
            // outputLabel
            // 
            outputLabel.AutoSize = true;
            outputLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            outputLabel.Location = new System.Drawing.Point(3, 642);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new System.Drawing.Size(90, 15);
            outputLabel.TabIndex = 3;
            outputLabel.Text = "No audio output.";
            // 
            // AzureTtsScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "AzureTtsScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rateNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)pitchNumeric).EndInit();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
