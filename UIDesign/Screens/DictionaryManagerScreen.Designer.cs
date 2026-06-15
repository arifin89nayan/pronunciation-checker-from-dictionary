namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class DictionaryManagerScreen
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
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.dgvDict = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDict)).BeginInit();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 5 — Dictionary Manager (Fixed List)";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "The permanent source of truth. Highest priority over AI readings.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblSearch
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Location = new System.Drawing.Point(20, 84);
            this.lblSearch.Text = "Search:";
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblSearch.AutoSize = true;
            // txtSearch
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Location = new System.Drawing.Point(80, 81);
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            // lblCategory
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Location = new System.Drawing.Point(400, 84);
            this.lblCategory.Text = "Category:";
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblCategory.AutoSize = true;
            // cmbCategory
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Location = new System.Drawing.Point(470, 81);
            this.cmbCategory.Size = new System.Drawing.Size(130, 25);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // dgvDict
            this.dgvDict.Name = "dgvDict";
            this.dgvDict.Location = new System.Drawing.Point(20, 116);
            this.dgvDict.Size = new System.Drawing.Size(820, 360);
            this.dgvDict.ReadOnly = true;
            this.dgvDict.AllowUserToAddRows = false;
            this.dgvDict.RowHeadersVisible = false;
            this.dgvDict.BackgroundColor = System.Drawing.Color.White;
            this.dgvDict.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDict.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDict.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDict.Font = new System.Drawing.Font("Segoe UI", 9F);
            // btnAdd
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Location = new System.Drawing.Point(20, 488);
            this.btnAdd.Size = new System.Drawing.Size(100, 32);
            this.btnAdd.Text = "Add New";
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // btnEdit
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Location = new System.Drawing.Point(128, 488);
            this.btnEdit.Size = new System.Drawing.Size(100, 32);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // btnDelete
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Location = new System.Drawing.Point(236, 488);
            this.btnDelete.Size = new System.Drawing.Size(100, 32);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // btnImport
            this.btnImport.Name = "btnImport";
            this.btnImport.Location = new System.Drawing.Point(344, 488);
            this.btnImport.Size = new System.Drawing.Size(100, 32);
            this.btnImport.Text = "Import CSV";
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // btnExport
            this.btnExport.Name = "btnExport";
            this.btnExport.Location = new System.Drawing.Point(452, 488);
            this.btnExport.Size = new System.Drawing.Size(100, 32);
            this.btnExport.Text = "Export XML";
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // btnBackup
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Location = new System.Drawing.Point(560, 488);
            this.btnBackup.Size = new System.Drawing.Size(100, 32);
            this.btnBackup.Text = "Backup";
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnBackup.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 530);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // DictionaryManagerScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.dgvDict);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.lblStatus);
            this.Name = "DictionaryManagerScreen";
            this.Size = new System.Drawing.Size(880, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDict)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.DataGridView dgvDict;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblStatus;
    }
}