namespace TTSAgent.UI.Screens
{
    partial class DashboardScreen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.TableLayoutPanel cardsLayout;
        private System.Windows.Forms.Panel cardDictionary;
        private System.Windows.Forms.Panel cardTerms;
        private System.Windows.Forms.Panel cardReview;
        private System.Windows.Forms.Panel cardAudio;
        private System.Windows.Forms.Label dictionaryCountLabel;
        private System.Windows.Forms.Label termsCountLabel;
        private System.Windows.Forms.Label reviewCountLabel;
        private System.Windows.Forms.Label audioStatusLabel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnDictionary;
        private System.Windows.Forms.TextBox workflowTextBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            titleLabel = new System.Windows.Forms.Label();
            introLabel = new System.Windows.Forms.Label();
            cardsLayout = new System.Windows.Forms.TableLayoutPanel();
            cardDictionary = new System.Windows.Forms.Panel();
            dictionaryCountLabel = new System.Windows.Forms.Label();
            cardTerms = new System.Windows.Forms.Panel();
            termsCountLabel = new System.Windows.Forms.Label();
            cardReview = new System.Windows.Forms.Panel();
            reviewCountLabel = new System.Windows.Forms.Label();
            cardAudio = new System.Windows.Forms.Panel();
            audioStatusLabel = new System.Windows.Forms.Label();
            btnStart = new System.Windows.Forms.Button();
            btnDictionary = new System.Windows.Forms.Button();
            workflowTextBox = new System.Windows.Forms.TextBox();
            rootLayout.SuspendLayout();
            cardsLayout.SuspendLayout();
            cardDictionary.SuspendLayout();
            cardTerms.SuspendLayout();
            cardReview.SuspendLayout();
            cardAudio.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 2;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(introLabel, 0, 1);
            rootLayout.Controls.Add(cardsLayout, 0, 2);
            rootLayout.Controls.Add(btnStart, 0, 3);
            rootLayout.Controls.Add(btnDictionary, 1, 3);
            rootLayout.Controls.Add(workflowTextBox, 0, 4);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 5;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Size = new System.Drawing.Size(999, 674);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            rootLayout.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            titleLabel.Location = new System.Drawing.Point(3, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(374, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Generative AI TTS Script Agent";
            // 
            // introLabel
            // 
            introLabel.AutoSize = true;
            rootLayout.SetColumnSpan(introLabel, 2);
            introLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            introLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            introLabel.Location = new System.Drawing.Point(3, 52);
            introLabel.Name = "introLabel";
            introLabel.Size = new System.Drawing.Size(902, 38);
            introLabel.TabIndex = 1;
            introLabel.Text = "This app extracts difficult Japanese kanji readings, sends uncertain words to human review, builds a fixed pronunciation list, generates SSML, synthesizes Azure TTS audio, and checks the generated voice.";
            // 
            // cardsLayout
            // 
            cardsLayout.ColumnCount = 4;
            rootLayout.SetColumnSpan(cardsLayout, 2);
            cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            cardsLayout.Controls.Add(cardDictionary, 0, 0);
            cardsLayout.Controls.Add(cardTerms, 1, 0);
            cardsLayout.Controls.Add(cardReview, 2, 0);
            cardsLayout.Controls.Add(cardAudio, 3, 0);
            cardsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            cardsLayout.Location = new System.Drawing.Point(3, 125);
            cardsLayout.Name = "cardsLayout";
            cardsLayout.RowCount = 1;
            cardsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            cardsLayout.Size = new System.Drawing.Size(993, 144);
            cardsLayout.TabIndex = 2;
            // 
            // cardDictionary
            // 
            cardDictionary.BackColor = System.Drawing.Color.White;
            cardDictionary.Controls.Add(dictionaryCountLabel);
            cardDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            cardDictionary.Location = new System.Drawing.Point(3, 3);
            cardDictionary.Name = "cardDictionary";
            cardDictionary.Padding = new System.Windows.Forms.Padding(18);
            cardDictionary.Size = new System.Drawing.Size(242, 138);
            cardDictionary.TabIndex = 0;
            // 
            // dictionaryCountLabel
            // 
            dictionaryCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            dictionaryCountLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            dictionaryCountLabel.Location = new System.Drawing.Point(18, 18);
            dictionaryCountLabel.Name = "dictionaryCountLabel";
            dictionaryCountLabel.Size = new System.Drawing.Size(206, 102);
            dictionaryCountLabel.TabIndex = 0;
            dictionaryCountLabel.Text = "Dictionary\r\n0";
            dictionaryCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardTerms
            // 
            cardTerms.BackColor = System.Drawing.Color.White;
            cardTerms.Controls.Add(termsCountLabel);
            cardTerms.Dock = System.Windows.Forms.DockStyle.Fill;
            cardTerms.Location = new System.Drawing.Point(251, 3);
            cardTerms.Name = "cardTerms";
            cardTerms.Padding = new System.Windows.Forms.Padding(18);
            cardTerms.Size = new System.Drawing.Size(242, 138);
            cardTerms.TabIndex = 1;
            // 
            // termsCountLabel
            // 
            termsCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            termsCountLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            termsCountLabel.Location = new System.Drawing.Point(18, 18);
            termsCountLabel.Name = "termsCountLabel";
            termsCountLabel.Size = new System.Drawing.Size(206, 102);
            termsCountLabel.TabIndex = 0;
            termsCountLabel.Text = "Extracted Terms\r\n0";
            termsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardReview
            // 
            cardReview.BackColor = System.Drawing.Color.White;
            cardReview.Controls.Add(reviewCountLabel);
            cardReview.Dock = System.Windows.Forms.DockStyle.Fill;
            cardReview.Location = new System.Drawing.Point(499, 3);
            cardReview.Name = "cardReview";
            cardReview.Padding = new System.Windows.Forms.Padding(18);
            cardReview.Size = new System.Drawing.Size(242, 138);
            cardReview.TabIndex = 2;
            // 
            // reviewCountLabel
            // 
            reviewCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            reviewCountLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            reviewCountLabel.Location = new System.Drawing.Point(18, 18);
            reviewCountLabel.Name = "reviewCountLabel";
            reviewCountLabel.Size = new System.Drawing.Size(206, 102);
            reviewCountLabel.TabIndex = 0;
            reviewCountLabel.Text = "Pending Review\r\n0";
            reviewCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardAudio
            // 
            cardAudio.BackColor = System.Drawing.Color.White;
            cardAudio.Controls.Add(audioStatusLabel);
            cardAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            cardAudio.Location = new System.Drawing.Point(747, 3);
            cardAudio.Name = "cardAudio";
            cardAudio.Padding = new System.Windows.Forms.Padding(18);
            cardAudio.Size = new System.Drawing.Size(243, 138);
            cardAudio.TabIndex = 3;
            // 
            // audioStatusLabel
            // 
            audioStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            audioStatusLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            audioStatusLabel.Location = new System.Drawing.Point(18, 18);
            audioStatusLabel.Name = "audioStatusLabel";
            audioStatusLabel.Size = new System.Drawing.Size(207, 102);
            audioStatusLabel.TabIndex = 0;
            audioStatusLabel.Text = "Audio\r\nNot generated";
            audioStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnStart.Location = new System.Drawing.Point(3, 281);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(180, 38);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start from Script Input";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // btnDictionary
            // 
            btnDictionary.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnDictionary.Location = new System.Drawing.Point(502, 281);
            btnDictionary.Name = "btnDictionary";
            btnDictionary.Size = new System.Drawing.Size(180, 38);
            btnDictionary.TabIndex = 4;
            btnDictionary.Text = "Open Dictionary";
            btnDictionary.UseVisualStyleBackColor = true;
            // 
            // workflowTextBox
            // 
            rootLayout.SetColumnSpan(workflowTextBox, 2);
            workflowTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            workflowTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            workflowTextBox.Location = new System.Drawing.Point(3, 331);
            workflowTextBox.Multiline = true;
            workflowTextBox.Name = "workflowTextBox";
            workflowTextBox.ReadOnly = true;
            workflowTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            workflowTextBox.Size = new System.Drawing.Size(993, 340);
            workflowTextBox.TabIndex = 5;
            // 
            // DashboardScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "DashboardScreen";
            Size = new System.Drawing.Size(999, 674);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            cardsLayout.ResumeLayout(false);
            cardDictionary.ResumeLayout(false);
            cardTerms.ResumeLayout(false);
            cardReview.ResumeLayout(false);
            cardAudio.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
