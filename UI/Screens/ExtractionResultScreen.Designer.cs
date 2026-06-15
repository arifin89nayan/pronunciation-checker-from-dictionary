namespace TTSAgent.UI.Screens
{
    partial class ExtractionResultScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.DataGridView termsGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnSendReview;
        private System.Windows.Forms.Button btnHumanReview;
        private System.Windows.Forms.Button btnTtsList;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            summaryLabel = new System.Windows.Forms.Label();
            termsGrid = new System.Windows.Forms.DataGridView();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnSendReview = new System.Windows.Forms.Button();
            btnHumanReview = new System.Windows.Forms.Button();
            btnTtsList = new System.Windows.Forms.Button();
            btnBack = new System.Windows.Forms.Button();
            rootLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)termsGrid).BeginInit();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(summaryLabel, 0, 0);
            rootLayout.Controls.Add(termsGrid, 0, 1);
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
            // summaryLabel
            // 
            summaryLabel.AutoSize = true;
            summaryLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            summaryLabel.Location = new System.Drawing.Point(3, 0);
            summaryLabel.Name = "summaryLabel";
            summaryLabel.Size = new System.Drawing.Size(229, 19);
            summaryLabel.TabIndex = 0;
            summaryLabel.Text = "No extraction result. Run Screen 2.";
            // 
            // termsGrid
            // 
            termsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            termsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            termsGrid.Location = new System.Drawing.Point(3, 51);
            termsGrid.Name = "termsGrid";
            termsGrid.ReadOnly = true;
            termsGrid.Size = new System.Drawing.Size(993, 562);
            termsGrid.TabIndex = 1;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnSendReview);
            buttonPanel.Controls.Add(btnHumanReview);
            buttonPanel.Controls.Add(btnTtsList);
            buttonPanel.Controls.Add(btnBack);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPanel.Location = new System.Drawing.Point(3, 619);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(993, 52);
            buttonPanel.TabIndex = 2;
            // 
            // btnSendReview
            // 
            btnSendReview.Location = new System.Drawing.Point(3, 3);
            btnSendReview.Name = "btnSendReview";
            btnSendReview.Size = new System.Drawing.Size(260, 38);
            btnSendReview.TabIndex = 0;
            btnSendReview.Text = "Send Need Review Words";
            btnSendReview.UseVisualStyleBackColor = true;
            // 
            // btnHumanReview
            // 
            btnHumanReview.Location = new System.Drawing.Point(269, 3);
            btnHumanReview.Name = "btnHumanReview";
            btnHumanReview.Size = new System.Drawing.Size(150, 34);
            btnHumanReview.TabIndex = 1;
            btnHumanReview.Text = "Human Review";
            btnHumanReview.UseVisualStyleBackColor = true;
            // 
            // btnTtsList
            // 
            btnTtsList.Location = new System.Drawing.Point(425, 3);
            btnTtsList.Name = "btnTtsList";
            btnTtsList.Size = new System.Drawing.Size(150, 34);
            btnTtsList.TabIndex = 2;
            btnTtsList.Text = "Go to TTS List";
            btnTtsList.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new System.Drawing.Point(581, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(130, 34);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back to Script";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // ExtractionResultScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "ExtractionResultScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)termsGrid).EndInit();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
