namespace WindowsFormsApp1
{
    partial class ContentDataForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAiContent = new System.Windows.Forms.Button();
            this.cmbContentFilter = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDetectedRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewContent = new System.Windows.Forms.DataGridView();
            this.ExitBnt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAiContent
            // 
            this.btnAiContent.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAiContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAiContent.Location = new System.Drawing.Point(1513, 97);
            this.btnAiContent.Name = "btnAiContent";
            this.btnAiContent.Size = new System.Drawing.Size(311, 57);
            this.btnAiContent.TabIndex = 0;
            this.btnAiContent.Text = "AI Generate";
            this.btnAiContent.UseVisualStyleBackColor = true;
            this.btnAiContent.Click += new System.EventHandler(this.btnAiContent_Click);
            // 
            // cmbContentFilter
            // 
            this.cmbContentFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContentFilter.FormattingEnabled = true;
            this.cmbContentFilter.Location = new System.Drawing.Point(357, 97);
            this.cmbContentFilter.Name = "cmbContentFilter";
            this.cmbContentFilter.Size = new System.Drawing.Size(192, 26);
            this.cmbContentFilter.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnAiContent);
            this.panel1.Controls.Add(this.lblDetectedRecords);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbContentFilter);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1912, 195);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(52, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(290, 36);
            this.label3.TabIndex = 4;
            this.label3.Text = "Content Data     ";
            // 
            // lblDetectedRecords
            // 
            this.lblDetectedRecords.AutoSize = true;
            this.lblDetectedRecords.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDetectedRecords.Location = new System.Drawing.Point(777, 97);
            this.lblDetectedRecords.Name = "lblDetectedRecords";
            this.lblDetectedRecords.Size = new System.Drawing.Size(314, 36);
            this.lblDetectedRecords.TabIndex = 3;
            this.lblDetectedRecords.Text = "Detected Records:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(39, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Content Type :";
            // 
            // dataGridViewContent
            // 
            this.dataGridViewContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContent.Location = new System.Drawing.Point(12, 253);
            this.dataGridViewContent.Name = "dataGridViewContent";
            this.dataGridViewContent.RowHeadersWidth = 62;
            this.dataGridViewContent.RowTemplate.Height = 27;
            this.dataGridViewContent.Size = new System.Drawing.Size(1912, 750);
            this.dataGridViewContent.TabIndex = 3;
            // 
            // ExitBnt
            // 
            this.ExitBnt.BackColor = System.Drawing.Color.Red;
            this.ExitBnt.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExitBnt.ForeColor = System.Drawing.Color.White;
            this.ExitBnt.Location = new System.Drawing.Point(1701, 1162);
            this.ExitBnt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ExitBnt.Name = "ExitBnt";
            this.ExitBnt.Size = new System.Drawing.Size(196, 71);
            this.ExitBnt.TabIndex = 14;
            this.ExitBnt.Text = "Exit";
            this.ExitBnt.UseVisualStyleBackColor = false;
            this.ExitBnt.Click += new System.EventHandler(this.ExitBnt_Click);
            // 
            // ContentDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1936, 1269);
            this.Controls.Add(this.ExitBnt);
            this.Controls.Add(this.dataGridViewContent);
            this.Controls.Add(this.panel1);
            this.Name = "ContentDataForm";
            this.Text = "ContentDataForm";
            this.Load += new System.EventHandler(this.ContentDataForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAiContent;
        private System.Windows.Forms.ComboBox cmbContentFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDetectedRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewContent;
        private System.Windows.Forms.Button ExitBnt;
    }
}