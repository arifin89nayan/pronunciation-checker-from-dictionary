namespace WindowsFormsApp1.UIDesign
{
    partial class KanjiReview
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NextBtn = new System.Windows.Forms.Button();
            this.Back_button = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1896, 110);
            this.pnlHeader.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(40, 18);
            this.label2.Name = "label2";
            this.label2.Text = "Kanji Review";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(188)))));
            this.lblSubtitle.Location = new System.Drawing.Point(44, 68);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Confirm or correct each hiragana reading. Green = matched, pink = needs review.";
            // 
            // NextBtn
            // 
            this.NextBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.NextBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.NextBtn.ForeColor = System.Drawing.Color.Black;
            this.NextBtn.Location = new System.Drawing.Point(1490, 905);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(180, 80);
            this.NextBtn.TabIndex = 1;
            this.NextBtn.Text = "Next";
            this.NextBtn.UseVisualStyleBackColor = false;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // Back_button
            // 
            this.Back_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.Back_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_button.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.Back_button.ForeColor = System.Drawing.Color.White;
            this.Back_button.Location = new System.Drawing.Point(1685, 905);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(180, 80);
            this.Back_button.TabIndex = 2;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // KanjiReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1896, 1009);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.pnlHeader);
            this.Name = "KanjiReview";
            this.Text = "Kanji Review";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Button Back_button;
    }
}