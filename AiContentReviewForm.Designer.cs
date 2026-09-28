namespace WindowsFormsApp1
{
    partial class AiContentReviewForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTopic = new System.Windows.Forms.Label();
            this.lblContentCounter = new System.Windows.Forms.Label();
            this.lblSourceTitle = new System.Windows.Forms.Label();
            this.lblGeneratedTitle = new System.Windows.Forms.Label();
            this.txtSourceText = new System.Windows.Forms.RichTextBox();
            this.txtGeneratedText = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRegenerate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTopic);
            this.pnlHeader.Controls.Add(this.lblContentCounter);
            this.pnlHeader.Location = new System.Drawing.Point(12, 12);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(2124, 124);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTopic
            // 
            this.lblTopic.AutoSize = true;
            this.lblTopic.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTopic.Location = new System.Drawing.Point(1569, 27);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(108, 36);
            this.lblTopic.TabIndex = 1;
            this.lblTopic.Text = "Title :";
            // 
            // lblContentCounter
            // 
            this.lblContentCounter.AutoSize = true;
            this.lblContentCounter.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblContentCounter.Location = new System.Drawing.Point(19, 27);
            this.lblContentCounter.Name = "lblContentCounter";
            this.lblContentCounter.Size = new System.Drawing.Size(164, 36);
            this.lblContentCounter.TabIndex = 0;
            this.lblContentCounter.Text = "Content :";
            // 
            // lblSourceTitle
            // 
            this.lblSourceTitle.AutoSize = true;
            this.lblSourceTitle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSourceTitle.Location = new System.Drawing.Point(84, 230);
            this.lblSourceTitle.Name = "lblSourceTitle";
            this.lblSourceTitle.Size = new System.Drawing.Size(213, 36);
            this.lblSourceTitle.TabIndex = 1;
            this.lblSourceTitle.Text = "Source Text";
            // 
            // lblGeneratedTitle
            // 
            this.lblGeneratedTitle.AutoSize = true;
            this.lblGeneratedTitle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGeneratedTitle.Location = new System.Drawing.Point(1135, 249);
            this.lblGeneratedTitle.Name = "lblGeneratedTitle";
            this.lblGeneratedTitle.Size = new System.Drawing.Size(266, 36);
            this.lblGeneratedTitle.TabIndex = 2;
            this.lblGeneratedTitle.Text = "Generated Text";
            // 
            // txtSourceText
            // 
            this.txtSourceText.Location = new System.Drawing.Point(90, 320);
            this.txtSourceText.Name = "txtSourceText";
            this.txtSourceText.Size = new System.Drawing.Size(865, 434);
            this.txtSourceText.TabIndex = 3;
            this.txtSourceText.Text = "";
            // 
            // txtGeneratedText
            // 
            this.txtGeneratedText.Location = new System.Drawing.Point(1141, 320);
            this.txtGeneratedText.Name = "txtGeneratedText";
            this.txtGeneratedText.Size = new System.Drawing.Size(865, 434);
            this.txtGeneratedText.TabIndex = 4;
            this.txtGeneratedText.Text = "";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblStatus.Location = new System.Drawing.Point(84, 815);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(141, 36);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status :";
            // 
            // btnRegenerate
            // 
            this.btnRegenerate.Location = new System.Drawing.Point(700, 981);
            this.btnRegenerate.Name = "btnRegenerate";
            this.btnRegenerate.Size = new System.Drawing.Size(165, 68);
            this.btnRegenerate.TabIndex = 6;
            this.btnRegenerate.Text = "Regenerate";
            this.btnRegenerate.UseVisualStyleBackColor = true;
            this.btnRegenerate.Click += new System.EventHandler(this.btnRegenerate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(969, 981);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(117, 62);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(1206, 975);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(157, 68);
            this.btnApprove.TabIndex = 8;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(1443, 975);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(128, 66);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // AiContentReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2148, 1269);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRegenerate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtGeneratedText);
            this.Controls.Add(this.txtSourceText);
            this.Controls.Add(this.lblGeneratedTitle);
            this.Controls.Add(this.lblSourceTitle);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AiContentReviewForm";
            this.Text = "AiContentReviewForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lblContentCounter;
        private System.Windows.Forms.Label lblSourceTitle;
        private System.Windows.Forms.Label lblGeneratedTitle;
        private System.Windows.Forms.RichTextBox txtSourceText;
        private System.Windows.Forms.RichTextBox txtGeneratedText;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRegenerate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnNext;
    }
}