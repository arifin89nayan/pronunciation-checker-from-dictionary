namespace WindowsFormsApp1
{
    partial class QuizAutoProcess
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
            this.Quiz_txt_userMessage = new System.Windows.Forms.RichTextBox();
            this.Quiz_button2 = new System.Windows.Forms.Button();
            this.Quiz_btn_output = new System.Windows.Forms.Button();
            this.Quiz_txt_outputLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Quiz_btn_audioFile = new System.Windows.Forms.Button();
            this.Quiz_txt_audioFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Quiz_CSVButton = new System.Windows.Forms.Button();
            this.Quiz_txt_FilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Quiz_btn_template = new System.Windows.Forms.Button();
            this.Quiz_txt_template = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Back_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Quiz_txt_userMessage
            // 
            this.Quiz_txt_userMessage.Location = new System.Drawing.Point(155, 243);
            this.Quiz_txt_userMessage.Name = "Quiz_txt_userMessage";
            this.Quiz_txt_userMessage.Size = new System.Drawing.Size(517, 172);
            this.Quiz_txt_userMessage.TabIndex = 61;
            this.Quiz_txt_userMessage.Text = "";
            // 
            // Quiz_button2
            // 
            this.Quiz_button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Quiz_button2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quiz_button2.ForeColor = System.Drawing.Color.Black;
            this.Quiz_button2.Location = new System.Drawing.Point(358, 183);
            this.Quiz_button2.Name = "Quiz_button2";
            this.Quiz_button2.Size = new System.Drawing.Size(127, 33);
            this.Quiz_button2.TabIndex = 60;
            this.Quiz_button2.Text = "Convert";
            this.Quiz_button2.UseVisualStyleBackColor = false;
            this.Quiz_button2.Click += new System.EventHandler(this.Quiz_button2_Click);
            // 
            // Quiz_btn_output
            // 
            this.Quiz_btn_output.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Quiz_btn_output.Location = new System.Drawing.Point(738, 107);
            this.Quiz_btn_output.Name = "Quiz_btn_output";
            this.Quiz_btn_output.Size = new System.Drawing.Size(38, 18);
            this.Quiz_btn_output.TabIndex = 59;
            this.Quiz_btn_output.Text = "...";
            this.Quiz_btn_output.UseVisualStyleBackColor = false;
            this.Quiz_btn_output.Click += new System.EventHandler(this.Quiz_btn_output_Click);
            // 
            // Quiz_txt_outputLocation
            // 
            this.Quiz_txt_outputLocation.BackColor = System.Drawing.Color.White;
            this.Quiz_txt_outputLocation.Location = new System.Drawing.Point(616, 108);
            this.Quiz_txt_outputLocation.Name = "Quiz_txt_outputLocation";
            this.Quiz_txt_outputLocation.Size = new System.Drawing.Size(120, 20);
            this.Quiz_txt_outputLocation.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(419, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 22);
            this.label4.TabIndex = 57;
            this.label4.Text = "Select Ouput  Location";
            // 
            // Quiz_btn_audioFile
            // 
            this.Quiz_btn_audioFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Quiz_btn_audioFile.Location = new System.Drawing.Point(358, 107);
            this.Quiz_btn_audioFile.Name = "Quiz_btn_audioFile";
            this.Quiz_btn_audioFile.Size = new System.Drawing.Size(38, 18);
            this.Quiz_btn_audioFile.TabIndex = 56;
            this.Quiz_btn_audioFile.Text = "...";
            this.Quiz_btn_audioFile.UseVisualStyleBackColor = false;
            this.Quiz_btn_audioFile.Click += new System.EventHandler(this.Quiz_btn_audioFile_Click);
            // 
            // Quiz_txt_audioFile
            // 
            this.Quiz_txt_audioFile.BackColor = System.Drawing.Color.White;
            this.Quiz_txt_audioFile.Location = new System.Drawing.Point(179, 109);
            this.Quiz_txt_audioFile.Name = "Quiz_txt_audioFile";
            this.Quiz_txt_audioFile.Size = new System.Drawing.Size(174, 20);
            this.Quiz_txt_audioFile.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(24, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 22);
            this.label3.TabIndex = 54;
            this.label3.Text = "Select Audio Files";
            // 
            // Quiz_CSVButton
            // 
            this.Quiz_CSVButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Quiz_CSVButton.Location = new System.Drawing.Point(738, 37);
            this.Quiz_CSVButton.Name = "Quiz_CSVButton";
            this.Quiz_CSVButton.Size = new System.Drawing.Size(38, 18);
            this.Quiz_CSVButton.TabIndex = 53;
            this.Quiz_CSVButton.Text = "...";
            this.Quiz_CSVButton.UseVisualStyleBackColor = false;
            this.Quiz_CSVButton.Click += new System.EventHandler(this.Quiz_CSVButton_Click);
            // 
            // Quiz_txt_FilePath
            // 
            this.Quiz_txt_FilePath.BackColor = System.Drawing.Color.White;
            this.Quiz_txt_FilePath.Location = new System.Drawing.Point(616, 37);
            this.Quiz_txt_FilePath.Name = "Quiz_txt_FilePath";
            this.Quiz_txt_FilePath.Size = new System.Drawing.Size(120, 20);
            this.Quiz_txt_FilePath.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(419, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 22);
            this.label1.TabIndex = 51;
            this.label1.Text = "Select CSV Files";
            // 
            // Quiz_btn_template
            // 
            this.Quiz_btn_template.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Quiz_btn_template.Location = new System.Drawing.Point(358, 40);
            this.Quiz_btn_template.Name = "Quiz_btn_template";
            this.Quiz_btn_template.Size = new System.Drawing.Size(38, 18);
            this.Quiz_btn_template.TabIndex = 50;
            this.Quiz_btn_template.Text = "...";
            this.Quiz_btn_template.UseVisualStyleBackColor = false;
            this.Quiz_btn_template.Click += new System.EventHandler(this.Quiz_btn_template_Click);
            // 
            // Quiz_txt_template
            // 
            this.Quiz_txt_template.BackColor = System.Drawing.Color.White;
            this.Quiz_txt_template.Location = new System.Drawing.Point(169, 40);
            this.Quiz_txt_template.Name = "Quiz_txt_template";
            this.Quiz_txt_template.Size = new System.Drawing.Size(184, 20);
            this.Quiz_txt_template.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(24, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 22);
            this.label2.TabIndex = 48;
            this.label2.Text = "Select Template";
            // 
            // Back_button
            // 
            this.Back_button.ForeColor = System.Drawing.Color.Black;
            this.Back_button.Location = new System.Drawing.Point(28, 404);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(87, 34);
            this.Back_button.TabIndex = 62;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = true;
           // this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // QuizAutoProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.Quiz_txt_userMessage);
            this.Controls.Add(this.Quiz_button2);
            this.Controls.Add(this.Quiz_btn_output);
            this.Controls.Add(this.Quiz_txt_outputLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Quiz_btn_audioFile);
            this.Controls.Add(this.Quiz_txt_audioFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Quiz_CSVButton);
            this.Controls.Add(this.Quiz_txt_FilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Quiz_btn_template);
            this.Controls.Add(this.Quiz_txt_template);
            this.Controls.Add(this.label2);
            this.Name = "QuizAutoProcess";
            this.Text = "QuizAutoProcess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Quiz_txt_userMessage;
        private System.Windows.Forms.Button Quiz_button2;
        private System.Windows.Forms.Button Quiz_btn_output;
        private System.Windows.Forms.TextBox Quiz_txt_outputLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Quiz_btn_audioFile;
        private System.Windows.Forms.TextBox Quiz_txt_audioFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Quiz_CSVButton;
        private System.Windows.Forms.TextBox Quiz_txt_FilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Quiz_btn_template;
        private System.Windows.Forms.TextBox Quiz_txt_template;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Back_button;
    }
}