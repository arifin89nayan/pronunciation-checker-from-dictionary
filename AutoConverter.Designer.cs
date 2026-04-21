namespace WindowsFormsApp1
{
    partial class AutoConverter
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
            this.btn_template = new System.Windows.Forms.Button();
            this.txt_template = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SDataButton = new System.Windows.Forms.Button();
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_audioFile = new System.Windows.Forms.Button();
            this.txt_audioFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_output = new System.Windows.Forms.Button();
            this.txt_outputLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_userMessage = new System.Windows.Forms.RichTextBox();
            this.Back_button = new System.Windows.Forms.Button();
            this.LangBtn = new System.Windows.Forms.Button();
            this.LanTextBx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIBCFile = new System.Windows.Forms.TextBox();
            this.btn_IbcFiles = new System.Windows.Forms.Button();
            this.ParentFolder = new System.Windows.Forms.Button();
            this.txt_workspace = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_template
            // 
            this.btn_template.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_template.Location = new System.Drawing.Point(788, 350);
            this.btn_template.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_template.Name = "btn_template";
            this.btn_template.Size = new System.Drawing.Size(130, 47);
            this.btn_template.TabIndex = 8;
            this.btn_template.Text = "...";
            this.btn_template.UseVisualStyleBackColor = false;
            this.btn_template.Click += new System.EventHandler(this.btn_template_click);
            // 
            // txt_template
            // 
            this.txt_template.BackColor = System.Drawing.Color.White;
            this.txt_template.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_template.Location = new System.Drawing.Point(50, 416);
            this.txt_template.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_template.Name = "txt_template";
            this.txt_template.Size = new System.Drawing.Size(868, 44);
            this.txt_template.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LemonChiffon;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(44, 361);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(345, 55);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select Template";
            // 
            // SDataButton
            // 
            this.SDataButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SDataButton.Location = new System.Drawing.Point(788, 704);
            this.SDataButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.SDataButton.Name = "SDataButton";
            this.SDataButton.Size = new System.Drawing.Size(130, 47);
            this.SDataButton.TabIndex = 11;
            this.SDataButton.Text = "...";
            this.SDataButton.UseVisualStyleBackColor = false;
            this.SDataButton.Click += new System.EventHandler(this.SDataButton_Click);
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.BackColor = System.Drawing.Color.White;
            this.txt_FilePath.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FilePath.Location = new System.Drawing.Point(50, 770);
            this.txt_FilePath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.Size = new System.Drawing.Size(868, 44);
            this.txt_FilePath.TabIndex = 10;
            this.txt_FilePath.TextChanged += new System.EventHandler(this.txt_FilePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LemonChiffon;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(44, 709);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(483, 55);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select CSV/Excel Files";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_audioFile
            // 
            this.btn_audioFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_audioFile.Location = new System.Drawing.Point(788, 468);
            this.btn_audioFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_audioFile.Name = "btn_audioFile";
            this.btn_audioFile.Size = new System.Drawing.Size(130, 47);
            this.btn_audioFile.TabIndex = 14;
            this.btn_audioFile.Text = "...";
            this.btn_audioFile.UseVisualStyleBackColor = false;
            this.btn_audioFile.Click += new System.EventHandler(this.btn_audioFile_Click);
            // 
            // txt_audioFile
            // 
            this.txt_audioFile.BackColor = System.Drawing.Color.White;
            this.txt_audioFile.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_audioFile.Location = new System.Drawing.Point(50, 534);
            this.txt_audioFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_audioFile.Name = "txt_audioFile";
            this.txt_audioFile.Size = new System.Drawing.Size(868, 44);
            this.txt_audioFile.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LemonChiffon;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(44, 477);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(384, 55);
            this.label3.TabIndex = 12;
            this.label3.Text = "Select Audio Files";
            // 
            // btn_output
            // 
            this.btn_output.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_output.Location = new System.Drawing.Point(788, 853);
            this.btn_output.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_output.Name = "btn_output";
            this.btn_output.Size = new System.Drawing.Size(130, 47);
            this.btn_output.TabIndex = 17;
            this.btn_output.Text = "...";
            this.btn_output.UseVisualStyleBackColor = false;
            this.btn_output.Click += new System.EventHandler(this.btn_output_click);
            // 
            // txt_outputLocation
            // 
            this.txt_outputLocation.BackColor = System.Drawing.Color.White;
            this.txt_outputLocation.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_outputLocation.Location = new System.Drawing.Point(50, 919);
            this.txt_outputLocation.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_outputLocation.Name = "txt_outputLocation";
            this.txt_outputLocation.Size = new System.Drawing.Size(868, 44);
            this.txt_outputLocation.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LemonChiffon;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(44, 856);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(450, 51);
            this.label4.TabIndex = 15;
            this.label4.Text = "Select Ouput  Location";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(949, 13);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(360, 98);
            this.button2.TabIndex = 18;
            this.button2.Text = "Convert";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_userMessage
            // 
            this.txt_userMessage.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_userMessage.Location = new System.Drawing.Point(949, 179);
            this.txt_userMessage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_userMessage.Name = "txt_userMessage";
            this.txt_userMessage.Size = new System.Drawing.Size(599, 528);
            this.txt_userMessage.TabIndex = 47;
            this.txt_userMessage.Text = "";
            // 
            // Back_button
            // 
            this.Back_button.BackColor = System.Drawing.Color.Red;
            this.Back_button.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Back_button.Location = new System.Drawing.Point(1355, 13);
            this.Back_button.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(193, 98);
            this.Back_button.TabIndex = 48;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // LangBtn
            // 
            this.LangBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LangBtn.Location = new System.Drawing.Point(788, 588);
            this.LangBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.LangBtn.Name = "LangBtn";
            this.LangBtn.Size = new System.Drawing.Size(130, 47);
            this.LangBtn.TabIndex = 51;
            this.LangBtn.Text = "...";
            this.LangBtn.UseVisualStyleBackColor = false;
            this.LangBtn.Click += new System.EventHandler(this.LangBtn_Click);
            // 
            // LanTextBx
            // 
            this.LanTextBx.BackColor = System.Drawing.Color.White;
            this.LanTextBx.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LanTextBx.Location = new System.Drawing.Point(50, 654);
            this.LanTextBx.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.LanTextBx.Name = "LanTextBx";
            this.LanTextBx.Size = new System.Drawing.Size(868, 44);
            this.LanTextBx.TabIndex = 50;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LemonChiffon;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(44, 595);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(586, 55);
            this.label5.TabIndex = 49;
            this.label5.Text = "Select Language Excel Files";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(959, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(488, 48);
            this.label6.TabIndex = 52;
            this.label6.Text = "Message for converting";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.LemonChiffon;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(44, 955);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(491, 51);
            this.label7.TabIndex = 53;
            this.label7.Text = "Select IBC File  Location";
            // 
            // txtIBCFile
            // 
            this.txtIBCFile.BackColor = System.Drawing.Color.White;
            this.txtIBCFile.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIBCFile.Location = new System.Drawing.Point(50, 1012);
            this.txtIBCFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtIBCFile.Name = "txtIBCFile";
            this.txtIBCFile.Size = new System.Drawing.Size(868, 44);
            this.txtIBCFile.TabIndex = 54;
            // 
            // btn_IbcFiles
            // 
            this.btn_IbcFiles.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_IbcFiles.Location = new System.Drawing.Point(799, 955);
            this.btn_IbcFiles.Name = "btn_IbcFiles";
            this.btn_IbcFiles.Size = new System.Drawing.Size(119, 50);
            this.btn_IbcFiles.TabIndex = 55;
            this.btn_IbcFiles.Text = "....";
            this.btn_IbcFiles.UseVisualStyleBackColor = false;
            this.btn_IbcFiles.Click += new System.EventHandler(this.btn_IbcFiles_Click);
            // 
            // ParentFolder
            // 
            this.ParentFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ParentFolder.Location = new System.Drawing.Point(768, 94);
            this.ParentFolder.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ParentFolder.Name = "ParentFolder";
            this.ParentFolder.Size = new System.Drawing.Size(130, 47);
            this.ParentFolder.TabIndex = 58;
            this.ParentFolder.Text = "...";
            this.ParentFolder.UseVisualStyleBackColor = false;
            this.ParentFolder.Click += new System.EventHandler(this.ParentFolder_Click);
            // 
            // txt_workspace
            // 
            this.txt_workspace.BackColor = System.Drawing.Color.White;
            this.txt_workspace.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_workspace.Location = new System.Drawing.Point(30, 160);
            this.txt_workspace.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_workspace.Name = "txt_workspace";
            this.txt_workspace.Size = new System.Drawing.Size(868, 44);
            this.txt_workspace.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Orange;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(24, 101);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(307, 55);
            this.label8.TabIndex = 56;
            this.label8.Text = "Parent Folder";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Orange;
            this.label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(24, 281);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(285, 55);
            this.label9.TabIndex = 59;
            this.label9.Text = "Child Folder";
            // 
            // AutoConverter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1878, 1128);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ParentFolder);
            this.Controls.Add(this.txt_workspace);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_IbcFiles);
            this.Controls.Add(this.txtIBCFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LangBtn);
            this.Controls.Add(this.LanTextBx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.txt_userMessage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_output);
            this.Controls.Add(this.txt_outputLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_audioFile);
            this.Controls.Add(this.txt_audioFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SDataButton);
            this.Controls.Add(this.txt_FilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_template);
            this.Controls.Add(this.txt_template);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "AutoConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GuideAutoConverter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_template;
        private System.Windows.Forms.TextBox txt_template;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SDataButton;
        private System.Windows.Forms.TextBox txt_FilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_audioFile;
        private System.Windows.Forms.TextBox txt_audioFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_output;
        private System.Windows.Forms.TextBox txt_outputLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox txt_userMessage;
        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.Button LangBtn;
        private System.Windows.Forms.TextBox LanTextBx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIBCFile;
        private System.Windows.Forms.Button btn_IbcFiles;
        private System.Windows.Forms.Button ParentFolder;
        private System.Windows.Forms.TextBox txt_workspace;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}