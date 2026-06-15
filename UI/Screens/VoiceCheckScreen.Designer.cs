namespace TTSAgent.UI.Screens
{
    partial class VoiceCheckScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.TableLayoutPanel fileLayout;
        private System.Windows.Forms.Label audioLabel;
        private System.Windows.Forms.TextBox audioPathTextBox;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.TextBox recognizedTextBox;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnRunCheck;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Label statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            fileLayout = new System.Windows.Forms.TableLayoutPanel();
            audioLabel = new System.Windows.Forms.Label();
            audioPathTextBox = new System.Windows.Forms.TextBox();
            btnBrowse = new System.Windows.Forms.Button();
            splitContainer = new System.Windows.Forms.SplitContainer();
            resultGrid = new System.Windows.Forms.DataGridView();
            recognizedTextBox = new System.Windows.Forms.TextBox();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnPlay = new System.Windows.Forms.Button();
            btnRunCheck = new System.Windows.Forms.Button();
            btnOpenFolder = new System.Windows.Forms.Button();
            statusLabel = new System.Windows.Forms.Label();
            rootLayout.SuspendLayout();
            fileLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultGrid).BeginInit();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(fileLayout, 0, 0);
            rootLayout.Controls.Add(splitContainer, 0, 1);
            rootLayout.Controls.Add(buttonPanel, 0, 2);
            rootLayout.Controls.Add(statusLabel, 0, 3);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // fileLayout
            // 
            fileLayout.ColumnCount = 3;
            fileLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            fileLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            fileLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            fileLayout.Controls.Add(audioLabel, 0, 0);
            fileLayout.Controls.Add(audioPathTextBox, 1, 0);
            fileLayout.Controls.Add(btnBrowse, 2, 0);
            fileLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            fileLayout.Location = new System.Drawing.Point(3, 3);
            fileLayout.Name = "fileLayout";
            fileLayout.RowCount = 1;
            fileLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            fileLayout.Size = new System.Drawing.Size(993, 52);
            fileLayout.TabIndex = 0;
            // 
            // audioLabel
            // 
            audioLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            audioLabel.AutoSize = true;
            audioLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            audioLabel.Location = new System.Drawing.Point(3, 16);
            audioLabel.Name = "audioLabel";
            audioLabel.Size = new System.Drawing.Size(75, 19);
            audioLabel.TabIndex = 0;
            audioLabel.Text = "Audio File";
            // 
            // audioPathTextBox
            // 
            audioPathTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            audioPathTextBox.Location = new System.Drawing.Point(93, 14);
            audioPathTextBox.Name = "audioPathTextBox";
            audioPathTextBox.Size = new System.Drawing.Size(777, 23);
            audioPathTextBox.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnBrowse.Location = new System.Drawing.Point(876, 9);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(100, 34);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(3, 61);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(resultGrid);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(recognizedTextBox);
            splitContainer.Size = new System.Drawing.Size(993, 520);
            splitContainer.SplitterDistance = 496;
            splitContainer.TabIndex = 1;
            // 
            // resultGrid
            // 
            resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            resultGrid.Location = new System.Drawing.Point(0, 0);
            resultGrid.Name = "resultGrid";
            resultGrid.ReadOnly = true;
            resultGrid.Size = new System.Drawing.Size(496, 520);
            resultGrid.TabIndex = 0;
            // 
            // recognizedTextBox
            // 
            recognizedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            recognizedTextBox.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            recognizedTextBox.Location = new System.Drawing.Point(0, 0);
            recognizedTextBox.Multiline = true;
            recognizedTextBox.Name = "recognizedTextBox";
            recognizedTextBox.ReadOnly = true;
            recognizedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            recognizedTextBox.Size = new System.Drawing.Size(493, 520);
            recognizedTextBox.TabIndex = 0;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnPlay);
            buttonPanel.Controls.Add(btnRunCheck);
            buttonPanel.Controls.Add(btnOpenFolder);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPanel.Location = new System.Drawing.Point(3, 587);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(993, 52);
            buttonPanel.TabIndex = 2;
            // 
            // btnPlay
            // 
            btnPlay.Location = new System.Drawing.Point(3, 3);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new System.Drawing.Size(110, 34);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "Play WAV";
            btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnRunCheck
            // 
            btnRunCheck.Location = new System.Drawing.Point(119, 3);
            btnRunCheck.Name = "btnRunCheck";
            btnRunCheck.Size = new System.Drawing.Size(170, 38);
            btnRunCheck.TabIndex = 1;
            btnRunCheck.Text = "Run Quality Check";
            btnRunCheck.UseVisualStyleBackColor = true;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.Location = new System.Drawing.Point(295, 3);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new System.Drawing.Size(130, 34);
            btnOpenFolder.TabIndex = 2;
            btnOpenFolder.Text = "Open Folder";
            btnOpenFolder.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            statusLabel.Location = new System.Drawing.Point(3, 642);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(39, 15);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Ready";
            // 
            // VoiceCheckScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "VoiceCheckScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            fileLayout.ResumeLayout(false);
            fileLayout.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)resultGrid).EndInit();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
