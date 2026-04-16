namespace WindowsFormsApp1
{
    partial class SetupForCaption
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
            //this.Language = new System.Windows.Forms.ComboBox();
            //this.Genderbox = new System.Windows.Forms.ComboBox();
            //this.Voice = new System.Windows.Forms.ComboBox();



            this.label1 = new System.Windows.Forms.Label();
            this.Language = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Voice = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Genderbox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Language:";
            // 
            // Language
            // 
            this.Language.FormattingEnabled = true;
            this.Language.Location = new System.Drawing.Point(131, 105);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(121, 21);
            this.Language.TabIndex = 1;
            this.Language.SelectedIndexChanged += new System.EventHandler(this.Language_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Voice:";
            // 
            // Voice
            // 
            this.Voice.FormattingEnabled = true;
            this.Voice.Location = new System.Drawing.Point(133, 202);
            this.Voice.Name = "Voice";
            this.Voice.Size = new System.Drawing.Size(121, 21);
            this.Voice.TabIndex = 3;
            this.Voice.SelectedIndexChanged += new System.EventHandler(this.Voice_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(378, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gender:";
            // 
            // Genderbox
            // 
            this.Genderbox.FormattingEnabled = true;
            this.Genderbox.Location = new System.Drawing.Point(131, 149);
            this.Genderbox.Name = "Genderbox";
            this.Genderbox.Size = new System.Drawing.Size(121, 21);
            this.Genderbox.TabIndex = 6;
            this.Genderbox.SelectedIndexChanged += new System.EventHandler(this.Genderbox_SelectedIndexChanged);
            // 
            // SetupForCaption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 411);
            this.Controls.Add(this.Genderbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Voice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Language);
            this.Controls.Add(this.label1);
            this.Name = "SetupForCaption";
            this.Text = "SetupForCaption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Voice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Genderbox;
    }
}