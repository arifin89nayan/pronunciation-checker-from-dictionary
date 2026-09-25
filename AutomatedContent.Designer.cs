namespace WindowsFormsApp1
{
    partial class AutomatedContent
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
            this.WorkSpaceFolder = new System.Windows.Forms.Button();
            this.txt_workspace = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AutodetectionBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_userMessage = new System.Windows.Forms.RichTextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WorkSpaceFolder
            // 
            this.WorkSpaceFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.WorkSpaceFolder.Location = new System.Drawing.Point(1004, 71);
            this.WorkSpaceFolder.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.WorkSpaceFolder.Name = "WorkSpaceFolder";
            this.WorkSpaceFolder.Size = new System.Drawing.Size(130, 47);
            this.WorkSpaceFolder.TabIndex = 61;
            this.WorkSpaceFolder.Text = "...";
            this.WorkSpaceFolder.UseVisualStyleBackColor = false;
            this.WorkSpaceFolder.Click += new System.EventHandler(this.WorkSpaceFolder_Click);
            // 
            // txt_workspace
            // 
            this.txt_workspace.BackColor = System.Drawing.Color.White;
            this.txt_workspace.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_workspace.Location = new System.Drawing.Point(53, 130);
            this.txt_workspace.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_workspace.Name = "txt_workspace";
            this.txt_workspace.Size = new System.Drawing.Size(1081, 44);
            this.txt_workspace.TabIndex = 60;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(47, 71);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 41);
            this.label8.TabIndex = 59;
            this.label8.Text = "Workspace Folder";
            // 
            // AutodetectionBtn
            // 
            this.AutodetectionBtn.BackColor = System.Drawing.Color.PaleTurquoise;
            this.AutodetectionBtn.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutodetectionBtn.ForeColor = System.Drawing.Color.Black;
            this.AutodetectionBtn.Location = new System.Drawing.Point(37, 222);
            this.AutodetectionBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.AutodetectionBtn.Name = "AutodetectionBtn";
            this.AutodetectionBtn.Size = new System.Drawing.Size(291, 69);
            this.AutodetectionBtn.TabIndex = 62;
            this.AutodetectionBtn.Text = "Auto detection";
            this.AutodetectionBtn.UseVisualStyleBackColor = false;
            this.AutodetectionBtn.Click += new System.EventHandler(this.AutodetectionBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(47, 311);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 36);
            this.label6.TabIndex = 64;
            this.label6.Text = "Message";
            // 
            // txt_userMessage
            // 
            this.txt_userMessage.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_userMessage.Location = new System.Drawing.Point(37, 350);
            this.txt_userMessage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_userMessage.Name = "txt_userMessage";
            this.txt_userMessage.Size = new System.Drawing.Size(1097, 270);
            this.txt_userMessage.TabIndex = 63;
            this.txt_userMessage.Text = "";
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.Green;
            this.StartBtn.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StartBtn.Location = new System.Drawing.Point(815, 703);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(319, 98);
            this.StartBtn.TabIndex = 65;
            this.StartBtn.Text = "Start Generation";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // AutomatedContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 895);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_userMessage);
            this.Controls.Add(this.AutodetectionBtn);
            this.Controls.Add(this.WorkSpaceFolder);
            this.Controls.Add(this.txt_workspace);
            this.Controls.Add(this.label8);
            this.Name = "AutomatedContent";
            this.Text = "AutomatedContent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button WorkSpaceFolder;
        private System.Windows.Forms.TextBox txt_workspace;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button AutodetectionBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txt_userMessage;
        private System.Windows.Forms.Button StartBtn;
    }
}