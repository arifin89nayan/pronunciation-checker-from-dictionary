namespace WindowsFormsApp1
{
    partial class AudioQualityChecker
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
            this.AudioSelect = new System.Windows.Forms.Button();
            this.txt_AudioSelect = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.XmlFile = new System.Windows.Forms.Button();
            this.txt_XMLFile = new System.Windows.Forms.TextBox();
            this.SelectXML = new System.Windows.Forms.Label();
            this.txt_OriginalText = new System.Windows.Forms.RichTextBox();
            this.OriginalText = new System.Windows.Forms.Label();
            this.txt_ConvertMessage = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Back_button = new System.Windows.Forms.Button();
            this.AQCBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AudioSelect
            // 
            this.AudioSelect.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AudioSelect.Location = new System.Drawing.Point(794, 61);
            this.AudioSelect.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.AudioSelect.Name = "AudioSelect";
            this.AudioSelect.Size = new System.Drawing.Size(130, 47);
            this.AudioSelect.TabIndex = 61;
            this.AudioSelect.Text = "...";
            this.AudioSelect.UseVisualStyleBackColor = false;
            this.AudioSelect.Click += new System.EventHandler(this.AudioSelect_Click);
            // 
            // txt_AudioSelect
            // 
            this.txt_AudioSelect.BackColor = System.Drawing.Color.White;
            this.txt_AudioSelect.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AudioSelect.Location = new System.Drawing.Point(56, 127);
            this.txt_AudioSelect.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_AudioSelect.Name = "txt_AudioSelect";
            this.txt_AudioSelect.Size = new System.Drawing.Size(868, 44);
            this.txt_AudioSelect.TabIndex = 60;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Orange;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(50, 68);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(277, 55);
            this.label8.TabIndex = 59;
            this.label8.Text = "Select Audio";
            // 
            // XmlFile
            // 
            this.XmlFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.XmlFile.Location = new System.Drawing.Point(790, 235);
            this.XmlFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.XmlFile.Name = "XmlFile";
            this.XmlFile.Size = new System.Drawing.Size(130, 47);
            this.XmlFile.TabIndex = 64;
            this.XmlFile.Text = "...";
            this.XmlFile.UseVisualStyleBackColor = false;
            this.XmlFile.Click += new System.EventHandler(this.XmlFile_Click);
            // 
            // txt_XMLFile
            // 
            this.txt_XMLFile.BackColor = System.Drawing.Color.White;
            this.txt_XMLFile.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_XMLFile.Location = new System.Drawing.Point(52, 301);
            this.txt_XMLFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_XMLFile.Name = "txt_XMLFile";
            this.txt_XMLFile.Size = new System.Drawing.Size(868, 44);
            this.txt_XMLFile.TabIndex = 63;
            // 
            // SelectXML
            // 
            this.SelectXML.AutoSize = true;
            this.SelectXML.BackColor = System.Drawing.Color.Peru;
            this.SelectXML.Cursor = System.Windows.Forms.Cursors.Default;
            this.SelectXML.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectXML.ForeColor = System.Drawing.Color.Black;
            this.SelectXML.Location = new System.Drawing.Point(46, 242);
            this.SelectXML.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.SelectXML.Name = "SelectXML";
            this.SelectXML.Size = new System.Drawing.Size(352, 55);
            this.SelectXML.TabIndex = 62;
            this.SelectXML.Text = "Select XML File";
            // 
            // txt_OriginalText
            // 
            this.txt_OriginalText.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_OriginalText.Location = new System.Drawing.Point(40, 435);
            this.txt_OriginalText.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_OriginalText.Name = "txt_OriginalText";
            this.txt_OriginalText.Size = new System.Drawing.Size(868, 543);
            this.txt_OriginalText.TabIndex = 65;
            this.txt_OriginalText.Text = "";
            // 
            // OriginalText
            // 
            this.OriginalText.AutoSize = true;
            this.OriginalText.BackColor = System.Drawing.Color.Gold;
            this.OriginalText.Cursor = System.Windows.Forms.Cursors.Default;
            this.OriginalText.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OriginalText.ForeColor = System.Drawing.Color.Black;
            this.OriginalText.Location = new System.Drawing.Point(46, 367);
            this.OriginalText.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OriginalText.Name = "OriginalText";
            this.OriginalText.Size = new System.Drawing.Size(295, 55);
            this.OriginalText.TabIndex = 66;
            this.OriginalText.Text = "Original Text";
            // 
            // txt_ConvertMessage
            // 
            this.txt_ConvertMessage.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_ConvertMessage.Location = new System.Drawing.Point(996, 363);
            this.txt_ConvertMessage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_ConvertMessage.Name = "txt_ConvertMessage";
            this.txt_ConvertMessage.Size = new System.Drawing.Size(638, 651);
            this.txt_ConvertMessage.TabIndex = 67;
            this.txt_ConvertMessage.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(1025, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(488, 48);
            this.label6.TabIndex = 68;
            this.label6.Text = "Message for converting";
            // 
            // Back_button
            // 
            this.Back_button.BackColor = System.Drawing.Color.Red;
            this.Back_button.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Back_button.Location = new System.Drawing.Point(1425, 92);
            this.Back_button.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(193, 98);
            this.Back_button.TabIndex = 70;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // AQCBTN
            // 
            this.AQCBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AQCBTN.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AQCBTN.ForeColor = System.Drawing.Color.Black;
            this.AQCBTN.Location = new System.Drawing.Point(1019, 92);
            this.AQCBTN.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.AQCBTN.Name = "AQCBTN";
            this.AQCBTN.Size = new System.Drawing.Size(360, 98);
            this.AQCBTN.TabIndex = 69;
            this.AQCBTN.Text = "Convert";
            this.AQCBTN.UseVisualStyleBackColor = false;
            this.AQCBTN.Click += new System.EventHandler(this.AQCBTN_ClickAsync);
            //this.AQCBTN.Click += async (s, e) => await this.AQCBTN_ClickAsync(s, e);
            // 
            // AudioQualityChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1720, 1267);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.AQCBTN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_ConvertMessage);
            this.Controls.Add(this.OriginalText);
            this.Controls.Add(this.txt_OriginalText);
            this.Controls.Add(this.XmlFile);
            this.Controls.Add(this.txt_XMLFile);
            this.Controls.Add(this.SelectXML);
            this.Controls.Add(this.AudioSelect);
            this.Controls.Add(this.txt_AudioSelect);
            this.Controls.Add(this.label8);
            this.Name = "AudioQualityChecker";
            this.Text = "AudioQualityChecker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AudioSelect;
        private System.Windows.Forms.TextBox txt_AudioSelect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button XmlFile;
        private System.Windows.Forms.TextBox txt_XMLFile;
        private System.Windows.Forms.Label SelectXML;
        private System.Windows.Forms.RichTextBox txt_OriginalText;
        private System.Windows.Forms.Label OriginalText;
        private System.Windows.Forms.RichTextBox txt_ConvertMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.Button AQCBTN;
    }
}