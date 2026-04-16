namespace WindowsFormsApp1
{
    partial class CaptionSetup
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
            this.FontStyle = new System.Windows.Forms.Button();
            this.BcgroundColor = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.LblSamtext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FontStyle
            // 
            this.FontStyle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FontStyle.Location = new System.Drawing.Point(32, 57);
            this.FontStyle.Name = "FontStyle";
            this.FontStyle.Size = new System.Drawing.Size(112, 23);
            this.FontStyle.TabIndex = 36;
            this.FontStyle.Text = "Caption Font Style";
            this.FontStyle.UseVisualStyleBackColor = false;
            this.FontStyle.Click += new System.EventHandler(this.FontStyle_Click);
            // 
            // BcgroundColor
            // 
            this.BcgroundColor.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BcgroundColor.Location = new System.Drawing.Point(190, 57);
            this.BcgroundColor.Name = "BcgroundColor";
            this.BcgroundColor.Size = new System.Drawing.Size(150, 23);
            this.BcgroundColor.TabIndex = 47;
            this.BcgroundColor.Text = "Caption Background Color";
            this.BcgroundColor.UseVisualStyleBackColor = false;
            this.BcgroundColor.Click += new System.EventHandler(this.BcgroundColor_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(237, 161);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 48;
            this.OkBtn.Text = "Ok";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // LblSamtext
            // 
            this.LblSamtext.AutoSize = true;
            this.LblSamtext.Location = new System.Drawing.Point(54, 135);
            this.LblSamtext.Name = "LblSamtext";
            this.LblSamtext.Size = new System.Drawing.Size(90, 13);
            this.LblSamtext.TabIndex = 49;
            this.LblSamtext.Text = "Sample Text Test";
            // 
            // CaptionSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 264);
            this.Controls.Add(this.LblSamtext);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.BcgroundColor);
            this.Controls.Add(this.FontStyle);
            this.Name = "CaptionSetup";
            this.Text = "CaptionSetup";
            this.Load += new System.EventHandler(this.CaptionSetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FontStyle;
        private System.Windows.Forms.Button BcgroundColor;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Label LblSamtext;
    }
}