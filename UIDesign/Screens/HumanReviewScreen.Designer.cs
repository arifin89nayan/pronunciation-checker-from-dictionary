namespace WindowsFormsApp1.UIDesign.Screens
{
    partial class HumanReviewScreen
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
            this.lblSentenceHdr = new System.Windows.Forms.Label();
            this.txtSentence = new System.Windows.Forms.TextBox();
            this.lblWord = new System.Windows.Forms.Label();
            this.txtWord = new System.Windows.Forms.TextBox();
            this.lblApi = new System.Windows.Forms.Label();
            this.txtApi = new System.Windows.Forms.TextBox();
            this.lblCorrect = new System.Windows.Forms.Label();
            this.txtCorrect = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblSaveType = new System.Windows.Forms.Label();
            this.cmbSaveType = new System.Windows.Forms.ComboBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnEditApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.lblQueueHdr = new System.Windows.Forms.Label();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Location = new System.Drawing.Point(16, 12);
            this.lblHeader.Text = "Screen 4 — Human Review (Confirmation List)";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblHeader.AutoSize = true;
            // lblCaption
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Location = new System.Drawing.Point(18, 44);
            this.lblCaption.Text = "Approve every uncertain reading. Approved words are saved to the Fixed List.";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblCaption.AutoSize = true;
            // lblSentenceHdr
            this.lblSentenceHdr.Name = "lblSentenceHdr";
            this.lblSentenceHdr.Location = new System.Drawing.Point(20, 80);
            this.lblSentenceHdr.Text = "Source Sentence";
            this.lblSentenceHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSentenceHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblSentenceHdr.AutoSize = true;
            // txtSentence
            this.txtSentence.Name = "txtSentence";
            this.txtSentence.Location = new System.Drawing.Point(20, 104);
            this.txtSentence.Size = new System.Drawing.Size(760, 50);
            this.txtSentence.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.txtSentence.Multiline = true;
            this.txtSentence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSentence.ReadOnly = true;
            // lblWord
            this.lblWord.Name = "lblWord";
            this.lblWord.Location = new System.Drawing.Point(20, 168);
            this.lblWord.Text = "Word:";
            this.lblWord.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblWord.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblWord.AutoSize = true;
            // txtWord
            this.txtWord.Name = "txtWord";
            this.txtWord.Location = new System.Drawing.Point(160, 165);
            this.txtWord.Size = new System.Drawing.Size(260, 23);
            this.txtWord.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.txtWord.ReadOnly = true;
            // lblApi
            this.lblApi.Name = "lblApi";
            this.lblApi.Location = new System.Drawing.Point(20, 200);
            this.lblApi.Text = "API Hiragana:";
            this.lblApi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblApi.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblApi.AutoSize = true;
            // txtApi
            this.txtApi.Name = "txtApi";
            this.txtApi.Location = new System.Drawing.Point(160, 197);
            this.txtApi.Size = new System.Drawing.Size(260, 23);
            this.txtApi.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.txtApi.ReadOnly = true;
            // lblCorrect
            this.lblCorrect.Name = "lblCorrect";
            this.lblCorrect.Location = new System.Drawing.Point(20, 232);
            this.lblCorrect.Text = "Correct Hiragana:";
            this.lblCorrect.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblCorrect.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblCorrect.AutoSize = true;
            // txtCorrect
            this.txtCorrect.Name = "txtCorrect";
            this.txtCorrect.Location = new System.Drawing.Point(160, 229);
            this.txtCorrect.Size = new System.Drawing.Size(260, 23);
            this.txtCorrect.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            // lblCategory
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Location = new System.Drawing.Point(20, 264);
            this.lblCategory.Text = "Category:";
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblCategory.AutoSize = true;
            // cmbCategory
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Location = new System.Drawing.Point(160, 261);
            this.cmbCategory.Size = new System.Drawing.Size(180, 25);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // lblSaveType
            this.lblSaveType.Name = "lblSaveType";
            this.lblSaveType.Location = new System.Drawing.Point(360, 264);
            this.lblSaveType.Text = "Save Type:";
            this.lblSaveType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblSaveType.ForeColor = System.Drawing.Color.FromArgb(35, 38, 43);
            this.lblSaveType.AutoSize = true;
            // cmbSaveType
            this.cmbSaveType.Name = "cmbSaveType";
            this.cmbSaveType.Location = new System.Drawing.Point(450, 261);
            this.cmbSaveType.Size = new System.Drawing.Size(150, 25);
            this.cmbSaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaveType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            // btnPreview
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Location = new System.Drawing.Point(20, 300);
            this.btnPreview.Size = new System.Drawing.Size(110, 32);
            this.btnPreview.Text = "Play Preview";
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnPreview.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnPreview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // btnApprove
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Location = new System.Drawing.Point(140, 300);
            this.btnApprove.Size = new System.Drawing.Size(100, 32);
            this.btnApprove.Text = "Approve";
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // btnEditApprove
            this.btnEditApprove.Name = "btnEditApprove";
            this.btnEditApprove.Location = new System.Drawing.Point(250, 300);
            this.btnEditApprove.Size = new System.Drawing.Size(130, 32);
            this.btnEditApprove.Text = "Edit && Approve";
            this.btnEditApprove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditApprove.BackColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnEditApprove.ForeColor = System.Drawing.Color.White;
            this.btnEditApprove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(199, 62, 58);
            this.btnEditApprove.UseVisualStyleBackColor = false;
            this.btnEditApprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditApprove.Click += new System.EventHandler(this.btnEditApprove_Click);
            // btnReject
            this.btnReject.Name = "btnReject";
            this.btnReject.Location = new System.Drawing.Point(390, 300);
            this.btnReject.Size = new System.Drawing.Size(90, 32);
            this.btnReject.Text = "Reject";
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.btnReject.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.btnReject.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(201, 212, 232);
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // lblQueueHdr
            this.lblQueueHdr.Name = "lblQueueHdr";
            this.lblQueueHdr.Location = new System.Drawing.Point(20, 344);
            this.lblQueueHdr.Text = "Review Queue";
            this.lblQueueHdr.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblQueueHdr.ForeColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.lblQueueHdr.AutoSize = true;
            // dgvQueue
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.Location = new System.Drawing.Point(20, 368);
            this.dgvQueue.Size = new System.Drawing.Size(820, 200);
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.BackgroundColor = System.Drawing.Color.White;
            this.dgvQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Font = new System.Drawing.Font("Segoe UI", 9F);
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new System.Drawing.Point(20, 576);
            this.lblStatus.Text = "";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.AutoSize = true;
            // HumanReviewScreen
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblSentenceHdr);
            this.Controls.Add(this.txtSentence);
            this.Controls.Add(this.lblWord);
            this.Controls.Add(this.txtWord);
            this.Controls.Add(this.lblApi);
            this.Controls.Add(this.txtApi);
            this.Controls.Add(this.lblCorrect);
            this.Controls.Add(this.txtCorrect);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblSaveType);
            this.Controls.Add(this.cmbSaveType);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.btnEditApprove);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.lblQueueHdr);
            this.Controls.Add(this.dgvQueue);
            this.Controls.Add(this.lblStatus);
            this.Name = "HumanReviewScreen";
            this.Size = new System.Drawing.Size(880, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblSentenceHdr;
        private System.Windows.Forms.TextBox txtSentence;
        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.Label lblApi;
        private System.Windows.Forms.TextBox txtApi;
        private System.Windows.Forms.Label lblCorrect;
        private System.Windows.Forms.TextBox txtCorrect;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblSaveType;
        private System.Windows.Forms.ComboBox cmbSaveType;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnEditApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Label lblQueueHdr;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.Label lblStatus;
    }
}