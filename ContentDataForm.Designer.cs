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
            this.SuspendLayout();
            // 
            // btnAiContent
            // 
            this.btnAiContent.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAiContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAiContent.Location = new System.Drawing.Point(1281, 753);
            this.btnAiContent.Name = "btnAiContent";
            this.btnAiContent.Size = new System.Drawing.Size(311, 70);
            this.btnAiContent.TabIndex = 0;
            this.btnAiContent.Text = "AI Generate";
            this.btnAiContent.UseVisualStyleBackColor = true;
            this.btnAiContent.Click += new System.EventHandler(this.btnAiContent_Click);
            // 
            // ContentDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1619, 953);
            this.Controls.Add(this.btnAiContent);
            this.Name = "ContentDataForm";
            this.Text = "ContentDataForm";
            this.Load += new System.EventHandler(this.ContentDataForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAiContent;
    }
}