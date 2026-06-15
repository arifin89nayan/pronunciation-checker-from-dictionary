namespace TTSAgent.UI.Screens
{
    partial class HumanReviewScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView reviewGrid;
        private System.Windows.Forms.TableLayoutPanel detailLayout;
        private System.Windows.Forms.Label wordLabel;
        private System.Windows.Forms.TextBox wordTextBox;
        private System.Windows.Forms.Label apiLabel;
        private System.Windows.Forms.TextBox apiReadingTextBox;
        private System.Windows.Forms.Label correctLabel;
        private System.Windows.Forms.TextBox correctReadingTextBox;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label saveTypeLabel;
        private System.Windows.Forms.ComboBox saveTypeComboBox;
        private System.Windows.Forms.Label sentenceLabel;
        private System.Windows.Forms.TextBox sentenceTextBox;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnDictionary;
        private System.Windows.Forms.Button btnTtsList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitContainer = new System.Windows.Forms.SplitContainer();
            reviewGrid = new System.Windows.Forms.DataGridView();
            detailLayout = new System.Windows.Forms.TableLayoutPanel();
            wordLabel = new System.Windows.Forms.Label();
            wordTextBox = new System.Windows.Forms.TextBox();
            apiLabel = new System.Windows.Forms.Label();
            apiReadingTextBox = new System.Windows.Forms.TextBox();
            correctLabel = new System.Windows.Forms.Label();
            correctReadingTextBox = new System.Windows.Forms.TextBox();
            categoryLabel = new System.Windows.Forms.Label();
            categoryComboBox = new System.Windows.Forms.ComboBox();
            saveTypeLabel = new System.Windows.Forms.Label();
            saveTypeComboBox = new System.Windows.Forms.ComboBox();
            sentenceLabel = new System.Windows.Forms.Label();
            sentenceTextBox = new System.Windows.Forms.TextBox();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnApprove = new System.Windows.Forms.Button();
            btnReject = new System.Windows.Forms.Button();
            btnExport = new System.Windows.Forms.Button();
            btnDictionary = new System.Windows.Forms.Button();
            btnTtsList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)reviewGrid).BeginInit();
            detailLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(reviewGrid);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(detailLayout);
            splitContainer.Size = new System.Drawing.Size(999, 674);
            splitContainer.SplitterDistance = 530;
            splitContainer.TabIndex = 0;
            // 
            // reviewGrid
            // 
            reviewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            reviewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            reviewGrid.Location = new System.Drawing.Point(0, 0);
            reviewGrid.Name = "reviewGrid";
            reviewGrid.ReadOnly = true;
            reviewGrid.Size = new System.Drawing.Size(530, 674);
            reviewGrid.TabIndex = 0;
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 2;
            detailLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            detailLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            detailLayout.Controls.Add(wordLabel, 0, 0);
            detailLayout.Controls.Add(wordTextBox, 1, 0);
            detailLayout.Controls.Add(apiLabel, 0, 1);
            detailLayout.Controls.Add(apiReadingTextBox, 1, 1);
            detailLayout.Controls.Add(correctLabel, 0, 2);
            detailLayout.Controls.Add(correctReadingTextBox, 1, 2);
            detailLayout.Controls.Add(categoryLabel, 0, 3);
            detailLayout.Controls.Add(categoryComboBox, 1, 3);
            detailLayout.Controls.Add(saveTypeLabel, 0, 4);
            detailLayout.Controls.Add(saveTypeComboBox, 1, 4);
            detailLayout.Controls.Add(sentenceLabel, 0, 5);
            detailLayout.Controls.Add(sentenceTextBox, 1, 5);
            detailLayout.Controls.Add(buttonPanel, 0, 6);
            detailLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            detailLayout.Location = new System.Drawing.Point(0, 0);
            detailLayout.Name = "detailLayout";
            detailLayout.Padding = new System.Windows.Forms.Padding(12);
            detailLayout.RowCount = 7;
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            detailLayout.Size = new System.Drawing.Size(465, 674);
            detailLayout.TabIndex = 0;
            // 
            // wordLabel
            // 
            wordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            wordLabel.AutoSize = true;
            wordLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            wordLabel.Location = new System.Drawing.Point(15, 26);
            wordLabel.Name = "wordLabel";
            wordLabel.Size = new System.Drawing.Size(45, 19);
            wordLabel.TabIndex = 0;
            wordLabel.Text = "Word";
            // 
            // wordTextBox
            // 
            wordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            wordTextBox.Location = new System.Drawing.Point(135, 24);
            wordTextBox.Name = "wordTextBox";
            wordTextBox.ReadOnly = true;
            wordTextBox.Size = new System.Drawing.Size(315, 23);
            wordTextBox.TabIndex = 1;
            // 
            // apiLabel
            // 
            apiLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            apiLabel.AutoSize = true;
            apiLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            apiLabel.Location = new System.Drawing.Point(15, 74);
            apiLabel.Name = "apiLabel";
            apiLabel.Size = new System.Drawing.Size(91, 19);
            apiLabel.TabIndex = 2;
            apiLabel.Text = "API Reading";
            // 
            // apiReadingTextBox
            // 
            apiReadingTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            apiReadingTextBox.Location = new System.Drawing.Point(135, 72);
            apiReadingTextBox.Name = "apiReadingTextBox";
            apiReadingTextBox.ReadOnly = true;
            apiReadingTextBox.Size = new System.Drawing.Size(315, 23);
            apiReadingTextBox.TabIndex = 3;
            // 
            // correctLabel
            // 
            correctLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            correctLabel.AutoSize = true;
            correctLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            correctLabel.Location = new System.Drawing.Point(15, 122);
            correctLabel.Name = "correctLabel";
            correctLabel.Size = new System.Drawing.Size(114, 19);
            correctLabel.TabIndex = 4;
            correctLabel.Text = "Correct Reading";
            // 
            // correctReadingTextBox
            // 
            correctReadingTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            correctReadingTextBox.Location = new System.Drawing.Point(135, 120);
            correctReadingTextBox.Name = "correctReadingTextBox";
            correctReadingTextBox.Size = new System.Drawing.Size(315, 23);
            correctReadingTextBox.TabIndex = 5;
            // 
            // categoryLabel
            // 
            categoryLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            categoryLabel.AutoSize = true;
            categoryLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            categoryLabel.Location = new System.Drawing.Point(15, 170);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new System.Drawing.Size(70, 19);
            categoryLabel.TabIndex = 6;
            categoryLabel.Text = "Category";
            // 
            // categoryComboBox
            // 
            categoryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Items.AddRange(new object[] { "fixed_dictionary", "place_name", "shrine_name", "museum_name", "cultural_term", "historical_term", "technical_term", "general_word", "unknown" });
            categoryComboBox.Location = new System.Drawing.Point(135, 168);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new System.Drawing.Size(315, 23);
            categoryComboBox.TabIndex = 7;
            // 
            // saveTypeLabel
            // 
            saveTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            saveTypeLabel.AutoSize = true;
            saveTypeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            saveTypeLabel.Location = new System.Drawing.Point(15, 218);
            saveTypeLabel.Name = "saveTypeLabel";
            saveTypeLabel.Size = new System.Drawing.Size(74, 19);
            saveTypeLabel.TabIndex = 8;
            saveTypeLabel.Text = "Save Type";
            // 
            // saveTypeComboBox
            // 
            saveTypeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            saveTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            saveTypeComboBox.FormattingEnabled = true;
            saveTypeComboBox.Items.AddRange(new object[] { "Fixed List", "General Only", "Skip" });
            saveTypeComboBox.Location = new System.Drawing.Point(135, 216);
            saveTypeComboBox.Name = "saveTypeComboBox";
            saveTypeComboBox.Size = new System.Drawing.Size(315, 23);
            saveTypeComboBox.TabIndex = 9;
            // 
            // sentenceLabel
            // 
            sentenceLabel.AutoSize = true;
            sentenceLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            sentenceLabel.Location = new System.Drawing.Point(15, 252);
            sentenceLabel.Name = "sentenceLabel";
            sentenceLabel.Size = new System.Drawing.Size(69, 19);
            sentenceLabel.TabIndex = 10;
            sentenceLabel.Text = "Sentence";
            // 
            // sentenceTextBox
            // 
            sentenceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            sentenceTextBox.Location = new System.Drawing.Point(135, 255);
            sentenceTextBox.Multiline = true;
            sentenceTextBox.Name = "sentenceTextBox";
            sentenceTextBox.ReadOnly = true;
            sentenceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            sentenceTextBox.Size = new System.Drawing.Size(315, 294);
            sentenceTextBox.TabIndex = 11;
            // 
            // buttonPanel
            // 
            detailLayout.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(btnApprove);
            buttonPanel.Controls.Add(btnReject);
            buttonPanel.Controls.Add(btnExport);
            buttonPanel.Controls.Add(btnDictionary);
            buttonPanel.Controls.Add(btnTtsList);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPanel.Location = new System.Drawing.Point(15, 555);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(435, 104);
            buttonPanel.TabIndex = 12;
            // 
            // btnApprove
            // 
            btnApprove.Location = new System.Drawing.Point(3, 3);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new System.Drawing.Size(130, 38);
            btnApprove.TabIndex = 0;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = true;
            // 
            // btnReject
            // 
            btnReject.Location = new System.Drawing.Point(139, 3);
            btnReject.Name = "btnReject";
            btnReject.Size = new System.Drawing.Size(110, 34);
            btnReject.TabIndex = 1;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new System.Drawing.Point(255, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(120, 34);
            btnExport.TabIndex = 2;
            btnExport.Text = "Export Queue";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnDictionary
            // 
            btnDictionary.Location = new System.Drawing.Point(3, 47);
            btnDictionary.Name = "btnDictionary";
            btnDictionary.Size = new System.Drawing.Size(130, 34);
            btnDictionary.TabIndex = 3;
            btnDictionary.Text = "Dictionary";
            btnDictionary.UseVisualStyleBackColor = true;
            // 
            // btnTtsList
            // 
            btnTtsList.Location = new System.Drawing.Point(139, 47);
            btnTtsList.Name = "btnTtsList";
            btnTtsList.Size = new System.Drawing.Size(130, 34);
            btnTtsList.TabIndex = 4;
            btnTtsList.Text = "TTS List";
            btnTtsList.UseVisualStyleBackColor = true;
            // 
            // HumanReviewScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer);
            Name = "HumanReviewScreen";
            Size = new System.Drawing.Size(999, 674);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)reviewGrid).EndInit();
            detailLayout.ResumeLayout(false);
            detailLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
