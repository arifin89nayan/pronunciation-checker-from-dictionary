namespace WindowsFormsApp1
{
    partial class SelectionForm
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
            this.option1 = new System.Windows.Forms.RichTextBox();
            this.option2 = new System.Windows.Forms.RichTextBox();
            this.option3 = new System.Windows.Forms.RichTextBox();
            this.correctAnsOpt1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.correctAnsOpt2 = new System.Windows.Forms.RadioButton();
            this.correctAnsOpt3 = new System.Windows.Forms.RadioButton();
            this.correctAnsOpt4 = new System.Windows.Forms.RadioButton();
            this.option4 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // option1
            // 
            this.option1.Location = new System.Drawing.Point(129, 73);
            this.option1.Name = "option1";
            this.option1.Size = new System.Drawing.Size(305, 41);
            this.option1.TabIndex = 1;
            this.option1.Text = "";
            // 
            // option2
            // 
            this.option2.Location = new System.Drawing.Point(129, 147);
            this.option2.Name = "option2";
            this.option2.Size = new System.Drawing.Size(305, 42);
            this.option2.TabIndex = 2;
            this.option2.Text = "";
            // 
            // option3
            // 
            this.option3.Location = new System.Drawing.Point(129, 211);
            this.option3.Name = "option3";
            this.option3.Size = new System.Drawing.Size(305, 39);
            this.option3.TabIndex = 3;
            this.option3.Text = "";
            // 
            // correctAnsOpt1
            // 
            this.correctAnsOpt1.AutoSize = true;
            this.correctAnsOpt1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.correctAnsOpt1.Location = new System.Drawing.Point(454, 85);
            this.correctAnsOpt1.Name = "correctAnsOpt1";
            this.correctAnsOpt1.Size = new System.Drawing.Size(76, 17);
            this.correctAnsOpt1.TabIndex = 4;
            this.correctAnsOpt1.TabStop = true;
            this.correctAnsOpt1.Text = "Quiz Ans 1";
            this.correctAnsOpt1.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(668, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(557, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Generate";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btn_GenerateClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(38, 409);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // correctAnsOpt2
            // 
            this.correctAnsOpt2.AutoSize = true;
            this.correctAnsOpt2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.correctAnsOpt2.Location = new System.Drawing.Point(454, 160);
            this.correctAnsOpt2.Name = "correctAnsOpt2";
            this.correctAnsOpt2.Size = new System.Drawing.Size(76, 17);
            this.correctAnsOpt2.TabIndex = 9;
            this.correctAnsOpt2.TabStop = true;
            this.correctAnsOpt2.Text = "Quiz Ans 2";
            this.correctAnsOpt2.UseVisualStyleBackColor = false;
            // 
            // correctAnsOpt3
            // 
            this.correctAnsOpt3.AutoSize = true;
            this.correctAnsOpt3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.correctAnsOpt3.Location = new System.Drawing.Point(454, 224);
            this.correctAnsOpt3.Name = "correctAnsOpt3";
            this.correctAnsOpt3.Size = new System.Drawing.Size(76, 17);
            this.correctAnsOpt3.TabIndex = 10;
            this.correctAnsOpt3.TabStop = true;
            this.correctAnsOpt3.Text = "Quiz Ans 3";
            this.correctAnsOpt3.UseVisualStyleBackColor = false;
            // 
            // correctAnsOpt4
            // 
            this.correctAnsOpt4.AutoSize = true;
            this.correctAnsOpt4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.correctAnsOpt4.Location = new System.Drawing.Point(454, 286);
            this.correctAnsOpt4.Name = "correctAnsOpt4";
            this.correctAnsOpt4.Size = new System.Drawing.Size(76, 17);
            this.correctAnsOpt4.TabIndex = 11;
            this.correctAnsOpt4.TabStop = true;
            this.correctAnsOpt4.Text = "Quiz Ans 4";
            this.correctAnsOpt4.UseVisualStyleBackColor = false;
            // 
            // option4
            // 
            this.option4.Location = new System.Drawing.Point(129, 273);
            this.option4.Name = "option4";
            this.option4.Size = new System.Drawing.Size(305, 38);
            this.option4.TabIndex = 12;
            this.option4.Text = "";
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.option4);
            this.Controls.Add(this.correctAnsOpt4);
            this.Controls.Add(this.correctAnsOpt3);
            this.Controls.Add(this.correctAnsOpt2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.correctAnsOpt1);
            this.Controls.Add(this.option3);
            this.Controls.Add(this.option2);
            this.Controls.Add(this.option1);
            this.Name = "SelectionForm";
            this.Text = "SelectionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox option1;
        private System.Windows.Forms.RichTextBox option2;
        private System.Windows.Forms.RichTextBox option3;
        private System.Windows.Forms.RadioButton correctAnsOpt1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton correctAnsOpt2;
        private System.Windows.Forms.RadioButton correctAnsOpt3;
        private System.Windows.Forms.RadioButton correctAnsOpt4;
        private System.Windows.Forms.RichTextBox option4;
    }
}