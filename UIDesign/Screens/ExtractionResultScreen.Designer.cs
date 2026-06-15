namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class ExtractionResultScreen
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
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.dgvTerms = new System.Windows.Forms.DataGridView();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnGoReview = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerms)).BeginInit();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 3 — Extraction Result";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "Every extracted term with its status. Send review-needed words on.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblFilter
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Location = new System.Drawing.Point(20, 84);
            this.lblFilter.Text = "Filter:";
            this.lblFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblFilter.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblFilter.AutoSize = true;
            // cmbFilter
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Location = new System.Drawing.Point(70, 81);
            this.cmbFilter.Size = new System.Drawing.Size(130, 25);
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // dgvTerms
            this.dgvTerms.Name = "dgvTerms";
            this.dgvTerms.Location = new System.Drawing.Point(20, 116);
            this.dgvTerms.Size = new System.Drawing.Size(820, 360);
            this.dgvTerms.ReadOnly = true;
            this.dgvTerms.AllowUserToAddRows = false;
            this.dgvTerms.RowHeadersVisible = false;
            this.dgvTerms.BackgroundColor = System.Drawing.Color.White;
            this.dgvTerms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTerms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTerms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTerms.Font = new System.Drawing.Font("Segoe UI", 9F);
            // btnSend
            this.btnSend.Name = "btnSend";
            this.btnSend.Location = new System.Drawing.Point(20, 488);
            this.btnSend.Size = new System.Drawing.Size(320, 34);
            this.btnSend.Text = "Send Need Review Words to Confirmation List";
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // btnGoReview
            this.btnGoReview.Name = "btnGoReview";
            this.btnGoReview.Location = new System.Drawing.Point(350, 488);
            this.btnGoReview.Size = new System.Drawing.Size(160, 34);
            this.btnGoReview.Text = "Go to Human Review";
            this.btnGoReview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGoReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoReview.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnGoReview.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnGoReview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnGoReview.UseVisualStyleBackColor = false;
            this.btnGoReview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoReview.Click += new System.EventHandler(this.btnGoReview_Click);
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 530);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // ExtractionResultScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.dgvTerms);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnGoReview);
            this.Controls.Add(this.lblStatus);
            this.Name = "ExtractionResultScreen";
            this.Size = new System.Drawing.Size(880, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.DataGridView dgvTerms;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnGoReview;
        private System.Windows.Forms.Label lblStatus;
    }
}