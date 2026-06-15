namespace TTSAgent.UI.Screens
{
    partial class TtsListScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.DataGridView ttsGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnAzure;
        private System.Windows.Forms.Button btnHumanReview;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            infoLabel = new System.Windows.Forms.Label();
            ttsGrid = new System.Windows.Forms.DataGridView();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnGenerate = new System.Windows.Forms.Button();
            btnExport = new System.Windows.Forms.Button();
            btnAzure = new System.Windows.Forms.Button();
            btnHumanReview = new System.Windows.Forms.Button();
            rootLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ttsGrid).BeginInit();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(infoLabel, 0, 0);
            rootLayout.Controls.Add(ttsGrid, 0, 1);
            rootLayout.Controls.Add(buttonPanel, 0, 2);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            infoLabel.Location = new System.Drawing.Point(3, 0);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new System.Drawing.Size(318, 19);
            infoLabel.TabIndex = 0;
            infoLabel.Text = "Generate final merged list for SSML injection.";
            // 
            // ttsGrid
            // 
            ttsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ttsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            ttsGrid.Location = new System.Drawing.Point(3, 51);
            ttsGrid.Name = "ttsGrid";
            ttsGrid.ReadOnly = true;
            ttsGrid.Size = new System.Drawing.Size(993, 562);
            ttsGrid.TabIndex = 1;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnGenerate);
            buttonPanel.Controls.Add(btnExport);
            buttonPanel.Controls.Add(btnAzure);
            buttonPanel.Controls.Add(btnHumanReview);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPanel.Location = new System.Drawing.Point(3, 619);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(993, 52);
            buttonPanel.TabIndex = 2;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new System.Drawing.Point(3, 3);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(190, 38);
            btnGenerate.TabIndex = 0;
            btnGenerate.Text = "Generate TTS List";
            btnGenerate.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new System.Drawing.Point(199, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(130, 34);
            btnExport.TabIndex = 1;
            btnExport.Text = "Export CSV";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnAzure
            // 
            btnAzure.Location = new System.Drawing.Point(335, 3);
            btnAzure.Name = "btnAzure";
            btnAzure.Size = new System.Drawing.Size(130, 34);
            btnAzure.TabIndex = 2;
            btnAzure.Text = "Azure TTS";
            btnAzure.UseVisualStyleBackColor = true;
            // 
            // btnHumanReview
            // 
            btnHumanReview.Location = new System.Drawing.Point(471, 3);
            btnHumanReview.Name = "btnHumanReview";
            btnHumanReview.Size = new System.Drawing.Size(140, 34);
            btnHumanReview.TabIndex = 3;
            btnHumanReview.Text = "Human Review";
            btnHumanReview.UseVisualStyleBackColor = true;
            // 
            // TtsListScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "TtsListScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ttsGrid).EndInit();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
