namespace WindowsFormsApp1
{
    partial class NewStartingForm
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
            this.AutoCon = new System.Windows.Forms.Button();
            this.ManualCon = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.IbcFileCreate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.AudioQualityChecker = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AutoCon
            // 
            this.AutoCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.AutoCon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AutoCon.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoCon.Location = new System.Drawing.Point(137, 154);
            this.AutoCon.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.AutoCon.Name = "AutoCon";
            this.AutoCon.Size = new System.Drawing.Size(422, 165);
            this.AutoCon.TabIndex = 0;
            this.AutoCon.Text = "Auto Converter";
            this.AutoCon.UseVisualStyleBackColor = false;
            this.AutoCon.Click += new System.EventHandler(this.AutoCon_Click);
            // 
            // ManualCon
            // 
            this.ManualCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ManualCon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ManualCon.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualCon.Location = new System.Drawing.Point(599, 154);
            this.ManualCon.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ManualCon.Name = "ManualCon";
            this.ManualCon.Size = new System.Drawing.Size(422, 165);
            this.ManualCon.TabIndex = 1;
            this.ManualCon.Text = "Manual Converter";
            this.ManualCon.UseVisualStyleBackColor = false;
            this.ManualCon.Click += new System.EventHandler(this.ManualCon_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1048, 768);
            this.button3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(208, 79);
            this.button3.TabIndex = 12;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // IbcFileCreate
            // 
            this.IbcFileCreate.BackColor = System.Drawing.Color.Red;
            this.IbcFileCreate.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IbcFileCreate.ForeColor = System.Drawing.Color.White;
            this.IbcFileCreate.Location = new System.Drawing.Point(137, 364);
            this.IbcFileCreate.Name = "IbcFileCreate";
            this.IbcFileCreate.Size = new System.Drawing.Size(422, 138);
            this.IbcFileCreate.TabIndex = 13;
            this.IbcFileCreate.Text = "IBC File Generate";
            this.IbcFileCreate.UseVisualStyleBackColor = false;
            this.IbcFileCreate.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Lime;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Maroon;
            this.button2.Location = new System.Drawing.Point(599, 364);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(422, 138);
            this.button2.TabIndex = 14;
            this.button2.Text = "Dictionary File Create";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AudioQualityChecker
            // 
            this.AudioQualityChecker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AudioQualityChecker.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioQualityChecker.ForeColor = System.Drawing.Color.White;
            this.AudioQualityChecker.Location = new System.Drawing.Point(330, 582);
            this.AudioQualityChecker.Name = "AudioQualityChecker";
            this.AudioQualityChecker.Size = new System.Drawing.Size(422, 138);
            this.AudioQualityChecker.TabIndex = 15;
            this.AudioQualityChecker.Text = "Audio Quality Checker";
            this.AudioQualityChecker.UseVisualStyleBackColor = false;
            this.AudioQualityChecker.Click += new System.EventHandler(this.AudioQualityChecker_Click);
            // 
            // NewStartingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1385, 942);
            this.Controls.Add(this.AudioQualityChecker);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.IbcFileCreate);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ManualCon);
            this.Controls.Add(this.AutoCon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "NewStartingForm";
            this.Text = "NewStartingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AutoCon;
        private System.Windows.Forms.Button ManualCon;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button IbcFileCreate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button AudioQualityChecker;
    }
}