namespace TTSAgent.UI.Screens
{
    partial class SettingsScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.Label llmKeyLabel;
        private System.Windows.Forms.TextBox llmKeyTextBox;
        private System.Windows.Forms.Label llmModelLabel;
        private System.Windows.Forms.TextBox llmModelTextBox;
        private System.Windows.Forms.Label azureKeyLabel;
        private System.Windows.Forms.TextBox azureKeyTextBox;
        private System.Windows.Forms.Label azureRegionLabel;
        private System.Windows.Forms.TextBox azureRegionTextBox;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label savedLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            noteLabel = new System.Windows.Forms.Label();
            llmKeyLabel = new System.Windows.Forms.Label();
            llmKeyTextBox = new System.Windows.Forms.TextBox();
            llmModelLabel = new System.Windows.Forms.Label();
            llmModelTextBox = new System.Windows.Forms.TextBox();
            azureKeyLabel = new System.Windows.Forms.Label();
            azureKeyTextBox = new System.Windows.Forms.TextBox();
            azureRegionLabel = new System.Windows.Forms.Label();
            azureRegionTextBox = new System.Windows.Forms.TextBox();
            outputLabel = new System.Windows.Forms.Label();
            outputTextBox = new System.Windows.Forms.TextBox();
            btnBrowseOutput = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            savedLabel = new System.Windows.Forms.Label();
            rootLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 3;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            rootLayout.Controls.Add(noteLabel, 0, 0);
            rootLayout.Controls.Add(llmKeyLabel, 0, 1);
            rootLayout.Controls.Add(llmKeyTextBox, 1, 1);
            rootLayout.Controls.Add(llmModelLabel, 0, 2);
            rootLayout.Controls.Add(llmModelTextBox, 1, 2);
            rootLayout.Controls.Add(azureKeyLabel, 0, 3);
            rootLayout.Controls.Add(azureKeyTextBox, 1, 3);
            rootLayout.Controls.Add(azureRegionLabel, 0, 4);
            rootLayout.Controls.Add(azureRegionTextBox, 1, 4);
            rootLayout.Controls.Add(outputLabel, 0, 5);
            rootLayout.Controls.Add(outputTextBox, 1, 5);
            rootLayout.Controls.Add(btnBrowseOutput, 2, 5);
            rootLayout.Controls.Add(btnSave, 1, 6);
            rootLayout.Controls.Add(savedLabel, 1, 7);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new System.Windows.Forms.Padding(12);
            rootLayout.RowCount = 8;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // noteLabel
            // 
            noteLabel.AutoSize = true;
            rootLayout.SetColumnSpan(noteLabel, 3);
            noteLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            noteLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            noteLabel.Location = new System.Drawing.Point(15, 12);
            noteLabel.Name = "noteLabel";
            noteLabel.Size = new System.Drawing.Size(718, 38);
            noteLabel.TabIndex = 0;
            noteLabel.Text = "Keys are saved to appsettings.json next to the executable. You can also use ANTHROPIC_API_KEY, AZURE_SPEECH_KEY, and AZURE_SPEECH_REGION environment variables.";
            // 
            // llmKeyLabel
            // 
            llmKeyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            llmKeyLabel.AutoSize = true;
            llmKeyLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            llmKeyLabel.Location = new System.Drawing.Point(15, 120);
            llmKeyLabel.Name = "llmKeyLabel";
            llmKeyLabel.Size = new System.Drawing.Size(91, 19);
            llmKeyLabel.TabIndex = 1;
            llmKeyLabel.Text = "LLM API Key";
            // 
            // llmKeyTextBox
            // 
            llmKeyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rootLayout.SetColumnSpan(llmKeyTextBox, 2);
            llmKeyTextBox.Location = new System.Drawing.Point(185, 118);
            llmKeyTextBox.Name = "llmKeyTextBox";
            llmKeyTextBox.PasswordChar = '*';
            llmKeyTextBox.Size = new System.Drawing.Size(799, 23);
            llmKeyTextBox.TabIndex = 2;
            // 
            // llmModelLabel
            // 
            llmModelLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            llmModelLabel.AutoSize = true;
            llmModelLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            llmModelLabel.Location = new System.Drawing.Point(15, 176);
            llmModelLabel.Name = "llmModelLabel";
            llmModelLabel.Size = new System.Drawing.Size(82, 19);
            llmModelLabel.TabIndex = 3;
            llmModelLabel.Text = "LLM Model";
            // 
            // llmModelTextBox
            // 
            llmModelTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rootLayout.SetColumnSpan(llmModelTextBox, 2);
            llmModelTextBox.Location = new System.Drawing.Point(185, 174);
            llmModelTextBox.Name = "llmModelTextBox";
            llmModelTextBox.Size = new System.Drawing.Size(799, 23);
            llmModelTextBox.TabIndex = 4;
            // 
            // azureKeyLabel
            // 
            azureKeyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            azureKeyLabel.AutoSize = true;
            azureKeyLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            azureKeyLabel.Location = new System.Drawing.Point(15, 232);
            azureKeyLabel.Name = "azureKeyLabel";
            azureKeyLabel.Size = new System.Drawing.Size(129, 19);
            azureKeyLabel.TabIndex = 5;
            azureKeyLabel.Text = "Azure Speech Key";
            // 
            // azureKeyTextBox
            // 
            azureKeyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rootLayout.SetColumnSpan(azureKeyTextBox, 2);
            azureKeyTextBox.Location = new System.Drawing.Point(185, 230);
            azureKeyTextBox.Name = "azureKeyTextBox";
            azureKeyTextBox.PasswordChar = '*';
            azureKeyTextBox.Size = new System.Drawing.Size(799, 23);
            azureKeyTextBox.TabIndex = 6;
            // 
            // azureRegionLabel
            // 
            azureRegionLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            azureRegionLabel.AutoSize = true;
            azureRegionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            azureRegionLabel.Location = new System.Drawing.Point(15, 288);
            azureRegionLabel.Name = "azureRegionLabel";
            azureRegionLabel.Size = new System.Drawing.Size(144, 19);
            azureRegionLabel.TabIndex = 7;
            azureRegionLabel.Text = "Azure Speech Region";
            // 
            // azureRegionTextBox
            // 
            azureRegionTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rootLayout.SetColumnSpan(azureRegionTextBox, 2);
            azureRegionTextBox.Location = new System.Drawing.Point(185, 286);
            azureRegionTextBox.Name = "azureRegionTextBox";
            azureRegionTextBox.Size = new System.Drawing.Size(799, 23);
            azureRegionTextBox.TabIndex = 8;
            // 
            // outputLabel
            // 
            outputLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            outputLabel.AutoSize = true;
            outputLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            outputLabel.Location = new System.Drawing.Point(15, 344);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new System.Drawing.Size(103, 19);
            outputLabel.TabIndex = 9;
            outputLabel.Text = "Output Folder";
            // 
            // outputTextBox
            // 
            outputTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            outputTextBox.Location = new System.Drawing.Point(185, 342);
            outputTextBox.Name = "outputTextBox";
            outputTextBox.Size = new System.Drawing.Size(653, 23);
            outputTextBox.TabIndex = 10;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnBrowseOutput.Location = new System.Drawing.Point(844, 337);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new System.Drawing.Size(110, 34);
            btnBrowseOutput.TabIndex = 11;
            btnBrowseOutput.Text = "Browse";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnSave.Location = new System.Drawing.Point(185, 392);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(160, 38);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save Settings";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // savedLabel
            // 
            savedLabel.AutoSize = true;
            savedLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            savedLabel.Location = new System.Drawing.Point(185, 438);
            savedLabel.Name = "savedLabel";
            savedLabel.Size = new System.Drawing.Size(39, 15);
            savedLabel.TabIndex = 13;
            savedLabel.Text = "Ready";
            // 
            // SettingsScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "SettingsScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            ResumeLayout(false);
        }
    }
}
