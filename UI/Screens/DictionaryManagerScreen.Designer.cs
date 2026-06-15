namespace TTSAgent.UI.Screens
{
    partial class DictionaryManagerScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.FlowLayoutPanel topButtonPanel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnExportPls;
        private System.Windows.Forms.DataGridView dictionaryGrid;
        private System.Windows.Forms.GroupBox editGroupBox;
        private System.Windows.Forms.TableLayoutPanel editLayout;
        private System.Windows.Forms.Label wordLabel;
        private System.Windows.Forms.TextBox wordTextBox;
        private System.Windows.Forms.Label readingLabel;
        private System.Windows.Forms.TextBox readingTextBox;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label pathLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            topButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnImport = new System.Windows.Forms.Button();
            btnSaveAs = new System.Windows.Forms.Button();
            btnBackup = new System.Windows.Forms.Button();
            btnExportPls = new System.Windows.Forms.Button();
            dictionaryGrid = new System.Windows.Forms.DataGridView();
            editGroupBox = new System.Windows.Forms.GroupBox();
            editLayout = new System.Windows.Forms.TableLayoutPanel();
            wordLabel = new System.Windows.Forms.Label();
            wordTextBox = new System.Windows.Forms.TextBox();
            readingLabel = new System.Windows.Forms.Label();
            readingTextBox = new System.Windows.Forms.TextBox();
            categoryLabel = new System.Windows.Forms.Label();
            categoryTextBox = new System.Windows.Forms.TextBox();
            btnAddUpdate = new System.Windows.Forms.Button();
            btnRemove = new System.Windows.Forms.Button();
            pathLabel = new System.Windows.Forms.Label();
            rootLayout.SuspendLayout();
            topButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dictionaryGrid).BeginInit();
            editGroupBox.SuspendLayout();
            editLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(topButtonPanel, 0, 0);
            rootLayout.Controls.Add(dictionaryGrid, 0, 1);
            rootLayout.Controls.Add(editGroupBox, 0, 2);
            rootLayout.Controls.Add(pathLabel, 0, 3);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // topButtonPanel
            // 
            topButtonPanel.Controls.Add(btnImport);
            topButtonPanel.Controls.Add(btnSaveAs);
            topButtonPanel.Controls.Add(btnBackup);
            topButtonPanel.Controls.Add(btnExportPls);
            topButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            topButtonPanel.Location = new System.Drawing.Point(3, 3);
            topButtonPanel.Name = "topButtonPanel";
            topButtonPanel.Size = new System.Drawing.Size(993, 48);
            topButtonPanel.TabIndex = 0;
            // 
            // btnImport
            // 
            btnImport.Location = new System.Drawing.Point(3, 3);
            btnImport.Name = "btnImport";
            btnImport.Size = new System.Drawing.Size(130, 34);
            btnImport.TabIndex = 0;
            btnImport.Text = "Import CSV";
            btnImport.UseVisualStyleBackColor = true;
            // 
            // btnSaveAs
            // 
            btnSaveAs.Location = new System.Drawing.Point(139, 3);
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new System.Drawing.Size(130, 34);
            btnSaveAs.TabIndex = 1;
            btnSaveAs.Text = "Save CSV As";
            btnSaveAs.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            btnBackup.Location = new System.Drawing.Point(275, 3);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new System.Drawing.Size(130, 34);
            btnBackup.TabIndex = 2;
            btnBackup.Text = "Backup";
            btnBackup.UseVisualStyleBackColor = true;
            // 
            // btnExportPls
            // 
            btnExportPls.Location = new System.Drawing.Point(411, 3);
            btnExportPls.Name = "btnExportPls";
            btnExportPls.Size = new System.Drawing.Size(140, 34);
            btnExportPls.TabIndex = 3;
            btnExportPls.Text = "Export PLS XML";
            btnExportPls.UseVisualStyleBackColor = true;
            // 
            // dictionaryGrid
            // 
            dictionaryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dictionaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            dictionaryGrid.Location = new System.Drawing.Point(3, 57);
            dictionaryGrid.Name = "dictionaryGrid";
            dictionaryGrid.ReadOnly = true;
            dictionaryGrid.Size = new System.Drawing.Size(993, 448);
            dictionaryGrid.TabIndex = 1;
            // 
            // editGroupBox
            // 
            editGroupBox.Controls.Add(editLayout);
            editGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            editGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            editGroupBox.Location = new System.Drawing.Point(3, 511);
            editGroupBox.Name = "editGroupBox";
            editGroupBox.Size = new System.Drawing.Size(993, 132);
            editGroupBox.TabIndex = 2;
            editGroupBox.TabStop = false;
            editGroupBox.Text = "Add / Update Entry";
            // 
            // editLayout
            // 
            editLayout.ColumnCount = 8;
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            editLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            editLayout.Controls.Add(wordLabel, 0, 0);
            editLayout.Controls.Add(wordTextBox, 1, 0);
            editLayout.Controls.Add(readingLabel, 2, 0);
            editLayout.Controls.Add(readingTextBox, 3, 0);
            editLayout.Controls.Add(categoryLabel, 4, 0);
            editLayout.Controls.Add(categoryTextBox, 5, 0);
            editLayout.Controls.Add(btnAddUpdate, 6, 0);
            editLayout.Controls.Add(btnRemove, 7, 0);
            editLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            editLayout.Location = new System.Drawing.Point(3, 21);
            editLayout.Name = "editLayout";
            editLayout.Padding = new System.Windows.Forms.Padding(8);
            editLayout.RowCount = 1;
            editLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            editLayout.Size = new System.Drawing.Size(987, 108);
            editLayout.TabIndex = 0;
            // 
            // wordLabel
            // 
            wordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            wordLabel.AutoSize = true;
            wordLabel.Location = new System.Drawing.Point(11, 44);
            wordLabel.Name = "wordLabel";
            wordLabel.Size = new System.Drawing.Size(45, 19);
            wordLabel.TabIndex = 0;
            wordLabel.Text = "Word";
            // 
            // wordTextBox
            // 
            wordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            wordTextBox.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            wordTextBox.Location = new System.Drawing.Point(81, 41);
            wordTextBox.Name = "wordTextBox";
            wordTextBox.Size = new System.Drawing.Size(168, 25);
            wordTextBox.TabIndex = 1;
            // 
            // readingLabel
            // 
            readingLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            readingLabel.AutoSize = true;
            readingLabel.Location = new System.Drawing.Point(255, 44);
            readingLabel.Name = "readingLabel";
            readingLabel.Size = new System.Drawing.Size(62, 19);
            readingLabel.TabIndex = 2;
            readingLabel.Text = "Reading";
            // 
            // readingTextBox
            // 
            readingTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            readingTextBox.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            readingTextBox.Location = new System.Drawing.Point(335, 41);
            readingTextBox.Name = "readingTextBox";
            readingTextBox.Size = new System.Drawing.Size(168, 25);
            readingTextBox.TabIndex = 3;
            // 
            // categoryLabel
            // 
            categoryLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            categoryLabel.AutoSize = true;
            categoryLabel.Location = new System.Drawing.Point(509, 44);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new System.Drawing.Size(70, 19);
            categoryLabel.TabIndex = 4;
            categoryLabel.Text = "Category";
            // 
            // categoryTextBox
            // 
            categoryTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            categoryTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            categoryTextBox.Location = new System.Drawing.Point(589, 41);
            categoryTextBox.Name = "categoryTextBox";
            categoryTextBox.Size = new System.Drawing.Size(174, 25);
            categoryTextBox.TabIndex = 5;
            // 
            // btnAddUpdate
            // 
            btnAddUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnAddUpdate.Location = new System.Drawing.Point(769, 35);
            btnAddUpdate.Name = "btnAddUpdate";
            btnAddUpdate.Size = new System.Drawing.Size(110, 38);
            btnAddUpdate.TabIndex = 6;
            btnAddUpdate.Text = "Add / Update";
            btnAddUpdate.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnRemove.Location = new System.Drawing.Point(889, 37);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(85, 34);
            btnRemove.TabIndex = 7;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // pathLabel
            // 
            pathLabel.AutoSize = true;
            pathLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            pathLabel.Location = new System.Drawing.Point(3, 646);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new System.Drawing.Size(90, 15);
            pathLabel.TabIndex = 3;
            pathLabel.Text = "No CSV loaded.";
            // 
            // DictionaryManagerScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "DictionaryManagerScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            topButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dictionaryGrid).EndInit();
            editGroupBox.ResumeLayout(false);
            editLayout.ResumeLayout(false);
            editLayout.PerformLayout();
            ResumeLayout(false);
        }
    }
}
